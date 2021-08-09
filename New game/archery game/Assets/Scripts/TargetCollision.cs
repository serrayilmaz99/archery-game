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

public class TargetCollision : MonoBehaviour
{
    [SerializeField]
    private TargetMovement targetMovement;
    [SerializeField]
    private GameObject impactParticles;

    private GameObject targetSpawnerGO;
    private TargetSpawner targetSpawner;

    private AudioSource impactSound;

    private PlayerData _playerData;
    MongoClient client = new MongoClient("mongodb+srv://admin-serra:serrayilmaz@mflix.ktzy1.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;
    // Start is called before the first frame update
   

        // Start is called before the first frame update
    void Start()
    {
        targetMovement = GetComponent<TargetMovement>();
        impactSound = GetComponent<AudioSource>();
        targetSpawnerGO= GameObject.Find("Target Spawner");
        targetSpawner= targetSpawnerGO.GetComponent<TargetSpawner>();

        database = client.GetDatabase("Archery");
        collection = database.GetCollection<BsonDocument>("Archery");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
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

            TargetSpawner.totalTargets--;

           //var document = new BsonDocument { { "x", gameObject.transform.position.x },{ "y", gameObject.transform.position.y },
           //    { "z", gameObject.transform.position.z },{"hit", "true" } };
           //collection.InsertOne(document);

           
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

}

