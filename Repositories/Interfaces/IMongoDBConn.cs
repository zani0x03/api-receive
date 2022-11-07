using MongoDB.Driver;

namespace apireceive.Repositories.Interfaces;

public interface IMongoDBConn
{
    MongoClient RetMongoDBClient();
}
