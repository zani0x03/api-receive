using apireceive.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace apireceive.Repositories;

public class MongoDBConn:IMongoDBConn
{

    private MongoClient _mongoClient = null;

    public MongoDBConn(){
        if (_mongoClient == null){
            var settings = MongoClientSettings.FromConnectionString(Environment.GetEnvironmentVariable("MONGODB"));
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            _mongoClient = new MongoClient(settings);
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
        }
    }

    public MongoClient RetMongoDBClient (){
        return _mongoClient;
    }
}

