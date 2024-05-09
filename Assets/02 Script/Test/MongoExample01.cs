using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MongoExample01 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string connectionString = "mongodb+srv://iron0944:qwas1234!@cluster0.czs65ij.mongodb.net/";
        // - 프로토콜 : mongodb + srv
        // - 아이디, 비밀번호
        // - 주소 cluster~.net
        //1. 접속
        MongoClient mongoClient = new MongoClient(connectionString);
        //2. DB검색
        List<BsonDocument> dbList = mongoClient.ListDatabases().ToList();
        foreach (BsonDocument db in dbList)
        {
            Debug.Log(db["name"]);
        }

        IMongoDatabase sampleDB = mongoClient.GetDatabase("sample_mflix");

        List<string> collectionNames = sampleDB.ListCollectionNames().ToList();
        foreach(string name in collectionNames)
        {

            Debug.Log(name);
        }

        var movieCollection = sampleDB.GetCollection<BsonDocument>("movies");
        long count = movieCollection.CountDocuments(new BsonDocument());
        Debug.Log($"영화 개수 : {count}");


        var firstDoc = movieCollection.Find(new BsonDocument()).FirstOrDefault();
        Debug.Log(firstDoc["plot"]);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
