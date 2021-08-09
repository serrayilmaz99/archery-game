
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    public Vector3 spawnArea = new Vector3(3, 3, 4);

    [SerializeField]
    private int targetsToSpawn = 3;
    
    public static int totalTargets;

    [SerializeField]
    private GameObject targetPrefab;

    MongoClient client = new MongoClient("mongodb+srv://admin-serra:serrayilmaz@mflix.ktzy1.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;

    // Start is called before the first frame update
    void Start()
    {
        database = client.GetDatabase("Archery");
        collection = database.GetCollection<BsonDocument>("Archery-TCP");

    }

    // Update is called once per frame
    void Update()
    {
        if(totalTargets == 0 && GameManager.gameStarted)
        {
            SpawnTargets();
        }

        if(GameManager.gameStarted == false && totalTargets > 0)
        {
            DestroyTargets();
        }
    }

    public void SpawnTargets()
    {
        if(totalTargets == 0 && GameManager.gameStarted)
        {
            float xMin = transform.position.x - spawnArea.x / 2;
            float yMin = transform.position.y - spawnArea.y / 2;
            float zMin = transform.position.z - spawnArea.z / 2;
            float xMax = transform.position.x + spawnArea.x / 2;
            float yMax = transform.position.y + spawnArea.y / 2;
            float zMax = transform.position.z + spawnArea.z / 2;

            for(int i = 0; i < targetsToSpawn; i++)
            {
                if (GameManager.TargetChoices == "Left")
                {
                    float xRandom = Random.Range(xMin, xMax);
                    float yRandom = Random.Range(yMin, yMax);
                    float zRandom = Random.Range(zMin / 2 + zMax / 2, zMax);

                    Vector3 randomPosition = new Vector3(xRandom, yRandom, zRandom);
                    Instantiate(targetPrefab, randomPosition, targetPrefab.transform.rotation);

                    totalTargets++;
                }
                if (GameManager.TargetChoices == "Both")
                {
                    float xRandom = Random.Range(xMin, xMax);
                    float yRandom = Random.Range(yMin, yMax);
                    float zRandom = Random.Range(zMin, zMax);

                    Vector3 randomPosition = new Vector3(xRandom, yRandom, zRandom);
                    Instantiate(targetPrefab, randomPosition, targetPrefab.transform.rotation);

                    totalTargets++;
                }
                if (GameManager.TargetChoices == "Right")
                {
                    float xRandom = Random.Range(xMin, xMax);
                    float yRandom = Random.Range(yMin, yMax);
                    float zRandom = Random.Range(zMin, zMin / 2 + zMax / 2);

                    Vector3 randomPosition = new Vector3(xRandom, yRandom, zRandom);
                    Instantiate(targetPrefab, randomPosition, targetPrefab.transform.rotation);

                    totalTargets++;
                }
            }
        }
    }

    void DestroyTargets()
    {
        totalTargets = 0;
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 0; i < targets.Length; i++){

            var document = new BsonDocument { { "x", targets[i].gameObject.transform.position.x},{ "y", targets[i].gameObject.transform.position.y},
                { "z", targets[i].gameObject.transform.position.z },{"hit", "false" } };
            collection.InsertOne(document);

            TargetMovement targetMovement = targets[i].GetComponent<TargetMovement>();
            targetMovement.enabled = false;

            Rigidbody targetRigidBody = targets[i].GetComponent<Rigidbody>();
            targetRigidBody.isKinematic = false;

            Collider targetCollider = targets[i].GetComponent<Collider>();
            targetCollider.enabled = false;

            Destroy(targets[i], 1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, spawnArea);
    }
}
