namespace MongoDataAccess.Util;
public class GameLogAggregation
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public double SummedBets { get; set; }
    public double MaxBet { get; set; }
    public double AverageBet { get; set; }
    public int BetCount { get; set; }
}
