using System.Collections;
using UnityEngine;
using System;

public class TargetCollision : MonoBehaviour
{
    
    public GameObject impactParticles = null;

    private TargetSpawner targetSpawner;

    GameManager gameManager;

    public int counter;


    static private GameObject targetSpawnerGO;

    private static AudioSource impactSound;

    private void Awake()
    {
        targetSpawner = GameObject.Find("Target Spawner").GetComponent<TargetSpawner>();
    }
    void Start()
    {
        counter = 0;
        impactSound = GetComponent<AudioSource>();
        targetSpawnerGO = GameObject.Find("Target Spawner");
        targetSpawner = targetSpawnerGO.GetComponent<TargetSpawner>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(Countdown());
    }

    public void SpawnTarget(Vector3 coords)
    {
        GameObject targett = Instantiate(targetSpawner.targetPrefab, coords, Quaternion.Euler(0,90,0));

        targetSpawner.Targets.Add(targett);

        targett.GetComponent<Collider>().enabled = true;
        targett.GetComponent<Rigidbody>().isKinematic = true;

        gameManager.totalTargets++;
    }

   
    
    Vector3 FindMax()
    {
        Vector3 max = new Vector3(0, 0, 0);
        if (targetSpawner.Targets.Count == 0)
        {
            max = targetSpawner.RandomCoords();
        } else if (targetSpawner.Targets.Count == 1)
        {
            max = targetSpawner.Targets[0].transform.position;

        } else {
            max = targetSpawner.Targets[0].transform.position;
            for (int i = 1; i < targetSpawner.Targets.Count; i++)
            {
                if(targetSpawner.Targets[i].GetComponent<TargetCollision>().counter >= targetSpawner.Targets[i - 1].GetComponent<TargetCollision>().counter)
                {
                    max = targetSpawner.Targets[i].transform.position;
                }
            } 
        }
        return max;
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            other.gameObject.GetComponent<Collider>().enabled = false;
            gameManager.totalTargets--;
            Vector3 maxMissed = FindMax();
            Vector3 newCoords;
            if (maxMissed == gameObject.transform.position)
            {
                newCoords = targetSpawner.RandomCoords();
            } else
            {
                newCoords = (maxMissed + gameObject.transform.position) * 0.5f;
                newCoords = (newCoords + gameObject.transform.position) * 0.5f;
                if ((Math.Abs((maxMissed - newCoords).x) <= 0.1f) || (Math.Abs((maxMissed - newCoords).y) <= 0.1f) || (Math.Abs((maxMissed - newCoords).z) <= 0.1f))
                {
                    newCoords = targetSpawner.RandomCoords();
                }
            }

            
            //other.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;

            impactSound.volume = other.relativeVelocity.normalized.magnitude;
            impactSound.Play();

            GameObject sparkle = Instantiate(impactParticles, transform.position, Quaternion.identity);
            Destroy(sparkle, 1f);

            gameManager.score++;
            other.transform.parent = transform;

            other.gameObject.GetComponent<Rigidbody>().Sleep();

            StopCoroutine(Countdown());
            targetSpawner.Targets.Remove(gameObject);
            SpawnTarget(newCoords);

            gameManager.HitTargets.Add(transform.position);
            Destroy(gameObject,2f);
        }
    }
    IEnumerator Countdown()
    { 
        while (true)
        {
            yield return new WaitForSeconds(1f);
            counter++;
        }
    
    }

}

