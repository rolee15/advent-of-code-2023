using AdventOfCode2023.DayTwo;
using FluentAssertions;

namespace AdventOfCode2023.UnitTests;

public class DayTwoTest
{
    [Fact]
    public void PartOne_Example()
    {
        string[] input =
        [
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        ];

        var result = Solution.PartOne(input);

        result.Should().Be(8);
    }


    [Fact]
    public void PartOne_All()
    {
        string[] input =
        [
            "Game 10: 1 red; 2 blue; 3 green",
            "Game 20: 1 blue; 3 green, 4 blue; 1 red, 1 green, 14 blue",
            "Game 30: 8 green, 6 blue, 10 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 40: 1 green, 12 red, 6 blue; 3 green, 6 red; 3 green, 5 blue, 1 red",
            "Game 50: 12 red, 14 blue, 13 green; 2 blue, 1 red, 2 green"
        ];

        var result = Solution.PartOne(input);

        result.Should().Be(150);
    }


    [Fact]
    public void PartOne_None()
    {
        string[] input =
        [
            "Game 10: 14 red; 2 blue; 3 green",
            "Game 20: 15 blue; 3 green, 4 blue; 1 red, 1 green, 14 blue",
            "Game 30: 18 green, 6 blue, 10 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 40: 14 green, 12 red, 6 blue; 3 green, 6 red; 3 green, 5 blue, 1 red",
            "Game 50: 120 red, 14 blue, 13 green; 2 blue, 1 red, 2 green"
        ];

        var result = Solution.PartOne(input);

        result.Should().Be(0);
    }


    [Fact]
    public void PartTwo_Example()
    {
        string[] input =
        [
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        ];

        var result = Solution.PartTwo(input);

        result.Should().Be(2286);
    }
}