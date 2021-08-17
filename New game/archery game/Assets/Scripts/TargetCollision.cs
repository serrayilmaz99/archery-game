using System.Collections;
using UnityEngine;
using System.Linq;
using System.Windows;
using System;
using System.Text;
using UnityEngine.Networking;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine.UI;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class TargetCollision : MonoBehaviour
{
   // [SerializeField]
   // private TargetMovement targetMovement;
    [SerializeField]
    private GameObject impactParticles;

    private TargetSpawner targetSpawner;

    GameManager GameManager;

    public int counter;
    public bool hit;

    [SerializeField]
    public GameObject targetPrefab = null;

    static private GameObject targetSpawnerGO;

    private static AudioSource impactSound;

    public List<Vector3> HitTargets = new List<Vector3>();
    public List<Vector3> MissedTargets = new List<Vector3>();

    // Start is called before the first frame update


    // Start is called before the first frame update
    private void Awake()
    {
        targetSpawner = GameObject.Find("Target Spawner").GetComponent<TargetSpawner>();
    }
    void Start()
    {
        //Debug.Log("Start");
        //targetMovement = GetComponent<TargetMovement>();
        counter = 0;
        impactSound = GetComponent<AudioSource>();
        targetSpawnerGO = GameObject.Find("Target Spawner");
        targetSpawner = targetSpawnerGO.GetComponent<TargetSpawner>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(Countdown());
    }

    void Update()
    {
    }      

    public void SpawnTarget(Vector3 coords)
    {
        GameObject targett = Instantiate(targetPrefab, coords, targetPrefab.transform.rotation);

        targetSpawner.Targets.Add(targett);

        targett.GetComponent<Collider>().enabled = true;
        targett.GetComponent<Rigidbody>().isKinematic = true;

        GameManager.totalTargets++;
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
          //  Debug.Log("max");
        } else {
            max = targetSpawner.Targets[0].transform.position;
            for (int i = 1; i < targetSpawner.Targets.Count; i++)
            {
                if(targetSpawner.Targets[i].GetComponent<TargetCollision>().counter >= targetSpawner.Targets[i - 1].GetComponent<TargetCollision>().counter)
                {
                    max = targetSpawner.Targets[i].transform.position;
               //     Debug.Log("max");
                }
            } 
        }
        return max;
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            GameManager.totalTargets--;

            //HitTargets.Add(other.gameObject.transform.position);

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

            other.gameObject.GetComponent<Collider>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;

            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;

            impactSound.volume = other.relativeVelocity.normalized.magnitude;
            impactSound.Play();

            Instantiate(impactParticles, transform.position, impactParticles.transform.rotation);

            //targetMovement.enabled = false;

            other.transform.parent = transform;

            other.gameObject.GetComponent<Rigidbody>().Sleep();
            // other.gameObject.GetComponent<Rigidbody>().collisionDetectionMode=CollisionDetectionMode.ContinuousSpeculative;

            StopCoroutine(Countdown());
            targetSpawner.Targets.Remove(gameObject);

            SpawnTarget(newCoords);
        }
    }
    IEnumerator Countdown()
    { 
        while (true)
        {
            yield return new WaitForSeconds(1f);
            counter++;
        }
        
       // Debug.Log(counter);
    }

}


/*
public class TargetCollision : MonoBehaviour
{
    [SerializeField]
    private TargetMovement targetMovement;
    [SerializeField]
    private GameObject impactParticles;

    private GameObject targetSpawnerGO;
    private TargetSpawner targetSpawner;

    private AudioSource impactSound;

    static public List<Vector3> HitTargets = new List<Vector3>();

    // Start is called before the first frame update
   

        // Start is called before the first frame update
    void Start()
    {
        targetMovement = GetComponent<TargetMovement>();
        impactSound = GetComponent<AudioSource>();
        targetSpawnerGO= GameObject.Find("Target Spawner");
        targetSpawner= targetSpawnerGO.GetComponent<TargetSpawner>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            TargetSpawner.totalTargets--;

            HitTargets.Add(other.gameObject.transform.position);

            other.gameObject.GetComponent<Collider>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            
            GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = false;


    // GameManager.score += GameManager.level;
            GameManager.score ++;
            
            if (GameManager.score % 10 == 0 && GameManager.score != 0)
            {
                targetSpawner.transform.position = new Vector3(targetSpawner.transform.position.x + 0.3f, targetSpawner.transform.position.y, targetSpawner.transform.position.z);
                GameManager.gameStateText.text = "You Passed Level " + GameManager.level + "!";
                GameManager.level++;
                GameManager.timer += 10;
                //gameStateTextAnim = gameStateUI.GetComponent<Animator>();

                GameManager.gameStateTextAnim.SetBool("ShowText", true);
               
                StartCoroutine(Countdown());

            }
                
            impactSound.volume = other.relativeVelocity.normalized.magnitude;
            impactSound.Play();

            //Instantiate(impactParticles, transform.position, impactParticles.transform.rotation);

            targetMovement.enabled = false;

            other.transform.parent = transform;

            other.gameObject.GetComponent<Rigidbody>().Sleep();
           // other.gameObject.GetComponent<Rigidbody>().collisionDetectionMode=CollisionDetectionMode.ContinuousSpeculative;

           var document = new BsonDocument { { "x", gameObject.transform.position.x },{ "y", gameObject.transform.position.y },
               { "z", gameObject.transform.position.z },{"hit", "true" } };
           GameManager.collection.InsertOne(document);

           
           // OYUNUN EN SONUNDA TOPLU HALDE DATABASE'E GEÇİR LİSTEYİ 


            Destroy(gameObject, 1f);
            
        }
    }
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.9f);
        //Debug.Log("a");       
        GameManager.gameStateTextAnim.SetBool("ShowText", false);
      //  TargetSpawner.DestroyTargets();
    }

} */

