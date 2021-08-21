
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetSpawner : MonoBehaviour
{
    public Vector3 spawnArea = new Vector3(1, 3, 4);
    public List<GameObject> Targets = new List<GameObject>(); // Keeps the list of targets that are active
    GameManager gameManager;
    public GameObject targetPrefab = null;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.gameStarted && gameManager.totalTargets == 0) // When game starts, spawn 3 targets randomly
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnTarget();
            }
        }
        if (!gameManager.gameStarted && gameManager.totalTargets > 0) // When game ends destroy the targets that are left
        {
            DestroyTargets();
            gameManager.totalTargets = 0;
        }
    }
    public Vector3 RandomCoords() // Returns the newly spawned object's position
    {
        float xMin = transform.position.x - spawnArea.x / 2;
        float yMin = transform.position.y - spawnArea.y / 2;
        float zMin = transform.position.z - spawnArea.z / 2;
        float xMax = transform.position.x + spawnArea.x / 2;
        float yMax = transform.position.y + spawnArea.y / 2;
        float zMax = transform.position.z + spawnArea.z / 2;

        if (gameManager.TargetChoices == "Vertical")
        {
            float xRandom = Random.Range(xMin, xMax);
            float yRandom = Random.Range(yMin, yMax);
            float zRandom = (zMin + zMax) / 2;

            return new Vector3(xRandom, yRandom, zRandom);
        }

        if (gameManager.TargetChoices == "Horizontal")
        {
            float xRandom = (xMin + xMax) / 2;
            float yRandom = (yMin + yMax) / 2;
            float zRandom = Random.Range(zMin, zMax);

            return new Vector3(xRandom, yRandom, zRandom);
        }

        if (gameManager.TargetChoices == "Left")
        {
            float xRandom = Random.Range(xMin, xMax);
            float yRandom = Random.Range(yMin, yMax);
            float zRandom = Random.Range(zMin / 2 + zMax / 2, zMax);

            return new Vector3(xRandom, yRandom, zRandom);
        }
        if (gameManager.TargetChoices == "Both")
        {
            float xRandom = Random.Range(xMin, xMax);
            float yRandom = Random.Range(yMin, yMax);
            float zRandom = Random.Range(zMin, zMax);

            return new Vector3(xRandom, yRandom, zRandom);
        }
        if (gameManager.TargetChoices == "Right")
        {
            float xRandom = Random.Range(xMin, xMax);
            float yRandom = Random.Range(yMin, yMax);
            float zRandom = Random.Range(zMin, zMin / 2 + zMax / 2);

            return new Vector3(xRandom, yRandom, zRandom);
        }
        return new Vector3(0, 0, 0);
    }
    public void SpawnTarget()
    {
        Vector3 posTarget = RandomCoords();

        GameObject targett = Instantiate(targetPrefab, posTarget, Quaternion.Euler(0,90,0));

        Targets.Add(targett); // Add the newly spawn target to the Targets list

        targett.GetComponent<Collider>().enabled = true;
        targett.GetComponent<Rigidbody>().isKinematic = true;

        gameManager.totalTargets++;
    }

    public void DestroyTargets() // Destroy the targets that are left when the game ends
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
