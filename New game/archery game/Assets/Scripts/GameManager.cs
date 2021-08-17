
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Bson;
using MongoDB.Driver;

public class GameManager : MonoBehaviour
{
    //[SerializeField]
    public float gameLengthInSeconds = 20f;

    public List<Vector3> HitTargets = new List<Vector3>();

    public int totalTargets;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    public static GameObject gameStateUI;

    public bool gameStarted = false;

    public int score;
    public static float timer;

    public static Text gameStateText;
    public static Animator gameStateTextAnim;

    [SerializeField]
    private AudioClip startGameChime;

    [SerializeField]
    private AudioClip endGameChime;

    private AudioSource gameStateSounds;

    private TargetSpawner targetSpawner;

    public string TargetChoices;
    public List<GameObject> buttons;

    MongoClient client = new MongoClient("mongodb+srv://admin-serra:serrayilmaz@mflix.ktzy1.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
    IMongoDatabase database;
    public static IMongoCollection<BsonDocument> collection;

    // Start is called before the first frame update
    void Start()
    {
        gameStateUI = GameObject.FindGameObjectWithTag("TextUI");
        if (gameStarted == false)
        {
            gameStateText = gameStateUI.GetComponent<Text>();
            gameStateText.text = "Select Target Positions";

            gameStateTextAnim = gameStateUI.GetComponent<Animator>();
            gameStateTextAnim.SetBool("ShowText", true);
        }
        database = client.GetDatabase("Archery");
        collection = database.GetCollection<BsonDocument>("Archery");

        totalTargets = 0;
        targetSpawner = GameObject.Find("Target Spawner").GetComponent<TargetSpawner>();

    }

    public void PreStartGame()
    {
        foreach (GameObject item in buttons)
        {
            item.SetActive(false);
        }
        gameStateText = gameStateUI.GetComponent<Text>();
        gameStateText.text = "Hit Space to Play!";

        gameStateTextAnim = gameStateUI.GetComponent<Animator>();
        gameStateTextAnim.SetBool("ShowText", true);

        gameStateSounds = GetComponent<AudioSource>();

        timer = gameLengthInSeconds;

        UpdateScoreBoard();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameStarted == false && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (gameStarted)
        {
            timer -= Time.deltaTime;

            UpdateScoreBoard();
        }

        if (gameStarted && timer <= 0)
        {
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void UpdateScoreBoard()
    {
        scoreText.text = score + " Points";

        timerText.text = Mathf.RoundToInt(timer) + " Seconds";
    }
   
    private void StartGame()
    {
        score = 0;
        gameStarted = true;
        gameStateTextAnim.SetBool("ShowText", false);
        gameStateSounds.clip = startGameChime;
        gameStateSounds.Play();
    }

    private void EndGame()
    {
        gameStateText.text = "Game Over!\nPress Space to Restart";
        gameStateTextAnim.SetBool("ShowText", true);
        
        gameStarted = false;
        timer = gameLengthInSeconds;
        gameStateSounds.clip = endGameChime;
        gameStateSounds.Play();
        foreach (var item in HitTargets)
        {
            var document = new BsonDocument { { "x", item.x},{ "y", item.y},
                { "z", item.z } };
            collection.InsertOne(document); 
        }

        HitTargets.Clear();
    }
}
