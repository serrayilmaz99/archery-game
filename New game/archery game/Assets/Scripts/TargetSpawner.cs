
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetSpawner : MonoBehaviour
{
    public Vector3 spawnArea = new Vector3(1, 3, 4);
    public List<GameObject> Targets = new List<GameObject>();
    GameManager gameManager;
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
                SpawnTarget();
            }
        }
        if (!gameManager.gameStarted && gameManager.totalTargets > 0)
        {
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

        GameObject targett = Instantiate(targetPrefab, posTarget, Quaternion.Euler(0,90,0));

        Targets.Add(targett);

        targett.GetComponent<Collider>().enabled = true;
        targett.GetComponent<Rigidbody>().isKinematic = true;

        gameManager.totalTargets++;
    }

    public void DestroyTargets()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

        for (int i = 0; i < targets.Length; i++)
        {
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
