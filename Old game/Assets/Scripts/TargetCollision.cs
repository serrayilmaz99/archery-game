using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Windows;
using System;
using MongoDB.Bson;
using MongoDB.Driver;

public class TargetCollision : MonoBehaviour
{
    [SerializeField]
    private TargetMovement targetMovement;
    [SerializeField]
    private GameObject impactParticles;

    MongoClient client = new MongoClient("mongodb+srv://admin-serra:serrayilmaz@mflix.ktzy1.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;

    private AudioSource impactSound;

    // Start is called before the first frame update
    void Start()
    {
        targetMovement = GetComponent<TargetMovement>();
        impactSound = GetComponent<AudioSource>();
        database = client.GetDatabase("Archery");
        collection = database.GetCollection<BsonDocument>("Archery-TCP");

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            
            impactSound.volume = other.relativeVelocity.normalized.magnitude;
            impactSound.Play();

            Instantiate(impactParticles, transform.position, impactParticles.transform.rotation);

            GameManager.score++;

            targetMovement.enabled = false;

            other.transform.parent = transform;

          //  other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            GetComponent<Rigidbody>().isKinematic = false;

            GetComponent<Collider>().enabled = false;

            TargetSpawner.totalTargets--;

            var document = new BsonDocument { { "x", gameObject.transform.position.x },{ "y", gameObject.transform.position.y },
                { "z", gameObject.transform.position.z },{"hit", "true" } };
            collection.InsertOne(document);



            Destroy(gameObject, 1f);
        }
    }
}
