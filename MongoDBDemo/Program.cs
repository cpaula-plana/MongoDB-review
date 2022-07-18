using MongoDataAccess.DataAccess;
using MongoDataAccess.Util;
using System.Diagnostics;

int insertQty = 1000;
var t = new Stopwatch();
TimeSpan creationTime;
TimeSpan readTime;
TimeSpan deleteTime;
TimeSpan aggregatingTime;
TimeSpan updateTime;

GameLogDataAccess db = new GameLogDataAccess();

//await db.DropDatabase();
Console.WriteLine("Running MongoDB Benchmark");
for (int i = 0; i < 5; i++)
{
    t.Start();
    for (int j = 0; j < insertQty; j++)
    {
        await db.CreateGameLog(GameLogModelBuilder.CreateNew());
    }
    t.Stop();
    creationTime = t.Elapsed;
    
    t.Restart();
    var gameLogs = await db.GetAll();
    t.Stop();
    readTime = t.Elapsed;

    t.Restart();
    foreach (var gameLog in gameLogs)
    {
        await db.UpdateGameLog(gameLog);
    }
    t.Stop();
    updateTime = t.Elapsed;

    t.Restart();
    var aggregation = db.AggregateGameBets();
    t.Stop();
    aggregatingTime = t.Elapsed;

    gameLogs = await db.GetAll();

    t.Restart();
    if (i != 4)
        foreach (var gameLog in gameLogs)
        {
            await db.DeleteGameLog(gameLog);
        }
    t.Stop();
    deleteTime = t.Elapsed;

    Console.Write($"======== {insertQty} records ========\n" +
        $"Insert Time:\t\tTotal - {creationTime}\tAverage - {creationTime/insertQty}\n" +
        $"Read Time:\t\tTotal - {readTime}\n" +
        $"Update Time:\t\tTotal - {updateTime}\tAverage - {updateTime / insertQty}\n" +
        $"Aggregate Time:\t\tTotal - {aggregatingTime}\n" +
        $"Delete Time:\t\tTotal - {deleteTime}\tAverage - {deleteTime / insertQty}\n");
    
    insertQty *= 10;
}

Console.WriteLine("======== Done ========");
Console.ReadKey();
