using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;

namespace MongoDataAccess.Models;

public class GameLogModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public double Bet { get; set; }
    public double? Balance { get; set; }
    public bool Win { get; set; }
    public int GameType { get; set; }
    public DateTime DateTime { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

