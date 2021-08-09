
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
/*
public class DatabaseAccess : MonoBehaviour
{
    MongoClient client = new MongoClient("mongodb+srv://admin-serra:serrayilmaz@mflix.ktzy1.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
    IMongoDatabase database;
    IMongoCollection<BsonDocument> collection;
    // Start is called before the first frame update
    void Start()
    {
        database = client.GetDatabase("Archery");
        collection = database.GetCollection<BsonDocument>("Archery");
        GetScoresFromDatabase();
    }

    public async void SaveScoreToDataBase(string userName, int score)
    {
        var document = new BsonDocument { { userName, score } };
        await collection.InsertOneAsync(document);


    }
    public async Task<List<HighScore>> GetScoresFromDatabase()
    {
        var allScoresTask = collection.FindAsync(new BsonDocument());
        var scoresAwaited = await allScoresTask;

        List<HighScore> highscores = new List<HighScore>();
        foreach (var score in scoresAwaited.ToList())
        {
            highscores.Add(Deserialize(score.ToString()));
        }

        return highscores;
    }
    private HighScore Deserialize(string rawJson)
    {
        var highScore = new HighScore();
        return highScore;

        //throw new System.NotImplementedException();
    }
}

public class HighScore
{
    public string UserName { get; set; }

    public int Score { get; set; }
}*/