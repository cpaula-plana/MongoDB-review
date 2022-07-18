namespace MongoDataAccess.Util;
public class User
{
    public string Name { get; set; }
    public int Id { get; set; }

    private static readonly Random rnd = new();

    private static readonly User luckyRonald = new() { Name = "LuckyRonald", Id = 777 };
    private static readonly User alpaca = new() { Name = "Alpaca", Id = 43 };
    private static readonly User llama = new() { Name = "Llama", Id = 42 };
    private static readonly User caio = new() { Name = "Caio", Id = 12 };
    private static readonly User vlad = new() { Name = "Vlad", Id = 2 };
    private static readonly User john = new() { Name = "John", Id = 71 };
    private static readonly User will = new() { Name = "Will", Id = 53 };
    private static readonly User adam = new() { Name = "Adam", Id = 23 };
    private static readonly User rosie = new() { Name = "Rosie", Id = 17 };
    private static readonly User alfie = new() { Name = "Alfie", Id = 7 };
    private static readonly User edward = new() { Name = "Edward", Id = 5 };
    private static readonly User jack = new() { Name = "Jack", Id = 11 };
    private static readonly User taylor = new() { Name = "Taylor", Id = 13 };

    public static User RandomUser()
    {
        var possibleUsers = new User[] {
            luckyRonald,
            alpaca,
            llama,
            caio,
            vlad,
            john,
            will,
            adam,
            rosie,
            alfie,
            edward,
            jack,
            taylor
        };

        return possibleUsers[rnd.Next(0, possibleUsers.Length)];
    }
}
