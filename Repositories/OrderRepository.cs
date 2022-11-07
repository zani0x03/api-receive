using apireceive.Models;
using apireceive.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace apireceive.Repositories;

public class OrderRepository:IOrderRepository
{
    private readonly IMongoDBConn _mongoDBConn;
    private readonly MongoClient _mongoConnection;

    private readonly String _collectionName = "order";


    public OrderRepository(IMongoDBConn mongoDBConn){
        this._mongoDBConn = mongoDBConn;
        _mongoConnection = _mongoDBConn.RetMongoDBClient();
    }

    public async Task OrderInsert(Order order){

        var collection = (_mongoConnection.GetDatabase("Cluster0")).GetCollection<Order>("order");
        await collection.InsertOneAsync(order);
    }
}

