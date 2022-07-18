using MongoDataAccess.Models;
using MongoDataAccess.Util;
using MongoDB.Driver;

namespace MongoDataAccess.DataAccess;
public class GameLogDataAccess
{
    private const string Conn = "mongodb://127.0.0.1:27017";
    private const string DatabaseName = "gamesdb";
    private const string GameLogCollection = "gameslog";
    private static MongoClient? client;

    public GameLogDataAccess()
    {
        client = new MongoClient(Conn);
    }

    private IMongoCollection<T> ConnectToMongo<T>(in string collection)
    {
        var db = client.GetDatabase(DatabaseName);
        return db.GetCollection<T>(collection);
    }

    public async Task<List<GameLogModel>> GetAll()
    {
        var gamesLogCollection = ConnectToMongo<GameLogModel>(GameLogCollection);
        var results = await gamesLogCollection.FindAsync(_ => true);
        return results.ToList();
    }

    public async Task<List<GameLogModel>> GetAllByUserId(int userId)
    {
        var gamesLogCollection = ConnectToMongo<GameLogModel>(GameLogCollection);
        var results = await gamesLogCollection.FindAsync(g => g.UserId == userId);
        return results.ToList();
    }

    public List<GameLogAggregation> AggregateGameBets()
    {
        var gamesLogCollection = ConnectToMongo<GameLogModel>(GameLogCollection);
        return gamesLogCollection.Aggregate()
            .Group( g => g.UserId, 
                    game => new GameLogAggregation {
                                UserId = game.First().UserId,
                                UserName = game.First().UserName,
                                SummedBets = game.Sum(g => g.Bet),
                                AverageBet = game.Average(g => g.Bet),
                                MaxBet = game.Max(g => g.Bet),
                                BetCount = game.Count()
                    }).SortByDescending(g => g.SummedBets).ToList();
    }

    public Task CreateGameLog(GameLogModel gameLog)
    {
        var gamesLogCollection = ConnectToMongo<GameLogModel>(GameLogCollection);
        return gamesLogCollection.InsertOneAsync(gameLog);
    }

    public Task UpdateGameLog(GameLogModel gameLog)
    {
        var gamesLogCollection = ConnectToMongo<GameLogModel>(GameLogCollection);
        var filter = Builders<GameLogModel>.Filter.Eq("Id", gameLog.Id);
        return gamesLogCollection.ReplaceOneAsync(filter, gameLog, new ReplaceOptions { IsUpsert = true });
    }

    public Task DeleteGameLog(GameLogModel gameLog)
    {
        var gamesLogCollection = ConnectToMongo<GameLogModel>(GameLogCollection);
        return gamesLogCollection.DeleteOneAsync(g => g.Id == gameLog.Id);
    }
}
