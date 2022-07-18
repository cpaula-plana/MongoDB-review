using MongoDataAccess.Models;

namespace MongoDataAccess.Util;
public static class GameLogModelBuilder
{
    public static GameLogModel CreateNew()
    {
        var rnd = new Random();
        var user = User.RandomUser();
        var bets = new double[] { 50.00, 100.00, 200.00, 300.00, 400.00, 500.00, 1000.00, 1500.00 };
        var bet = bets[rnd.Next(0, bets.Length)];
        var gameType = rnd.Next(1, 5);
        var win = rnd.Next(0, 2) == 1;
        var dateTime = new DateTime(2022, 6, 20, 0, 0, 0);
        dateTime = dateTime.AddSeconds(rnd.Next(0, 60));
        dateTime = dateTime.AddMinutes(rnd.Next(0, 60));
        dateTime = dateTime.AddHours(rnd.Next(0, 24));
        dateTime = dateTime.AddDays(rnd.Next(0, 21));

        return new GameLogModel { UserId = user.Id, UserName = user.Name, Bet = bet, Win = win, GameType = gameType, DateTime = dateTime };
    }
}
