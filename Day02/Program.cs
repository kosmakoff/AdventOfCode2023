using static Common.Utils;

namespace Day02;

class Program
{
    private const int MaxRed = 12;
    private const int MaxGreen = 13;
    private const int MaxBlue = 14;
    
    static void Main()
    {
        PrintHeader("Day 02");

        var input = File.ReadAllLines("Input.txt");

        var answer1 = CalculateAnswer1(input);
        var answer2 = CalculateAnswer2(input);

        PrintAnswer("Answer 1", answer1);
        PrintAnswer("Answer 2", answer2);
    }

    private static int CalculateAnswer1(string[] input)
    {
        return input
            .Select(Game.Parse)
            .Where(game => game.Polls.All(poll => poll is { Red: <= MaxRed, Green: <= MaxGreen, Blue: <= MaxBlue }))
            .Sum(game => game.Id);
    }

    private static int CalculateAnswer2(string[] input)
    {
        return input
            .Select(Game.Parse)
            .Select(CalculatePower)
            .Sum();

        static int CalculatePower(Game game)
        {
            return game.Polls.Aggregate(
                (Red: 0, Green: 0, Blue: 0),
                (acc, poll) => (Math.Max(acc.Red, poll.Red),
                    Math.Max(acc.Green, poll.Green),
                    Math.Max(acc.Blue, poll.Blue)),
                acc => acc.Red * acc.Green * acc.Blue);
        }
    }
}