using AdventOfCode2023.DayOne;
using FluentAssertions;

namespace AdventOfCode2023.UnitTests;

public class DayOneTest
{
    [Fact]
    public void PartOne_Example()
    {
        string[] input =
        [
            "1abc2",
            "pqr3stu8vwx",
            "a1b2c3d4e5f",
            "treb7uchet"
        ];

        var result = DayOne.DayOne.PartOne(input);

        result.Should().Be(142);
    }

    [Fact]
    public void PartOne_DigitsNextToEachOther()
    {
        string[] input =
        [
            "123",
            "456",
            "789"
        ];

        var result = DayOne.DayOne.PartOne(input);

        result.Should().Be(138);
    }

    [Fact]
    public void PartTwo_Example()
    {
        string[] input =
        [
            "two1nine",
            "eightwothree",
            "abcone2threexyz",
            "xtwone3four",
            "4nineeightseven2",
            "zoneight234",
            "7pqrstsixteen"
        ];

        var result = DayOne.DayOne.PartTwo(input);

        result.Should().Be(281);
    }
}