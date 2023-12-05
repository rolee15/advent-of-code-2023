namespace AdventOfCode2023.DayFive;

public readonly record struct Id(long Value);

public static class Solution
{
    private static Dictionary<Id, Id> _seedToSoilMap;
    private static Dictionary<Id, Id> _soilToFertilizerMap;
    private static Dictionary<Id, Id> _fertilizerToWaterMap;
    private static Dictionary<Id, Id> _waterToLightMap;
    private static Dictionary<Id, Id> _lightToTemperatureMap;
    private static Dictionary<Id, Id> _temperatureToHumidityMap;
    private static Dictionary<Id, Id> _humidityToLocationMap;

    public static long PartOne(string[] input)
    {
        var chunks = Parser.ChunkByEmptyLines(input);
        var seedIds = Parser.ParseSeedIds(chunks[0]);

        InitMaps(chunks[1..chunks.Count]);

        return seedIds.Select(MapSeedToLocation).Min(x => x.Value);
    }

    public static long PartTwo(string[] input)
    {
        return 0;
    }
    
    private static Id MapSeedToLocation(Id id)
    {
        return id;
    }

    private static void InitMaps(IReadOnlyCollection<string[]> skip)
    {
        _seedToSoilMap = Parser.ParseMap(skip.ElementAt(0));
        _soilToFertilizerMap = Parser.ParseMap(skip.ElementAt(1));
        _fertilizerToWaterMap = Parser.ParseMap(skip.ElementAt(2));
        _waterToLightMap = Parser.ParseMap(skip.ElementAt(3));
        _lightToTemperatureMap = Parser.ParseMap(skip.ElementAt(4));
        _temperatureToHumidityMap = Parser.ParseMap(skip.ElementAt(5));
        _humidityToLocationMap = Parser.ParseMap(skip.ElementAt(6));
    }
}

internal static class Parser
{
    public static List<string[]> ChunkByEmptyLines(string[] input)
    {
        var chunks = new List<string[]>();
        var start = 0;
        for (var i = 0; i < input.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(input[i])) continue;
            
            chunks.Add(input.AsSpan().Slice(start, i - start).ToArray());
            start = i + 1;
        }

        return chunks;
    }
    
    public static IEnumerable<Id> ParseSeedIds(string[] input)
    {
        var rest = input[0].SkipWhile(IsNotDigit).ToArray();
        var ids = new string(rest).Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => new Id(long.Parse(x)))
            .ToArray();
        return ids;
    }

    private static bool IsDigit(char c)
    {
        return c is > '0' and < '9';
    }

    private static bool IsNotDigit(char c)
    {
        return !IsDigit(c);
    }

    public static Dictionary<Id, Id> ParseMap(string[] lines)
    {
        var map = new Dictionary<Id, Id>();
        for (var i = 1; i < lines.Length; i++)
        {
            var nums = lines[i].Split(' ').Select(long.Parse).ToArray();
            var dest = nums[0];
            var src = nums[1];
            var range = nums[2];
            
            
        }

        return map;
    }
}

public static class Extensions
{
    public static IEnumerable<Id> Map(this IEnumerable<Id> ids, Dictionary<long, Id> map)
    {
        foreach (var id in ids)
        {
            if (map.TryGetValue(id.Value, out var value))
            {
                yield return value;
            }
        }
    }
}