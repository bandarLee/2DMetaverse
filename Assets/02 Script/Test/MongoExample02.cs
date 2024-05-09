using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MongoExample02 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string connectionString = "mongodb+srv://iron0944:qwas1234!@cluster0.czs65ij.mongodb.net/";
        MongoClient mongoClient = new MongoClient(connectionString);
        IMongoDatabase sampleDB = mongoClient.GetDatabase("sample_mflix");
        var movieCollection = sampleDB.GetCollection<BsonDocument>("movies");
        BsonDocument data = movieCollection.Find(new BsonDocument()).First() ;
        Debug.Log(data["plot"]);
        List<BsonDocument> datas = movieCollection.Find(new BsonDocument()).Limit(10).ToList();
        /*        foreach ( var item in datas)
                {
                    Debug.Log(item["title"]);
                }*/
        /*        BsonDocument filter = new BsonDocument();
                filter["year"] = 2002;
                List<BsonDocument> Movies2002 = movieCollection.Find(filter).Limit(5).ToList();
                foreach (var item in Movies2002)
                {
                    Debug.Log(item["year"]);
                }*/

        var filter2 = Builders<BsonDocument>.Filter.Gt("year", 2002);
        var data20022 = movieCollection.Find(filter2).Limit(5).ToList();
        /*        foreach (var item in data20022)
        {
            Debug.Log(item["year"]);
        }*/
    /*    var filter3 = Builders<BsonDocument>.Filter.Gte("year", 1992);
        var filter4 = Builders<BsonDocument>.Filter.Lte("year", 2002);
        var filterFinal = Builders<BsonDocument>.Filter.And(filter3, filter4);

        var result3 = movieCollection.Find(filterFinal).Limit(5).ToList();
        foreach (var item3 in result3)
        {
            Debug.Log(item3["year"]);
        }*/

   /*     var whereFilter = Builders<BsonDocument>.
            Filter.
            Where( d =>1992 <= d["year"] && d["year"] <= 2002)
            .Limit(5)
            .ToList();*/
   var finalData = movieCollection
            .Find(d => 1992 <= d["year"] && d["year"] <= 2002)
            .Limit(5)
            .ToList();
        foreach (var item4 in finalData)
        {
            Debug.Log(item4["year"]);
        }







    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
