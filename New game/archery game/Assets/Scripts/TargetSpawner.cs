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
    [SerializeField]
    private Vector3 spawnArea = new Vector3(3, 3, 4);

    static public Vector3[] falseLeft;
    static public Vector3[] falseBoth;
    static public Vector3[] falseRight;

    private Vector3 scaleChange;

    static public Vector3 initSpawnerPos;

    [SerializeField]
    //private int targetsToSpawn = GameManager.level+3;

    private PlayerData playerData;

    public static int totalTargets;

    static public float scale = 1f;

    [SerializeField]
    private GameObject targetPrefab = null;

    MongoClient client = new MongoClient("mongodb+srv://admin-serra:serrayilmaz@mflix.ktzy1.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
    IMongoDatabase database;
    static IMongoCollection<BsonDocument> collection;

    void Start()
    {
        database = client.GetDatabase("Archery");
        collection = database.GetCollection<BsonDocument>("Archery");
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
        if (GameManager.score % 10 == 0 && GameManager.score != 0)
        {
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
        }
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

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);
                        targett.transform.localScale += scaleChange;
                      /*  if (GameManager.score % 10 == 0 && GameManager.score != 0)
                        {
                            // transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                        } */

                        totalTargets++;
                    }

                    if (GameManager.TargetChoices == "Horizontal")
                    {
                        float xRandom = (xMin + xMax) / 2;
                        float yRandom = (yMin + yMax) / 2;
                        float zRandom = Random.Range(zMin, zMax);
                    
                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);
                        targett.transform.localScale += scaleChange;
                      /*  if (GameManager.score % 10 == 0 && GameManager.score != 0)
                        {
                           // transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                        } */

                        totalTargets++;
                    }

                    if(GameManager.TargetChoices == "Left")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = Random.Range(zMin / 2 + zMax / 2, zMax );

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                       /* if (falseLeft.Length >= 5)
                        {
                            int RandomNum = Random.Range(0, falseLeft.Length);
                            posTarget = falseLeft[RandomNum];
                           // Debug.Log(posTarget);
                        } */

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);
                       /* if (GameManager.score % 10 == 0 && GameManager.score != 0)
                        {
                            transform.position = new Vector3(transform.position.x+0.7f, transform.position.y, transform.position.z);
                        } */

                        totalTargets++;
                    }
                    if (GameManager.TargetChoices == "Both")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = Random.Range(zMin, zMax);

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                      /*  if (falseBoth.Length >= 10)
                        {
                            int RandomNum = Random.Range(0, falseBoth.Length);
                            posTarget = falseBoth[RandomNum];
                        } */

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);
                       /* if (GameManager.score % 10 == 0 && GameManager.score != 0)
                        {
                            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                        } */

                        totalTargets++;
                    }
                    if (GameManager.TargetChoices == "Right")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = Random.Range(zMin , zMin / 2 + zMax / 2);

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                      /*  if (falseRight.Length>=10)
                        {
                            int RandomNum = Random.Range(0, falseRight.Length);
                            posTarget = falseRight[RandomNum];
                        } */

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);
                      /*  if (GameManager.score % 10 == 0 && GameManager.score != 0)
                        {
                            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                        } */

                        totalTargets++;
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
                        targett.transform.localScale += scaleChange;
                        /*  if (GameManager.score % 10 == 0 && GameManager.score != 0)
                          {
                              // transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                          } */

                        totalTargets++;
                    }

                    if (GameManager.TargetChoices == "Horizontal")
                    {
                        float xRandom = (xMin + xMax) / 2;
                        float yRandom = (yMin + yMax) / 2;
                        float zRandom = Random.Range(zMin, zMax);

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);
                        targett.transform.localScale += scaleChange;
                        /*  if (GameManager.score % 10 == 0 && GameManager.score != 0)
                          {
                             // transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                          } */

                        totalTargets++;
                    }

                    if (GameManager.TargetChoices == "Left")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = Random.Range(zMin / 2 + zMax / 2, zMax);

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                        /* if (falseLeft.Length >= 5)
                         {
                             int RandomNum = Random.Range(0, falseLeft.Length);
                             posTarget = falseLeft[RandomNum];
                            // Debug.Log(posTarget);
                         } */

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);
                        /* if (GameManager.score % 10 == 0 && GameManager.score != 0)
                         {
                             transform.position = new Vector3(transform.position.x+0.7f, transform.position.y, transform.position.z);
                         } */

                        totalTargets++;
                    }
                    if (GameManager.TargetChoices == "Both")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = Random.Range(zMin, zMax);

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                        /*  if (falseBoth.Length >= 10)
                          {
                              int RandomNum = Random.Range(0, falseBoth.Length);
                              posTarget = falseBoth[RandomNum];
                          } */

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);
                        /* if (GameManager.score % 10 == 0 && GameManager.score != 0)
                         {
                             transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                         } */

                        totalTargets++;
                    }
                    if (GameManager.TargetChoices == "Right")
                    {
                        float xRandom = Random.Range(xMin, xMax);
                        float yRandom = Random.Range(yMin, yMax);
                        float zRandom = Random.Range(zMin, zMin / 2 + zMax / 2);

                        Vector3 posTarget = new Vector3(xRandom, yRandom, zRandom);

                        /*  if (falseRight.Length>=10)
                          {
                              int RandomNum = Random.Range(0, falseRight.Length);
                              posTarget = falseRight[RandomNum];
                          } */

                        GameObject targett = Instantiate(targetPrefab, posTarget, targetPrefab.transform.rotation);
                        /*  if (GameManager.score % 10 == 0 && GameManager.score != 0)
                          {
                              transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                          } */

                        totalTargets++;
                    }
                }
            }
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
            collection.InsertOne(document);

            /*
            if (GameManager.TargetChoices == "Left")
            {
                Array.Resize(ref falseLeft, falseLeft.Length + 1);
                falseLeft[falseLeft.Length - 1] = new Vector3(targets[i].gameObject.transform.position.x,
                    targets[i].gameObject.transform.position.y, targets[i].gameObject.transform.position.z);
                //Debug.Log(falseLeft.Length);
            }
            if (GameManager.TargetChoices == "Both")
            {
                Array.Resize(ref falseBoth, falseBoth.Length + 1);
                falseBoth[falseBoth.Length - 1] = new Vector3(targets[i].gameObject.transform.position.x,
                    targets[i].gameObject.transform.position.y, targets[i].gameObject.transform.position.z);
            }
            if (GameManager.TargetChoices == "Right")
            {
                Array.Resize(ref falseRight, falseRight.Length + 1);
                falseRight[falseRight.Length - 1] = new Vector3(targets[i].gameObject.transform.position.x,
                    targets[i].gameObject.transform.position.y, targets[i].gameObject.transform.position.z);
            } */

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
