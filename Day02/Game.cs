namespace Day02;

public class Game
{
    public int Id { get; }
    public List<Poll> Polls { get; }

    public Game(int id, List<Poll> polls)
    {
        Id = id;
        Polls = polls;
    }

    public override string ToString()
    {
        return $"Game {Id}: {string.Join("; ", Polls)}";
    }

    public static Game Parse(string rawGame)
    {
        var rawGameSpan = rawGame.AsSpan();
        var colonIndex = rawGameSpan.IndexOf(':');
        var gameIdSpan = rawGameSpan[5..colonIndex];
        var gameId = int.Parse(gameIdSpan);

        var polls = new List<Poll>();
        
        var pollsSpan = rawGameSpan[(colonIndex + 1)..];
        while (true)
        {
            var semicolonIndex = pollsSpan.IndexOf(';');

            var pollSpan = semicolonIndex >= 0 ? pollsSpan[..semicolonIndex] : pollsSpan;
            var poll = Poll.Parse(pollSpan);
            polls.Add(poll);

            pollsSpan = semicolonIndex >= 0 ? pollsSpan[(semicolonIndex + 1)..] : ReadOnlySpan<char>.Empty;

            if (semicolonIndex == -1)
            {
                break;
            }
        }

        return new Game(gameId, polls);
    }

    public class Poll
    {
        public int Red { get; }
        public int Green { get; }
        public int Blue { get; }

        public override string ToString()
        {
            return $"{Red} red, {Green} green, {Blue} blue";
        }

        public Poll(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public static Poll Parse(ReadOnlySpan<char> pollSpan)
        {
            int red = 0, green = 0, blue = 0;
            
            while (true)
            {
                int comaIndex = pollSpan.IndexOf(',');

                var colorSpan = comaIndex >= 0 ? pollSpan[..comaIndex].Trim() : pollSpan.Trim();
                pollSpan = comaIndex >= 0 ? pollSpan[(comaIndex + 1)..] : ReadOnlySpan<char>.Empty;

                if (colorSpan.IsEmpty)
                {
                    break;
                }

                var spaceIndex = colorSpan.IndexOf(' ');
                var numberSpan = colorSpan[..spaceIndex];
                var colorNameSpan = colorSpan[(spaceIndex + 1)..];
                var number = int.Parse(numberSpan);

                switch (colorNameSpan)
                {
                    case "red":
                        red = number;
                        break;
                    case "green":
                        green = number;
                        break;
                    case "blue":
                        blue = number;
                        break;
                }
                
                if (comaIndex == -1)
                {
                    break;
                }
            }

            return new Poll(red, green, blue);
        }
    }
}