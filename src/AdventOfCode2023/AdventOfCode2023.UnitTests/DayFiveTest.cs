using AdventOfCode2023.DayFive;
using FluentAssertions;

namespace AdventOfCode2023.UnitTests;

public class DayFiveTest
{
    private const string TestInput = 
        """
        seeds: 79 14 55 13

        seed-to-soil map:
        50 98 2
        52 50 48

        soil-to-fertilizer map:
        0 15 37
        37 52 2
        39 0 15

        fertilizer-to-water map:
        49 53 8
        0 11 42
        42 0 7
        57 7 4

        water-to-light map:
        88 18 7
        18 25 70

        light-to-temperature map:
        45 77 23
        81 45 19
        68 64 13

        temperature-to-humidity map:
        0 69 1
        1 0 69

        humidity-to-location map:
        60 56 37
        56 93 4
        """;
    
    [Fact]
    public void PartOneTest()
    {
        var input = TestInput.Split(Environment.NewLine);

        var result = Solution.PartOne(input);

        result.Should().Be(35);
    }
    
    [Fact]
    public void PartTwoTest()
    {
        var input = TestInput.Split(Environment.NewLine);

        var result = Solution.PartTwo(input);

        result.Should().Be(46);
    }

    [Fact]
    public void MapEntry_IsOverlapping()
    {
        var baseMapEntry = new MapEntry(10, 0, 10);
        var toTheLeftNotOverlapping = new MapEntry(0, 0, 10);
        var toTheLeftOverlapping = new MapEntry(0, 0, 11);
        var toTheRightNotOverlapping = new MapEntry(20, 0, 10);
        var toTheRightOverlapping = new MapEntry(19, 0, 10);
        var insideOverlapping = new MapEntry(12, 0, 5);
        
        baseMapEntry.IsOverlappingDestination(toTheLeftNotOverlapping).Should().BeFalse();
        baseMapEntry.IsOverlappingDestination(toTheLeftOverlapping).Should().BeTrue();
        baseMapEntry.IsOverlappingDestination(toTheRightNotOverlapping).Should().BeFalse();
        baseMapEntry.IsOverlappingDestination(toTheRightOverlapping).Should().BeTrue();
        baseMapEntry.IsOverlappingDestination(insideOverlapping).Should().BeTrue();
    }

    [Fact]
    public void Extensions_Contains()
    {
        var range1 = new IdRange(1, 3);
        var range2 = new IdRange(5, 5);
        var ranges = new[] {range1, range2};
        
        ranges.Contains(0).Should().BeFalse();
        ranges.Contains(1).Should().BeTrue();
        ranges.Contains(2).Should().BeTrue();
        ranges.Contains(3).Should().BeTrue();
        ranges.Contains(4).Should().BeFalse();
        ranges.Contains(5).Should().BeTrue();
        ranges.Contains(6).Should().BeTrue();
        ranges.Contains(7).Should().BeTrue();
        ranges.Contains(8).Should().BeTrue();
        ranges.Contains(9).Should().BeTrue();
        ranges.Contains(10).Should().BeFalse();
    }

    [Fact]
    public void Mapper_ReverseMap()
    {
        var chunks = Parser.ChunkByEmptyLines(TestInput.Split(Environment.NewLine));
        var reducer = new Reducer(chunks[1..chunks.Count]);
        
        var result = reducer.ReduceLocationId(46);
        
        result.Should().Be(82);
    }
}