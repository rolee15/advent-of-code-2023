using System.Buffers;
using System.Collections.Frozen;

namespace AdventOfCode2023.DayOne;

public class Solution
{
    private static readonly SearchValues<char> PartOneSearchValues =
        SearchValues.Create(['1', '2', '3', '4', '5', '6', '7', '8', '9']);

    private static readonly string[] PartTwoSearchValues =
    [
        "1", "2", "3", "4", "5", "6", "7", "8", "9",
        "one", "two", "three",
        "four", "five", "six",
        "seven", "eight", "nine"
    ];

    private static readonly FrozenDictionary<string, char> NumberMap = new Dictionary<string, char>
    {
        { "1", '1' }, { "2", '2' }, { "3", '3' },
        { "4", '4' }, { "5", '5' }, { "6", '6' },
        { "7", '7' }, { "8", '8' }, { "9", '9' },
        { "one", '1' }, { "two", '2' }, { "three", '3' },
        { "four", '4' }, { "five", '5' }, { "six", '6' },
        { "seven", '7' }, { "eight", '8' }, { "nine", '9' }
    }.ToFrozenDictionary();

    public static int PartOne(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var lineSpan = line.AsSpan();
            var leftIndex = lineSpan.IndexOfAny(PartOneSearchValues);
            var rightIndex = lineSpan.LastIndexOfAny(PartOneSearchValues);

            sum += ConvertDigitsToNumber(lineSpan[leftIndex], lineSpan[rightIndex]);
        }

        return sum;
    }

    public static int PartTwo(string[] input)
    {
        var sum = 0;
        foreach (var line in input)
        {
            var leftNumberString = "";
            var leftIndex = int.MaxValue;
            var rightNumberString = "";
            var rightIndex = int.MinValue;
            foreach (var token in PartTwoSearchValues)
            {
                var leftIdx = line.IndexOf(token);
                if (leftIdx == -1) continue;

                if (leftIdx < leftIndex)
                {
                    leftNumberString = token;
                    leftIndex = leftIdx;
                }

                var rightIdx = line.LastIndexOf(token);
                if (rightIdx > rightIndex)
                {
                    rightNumberString = token;
                    rightIndex = rightIdx;
                }
            }

            var leftChar = NumberMap[leftNumberString];
            var rightChar = NumberMap[rightNumberString];
            sum += ConvertDigitsToNumber(leftChar, rightChar);
        }

        return sum;
    }

    private static int ConvertDigitsToNumber(char left, char right)
    {
        var s = string.Format("{0}{1}", left, right);
        return int.Parse(s);
    }
}