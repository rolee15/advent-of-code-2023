namespace AdventOfCode2023.DayFive;

public static class Solution
{
    public static long PartOne(string[] input)
    {
        var chunks = Parser.ChunkByEmptyLines(input);
        var seedIds = Parser.ParseSeedIds(chunks[0]);
        var reducer = new Reducer(chunks[1..chunks.Count]);

        return seedIds.Select(reducer.Reduce).Min();
    }

    public static long PartTwo(string[] input)
    {
        return 0;
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

        // Add the last chunk
        chunks.Add(input.AsSpan().Slice(start, input.Length - start).ToArray());

        return chunks;
    }

    public static long[] ParseSeedIds(string[] input)
    {
        var rest = input[0].SkipWhile(IsNotDigit).ToArray();
        var ids = new string(rest).Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();

        return ids;
    }

    public static List<MapEntry> ParseMap(string[] chunk)
    {
        return chunk.Skip(1).Select(ParseMapEntry).ToList();
    }

    private static MapEntry ParseMapEntry(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return new MapEntry(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2]));
    }

    private static bool IsDigit(char c)
    {
        return c is > '0' and < '9';
    }

    private static bool IsNotDigit(char c)
    {
        return !IsDigit(c);
    }
}

internal class Reducer(IReadOnlyList<string[]> chunks)
{
    private readonly Mapper _seedToSoilMapper = new(chunks[0]);
    private readonly Mapper _soilToFertilizerMapper = new(chunks[1]);
    private readonly Mapper _fertilizerToWaterMapper = new(chunks[2]);
    private readonly Mapper _waterToLightMapper = new(chunks[3]);
    private readonly Mapper _lightToTemperatureMapper = new(chunks[4]);
    private readonly Mapper _temperatureToHumidityMapper = new(chunks[5]);
    private readonly Mapper _humidityToLocationMapper = new(chunks[6]);

    public long Reduce(long seedId)
    {
        var soilId = _seedToSoilMapper.Map(seedId);
        var fertilizerId = _soilToFertilizerMapper.Map(soilId);
        var waterId = _fertilizerToWaterMapper.Map(fertilizerId);
        var lightId = _waterToLightMapper.Map(waterId);
        var temperatureId = _lightToTemperatureMapper.Map(lightId);
        var humidityId = _temperatureToHumidityMapper.Map(temperatureId);
        var locationId = _humidityToLocationMapper.Map(humidityId);
        return locationId;
    }
}

internal class Mapper(List<MapEntry> mapEntries)
{
    public Mapper(string[] chunk) : this(Parser.ParseMap(chunk))
    {
    }

    public long Map(long source)
    {
        var entry = mapEntries.FirstOrDefault(e => e.SourceStart <= source && e.SourceEnd >= source);
        if (entry == default) return source;
        return entry.DestinationStart + (source - entry.SourceStart);
    }
}

internal readonly record struct MapEntry(long DestinationStart, long DestinationEnd, long SourceStart, long SourceEnd)
{
    public MapEntry(long destination, long source, long length) : this(destination, destination + length, source,
        source + length)
    {
    }
}