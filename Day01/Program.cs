using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Common.Utils;

namespace Day01;

class Program
{
    private static readonly Dictionary<string, int> WordsMap = new Dictionary<string, int>()
    {
        ["one"] = 1,
        ["1"] = 1,
        ["two"] = 2,
        ["2"] = 2,
        ["three"] = 3,
        ["3"] = 3,
        ["four"] = 4,
        ["4"] = 4,
        ["five"] = 5,
        ["5"] = 5,
        ["six"] = 6,
        ["6"] = 6,
        ["seven"] = 7,
        ["7"] = 7,
        ["eight"] = 8,
        ["8"] = 8,
        ["nine"] = 9,
        ["9"] = 9
    };

    static void Main()
    {
        PrintHeader("Day 01");

        var input = File.ReadAllLines("Input.txt");
        var answer1 = CalculateAnswer1(input);
        var answer2 = CalculateAnswer2(input);

        PrintAnswer("Answer 1", answer1);
        PrintAnswer("Answer 2", answer2);
    }

    static int CalculateAnswer1(IEnumerable<string> lines)
    {
        return lines
            .Select(GetNumber)
            .Sum();

        static int GetNumber(string text)
        {
            var firstNumberChar = text.First(char.IsNumber);
            var lastNumberChar = text.Last(char.IsNumber);
            var firstNumber = firstNumberChar - '0';
            var lastNumber = lastNumberChar - '0';
            return firstNumber * 10 + lastNumber;
        }
    }

    private static int CalculateAnswer2(IEnumerable<string> lines)
    {
        return lines
            .Select(ParseNumber)
            .Sum();
    }

    private static int ParseNumber(string text)
    {
        var words = WordsMap.Keys;
        var textSpan = text.AsSpan();
        var textSpanLength = textSpan.Length;

        int firstNumber = 0, lastNumber = 0;

        for (int i = 0; i < textSpanLength;i++)
        {
            foreach (var word in words)
            {
                if (word.Length > textSpanLength - i)
                {
                    continue;
                }
                var subtextSpan = textSpan[i..(i + word.Length)];
                if (word.AsSpan().SequenceEqual(subtextSpan))
                {
                    var number = WordsMap[word];
                    lastNumber = number;
                    firstNumber = firstNumber == 0 ? number : firstNumber;
                    break;
                }
            }
        }

        return firstNumber * 10 + lastNumber;
    }
}