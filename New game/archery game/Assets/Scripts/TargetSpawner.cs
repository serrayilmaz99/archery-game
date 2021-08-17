using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Text;
using UnityEngine.Networking;
using MongoDB.Bson;
using MongoDB.Driver;

public class TargetSpawner : MonoBehaviour
{
    public Vector3 spawnArea = new Vector3(1, 3, 4);
    public List<GameObject> Targets = new List<GameObject>();
    GameManager gameManager;

    [SerializeField]
    public GameObject targetPrefab = null;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.gameStarted && gameManager.totalTargets == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                //Debug.Log("Loop");
                SpawnTarget();
            }
        }
        if (!gameManager.gameStarted && gameManager.totalTargets > 0)
        {
            //Debug.Log("Here");
            DestroyTargets();
            gameManager.totalTargets = 0;
        }
    }
    public Vector3 RandomCoords()
    {
        float xMin = GameObject.FindGameObjectWithTag("TargetSpawner").transform.position.x - spawnArea.x / 2;
        float yMin = GameObject.FindGameObjectWithTag("TargetSpawner").transform.position.y - spawnArea.y / 2;
        float zMin = GameObject.FindGameObjectWithTag("TargetSpawner").transform.position.z - spawnArea.z / 2;
        float xMax = GameObject.FindGameObjectWithTag("TargetSpawner").transform.position.x + spawnArea.x / 2;
        float yMax = GameObject.FindGameObjectWithTag("TargetSpawner").transform.position.y + spawnArea.y / 2;
        float zMax = GameObject.FindGameObjectWithTag("TargetSpawner").transform.position.z + spawnArea.z / 2;


        float xRandom = Random.Range(xMin, xMax);
        float yRandom = Random.Range(yMin, yMax);
        float zRandom = Random.Range(zMin, zMax);

        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

        return posTarget;
    }
    public void SpawnTarget()
    {
        Vector3 posTarget = RandomCoords();

        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);

        Targets.Add(targett);

        targett.GetComponent<Collider>().enabled = true;
        targett.GetComponent<Rigidbody>().isKinematic = true;

        gameManager.totalTargets++;
    }

    // Update is called once per frame

    public void DestroyTargets()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 0; i < targets.Length; i++)
        {
            /*
            var document = new BsonDocument { { "x", targets[i].gameObject.transform.position.x},{ "y", targets[i].gameObject.transform.position.y},
                { "z", targets[i].gameObject.transform.position.z },{"hit", "false" } };
            GameManager.collection.InsertOne(document); */

            TargetMovement targetMovement = targets[i].GetComponent<TargetMovement>();
            targetMovement.enabled = false;

            Rigidbody targetRigidBody = targets[i].GetComponent<Rigidbody>();
            targetRigidBody.isKinematic = false;

            Collider targetCollider = targets[i].GetComponent<Collider>();
            targetCollider.enabled = false;

            Targets.Remove(targets[i]);

            Destroy(targets[i], 1f);
        }

    }

    private static Transform GetTransform(GameObject[] targets, int i)
    {
        return targets[i].transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}

/*
public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector3 spawnArea = new Vector3(3, 3, 4);

    private Vector3 scaleChange;

    static public Vector3 initSpawnerPos;

    public static int totalTargets;

    static public float scale = 1f;

    [SerializeField]
    private GameObject targetPrefab = null;

    public List<Vector3> MissedTargets = new List<Vector3>();


    void Start()
    {
        initSpawnerPos = transform.position;
        scaleChange = new Vector3(-0.2f, -0.2f, -0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (totalTargets == 0 && GameManager.gameStarted)
        {
            SpawnTargets();
        }

        if (GameManager.gameStarted == false && totalTargets > 0)
        {
            DestroyTargets();
        }
    }

    public void SpawnTargets()
    {
        if (totalTargets == 0 && GameManager.gameStarted)
        {

            float xMin = transform.position.x - spawnArea.x / 2;
            float yMin = transform.position.y - spawnArea.y / 2;
            float zMin = transform.position.z - spawnArea.z / 2;
            float xMax = transform.position.x + spawnArea.x / 2;
            float yMax = transform.position.y + spawnArea.y / 2;
            float zMax = transform.position.z + spawnArea.z / 2;
            int Level = GameManager.level + 2;
            if (GameManager.level < 5) { 
                for (int i = 0; i < Level ; i++)
                {
                    if (GameManager.TargetChoices == "Vertical")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = (zMin + zMax) / 2;

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                        if (MissedTargets.Count >= 5 && TargetCollision.HitTargets.Count >= 5)
                        {
                            //Debug.Log(MissedTargets.Count);
                            int RandomPosMissed = Random.Range(0, MissedTargets.Count);
                            int RandomPosHit = Random.Range(0, TargetCollision.HitTargets.Count);
                            posTarget = (MissedTargets[RandomPosMissed] + TargetCollision.HitTargets[RandomPosHit]) * 0.5f;
                        }

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);
                        targett.transform.localScale += scaleChange;
                        totalTargets++;
                        StartCoroutine(Countdown(targett));
                    }

                    if (GameManager.TargetChoices == "Horizontal")
                    {
                        float xRandom = (xMin + xMax) / 2;
                        float yRandom = (yMin + yMax) / 2;
                        float zRandom = Random.Range(zMin, zMax);
                    
                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);


                        if (MissedTargets.Count >= 5 && TargetCollision.HitTargets.Count >= 5)
                        {
                            //Debug.Log(MissedTargets.Count);
                            int RandomPosMissed = Random.Range(0, MissedTargets.Count);
                            int RandomPosHit = Random.Range(0, TargetCollision.HitTargets.Count);
                            posTarget = (MissedTargets[RandomPosMissed] + TargetCollision.HitTargets[RandomPosHit]) * 0.5f;
                        }

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);
                        targett.transform.localScale += scaleChange;
                        totalTargets++;
                        StartCoroutine(Countdown(targett));
                    }

                    if(GameManager.TargetChoices == "Left")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = Random.Range(zMin / 2 + zMax / 2, zMax );

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                        if (MissedTargets.Count >= 5 && TargetCollision.HitTargets.Count>=5)
                        {
                            //Debug.Log(MissedTargets.Count);
                            int RandomPosMissed = Random.Range(0, MissedTargets.Count);
                            int RandomPosHit = Random.Range(0, TargetCollision.HitTargets.Count);
                            posTarget = (MissedTargets[RandomPosMissed] + TargetCollision.HitTargets[RandomPosHit]) * 0.5f;
                        }

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);
                        totalTargets++;
                        StartCoroutine(Countdown(targett));
                        
                    }
                    if (GameManager.TargetChoices == "Both")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = Random.Range(zMin, zMax);

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                        if (MissedTargets.Count >= 5 && TargetCollision.HitTargets.Count >= 5)
                        {
                            //Debug.Log(MissedTargets.Count);
                            int RandomPosMissed = Random.Range(0, MissedTargets.Count);
                            int RandomPosHit = Random.Range(0, TargetCollision.HitTargets.Count);
                            posTarget = (MissedTargets[RandomPosMissed] + TargetCollision.HitTargets[RandomPosHit]) * 0.5f;
                        }

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);
                      
                        totalTargets++;
                        StartCoroutine(Countdown(targett));
                    }
                    if (GameManager.TargetChoices == "Right")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = Random.Range(zMin , zMin / 2 + zMax / 2);

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);


                        if (MissedTargets.Count >= 5 && TargetCollision.HitTargets.Count >= 5)
                        {
                            //Debug.Log(MissedTargets.Count);
                            int RandomPosMissed = Random.Range(0, MissedTargets.Count);
                            int RandomPosHit = Random.Range(0, TargetCollision.HitTargets.Count);
                            posTarget = (MissedTargets[RandomPosMissed] + TargetCollision.HitTargets[RandomPosHit]) * 0.5f;
                        }

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);

                        totalTargets++;
                        StartCoroutine(Countdown(targett));
                    }
                }
            }     
            else
            {
                for (int i = 0; i < 7; i++)
                {
                    if (GameManager.TargetChoices == "Vertical")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = (zMin + zMax) / 2;

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);

                        totalTargets++;
                        targett.transform.localScale += scaleChange;
                        StartCoroutine(Countdown(targett));
        
                    }

                    if (GameManager.TargetChoices == "Horizontal")
                    {
                        float xRandom = (xMin + xMax) / 2;
                        float yRandom = (yMin + yMax) / 2;
                        float zRandom = Random.Range(zMin, zMax);

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);

                        totalTargets++;
                        targett.transform.localScale += scaleChange;
                        StartCoroutine(Countdown(targett));
                        
                    }

                    if (GameManager.TargetChoices == "Left")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = Random.Range(zMin / 2 + zMax / 2, zMax);

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                        if (MissedTargets.Count >= 5 && TargetCollision.HitTargets.Count >= 5)
                        {
                            //Debug.Log(MissedTargets.Count);
                            int RandomPosMissed = Random.Range(0, MissedTargets.Count);
                            int RandomPosHit = Random.Range(0, TargetCollision.HitTargets.Count);
                            posTarget = (MissedTargets[RandomPosMissed] + TargetCollision.HitTargets[RandomPosHit]) * 0.5f;
                        }

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);

                        totalTargets++;
                        StartCoroutine(Countdown(targett));
                    }
                    if (GameManager.TargetChoices == "Both")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = Random.Range(zMin, zMax);

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                        if (MissedTargets.Count >= 5 && TargetCollision.HitTargets.Count >= 5)
                        {
                            //Debug.Log(MissedTargets.Count);
                            int RandomPosMissed = Random.Range(0, MissedTargets.Count);
                            int RandomPosHit = Random.Range(0, TargetCollision.HitTargets.Count);
                            posTarget = (MissedTargets[RandomPosMissed] + TargetCollision.HitTargets[RandomPosHit]) * 0.5f;
                        }

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);

                        totalTargets++;
                        StartCoroutine(Countdown(targett));
                    }
                    if (GameManager.TargetChoices == "Right")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = Random.Range(zMin, zMin / 2 + zMax / 2);

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                        if (MissedTargets.Count >= 5 && TargetCollision.HitTargets.Count >= 5)
                        {
                            //Debug.Log(MissedTargets.Count);
                            int RandomPosMissed = Random.Range(0, MissedTargets.Count);
                            int RandomPosHit = Random.Range(0, TargetCollision.HitTargets.Count);
                            posTarget = (MissedTargets[RandomPosMissed] + TargetCollision.HitTargets[RandomPosHit]) * 0.5f;
                        }

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);

                        totalTargets++;
                        StartCoroutine(Countdown(targett));
                    }
                }
            }
        }
    }

    IEnumerator Countdown(GameObject target)
    {
        yield return new WaitForSeconds(5f);
        if(target != null)
        {
            totalTargets--;
            MissedTargets.Add(target.transform.position);

            var document = new BsonDocument { { "x", target.gameObject.transform.position.x},{ "y", target.gameObject.transform.position.y},
                { "z", target.gameObject.transform.position.z },{"hit", "false" } };
            GameManager.collection.InsertOne(document);

            TargetMovement targetMovement = target.GetComponent<TargetMovement>();
            targetMovement.enabled = false;

            Rigidbody targetRigidBody = target.GetComponent<Rigidbody>();
            targetRigidBody.isKinematic = false;

            Collider targetCollider = target.GetComponent<Collider>();
            targetCollider.enabled = false;
            
            Destroy(target, 1f);
            
        }
    }

    static public void DestroyTargets()
    {
        totalTargets = 0;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 0; i < targets.Length; i++)
        {
            var document = new BsonDocument { { "x", targets[i].gameObject.transform.position.x},{ "y", targets[i].gameObject.transform.position.y},
                { "z", targets[i].gameObject.transform.position.z },{"hit", "false" } };
            GameManager.collection.InsertOne(document);

            TargetMovement targetMovement = targets[i].GetComponent<TargetMovement>();
            targetMovement.enabled = false;

            Rigidbody targetRigidBody = targets[i].GetComponent<Rigidbody>();
            targetRigidBody.isKinematic = false;

            Collider targetCollider = targets[i].GetComponent<Collider>();
            targetCollider.enabled = false;

            Destroy(targets[i], 1f);
        }
       
    }

    private static Transform GetTransform(GameObject[] targets, int i)
    {
        return targets[i].transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}
*/
