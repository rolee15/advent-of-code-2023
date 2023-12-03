using AdventOfCode2023.DayThree;
using FluentAssertions;

namespace AdventOfCode2023.UnitTests;

public class DayThreeTest
{
    [Fact]
    public void PartOne_Example()
    {
        var input = new List<string>
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };
        
        var result = Solution.PartOne(input);

        result.Should().Be(4361);
    }
    
    [Fact]
    public void PartTwo_Example()
    {
        var input = new List<string>
        {
            "467..114..",
            "...*......",
            "..35..633.",
            "......#...",
            "617*......",
            ".....+.58.",
            "..592.....",
            "......755.",
            "...$.*....",
            ".664.598.."
        };
        
        var result = Solution.PartTwo(input);

        result.Should().Be(467835);
    }
}