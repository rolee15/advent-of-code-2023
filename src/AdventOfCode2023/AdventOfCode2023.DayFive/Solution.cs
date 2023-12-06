namespace AdventOfCode2023.DayFive;

public static class Solution
{
    public static long PartOne(string[] input)
    {
        var chunks = Parser.ChunkByEmptyLines(input);
        var seedIds = Parser.ParseSeedIds(chunks[0]);
        var reducer = new Reducer(chunks[1..chunks.Count]);

        return seedIds.Select(reducer.ReduceSeedId).Min();
    }

    public static long PartTwo(string[] input)
    {
        var chunks = Parser.ChunkByEmptyLines(input);
        var seedIdRanges = Parser.ParseSeedIdRanges(chunks[0]);
        var reducer = new Reducer(chunks[1..chunks.Count]);

        if (!reducer.IsReducingReversible())
            throw new YouAreFuckedException("Aww, you thought it will be that easy?");

        for (long i = 0; i < long.MaxValue; i++)
        {
            var seedId = reducer.ReduceLocationId(i);
            if (seedIdRanges.Contains(seedId)) return i;
        }

        return -1;
    }
}

public static class Parser
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

    public static List<IdRange> ParseSeedIdRanges(string[] input)
    {
        var rest = input[0].SkipWhile(IsNotDigit).ToArray();
        var ids = new string(rest).Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();

        var idRanges = new List<IdRange>(ids.Length / 2);
        for (var i = 0; i < ids.Length / 2; i++)
        {
            var start = ids[2 * i];
            var length = ids[2 * i + 1];
            idRanges.Add(new IdRange(start, length));
        }

        idRanges.Sort();

        return idRanges;
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

public class Reducer(IReadOnlyList<string[]> chunks)
{
    private readonly Mapper _seedToSoilMapper = new(chunks[0]);
    private readonly Mapper _soilToFertilizerMapper = new(chunks[1]);
    private readonly Mapper _fertilizerToWaterMapper = new(chunks[2]);
    private readonly Mapper _waterToLightMapper = new(chunks[3]);
    private readonly Mapper _lightToTemperatureMapper = new(chunks[4]);
    private readonly Mapper _temperatureToHumidityMapper = new(chunks[5]);
    private readonly Mapper _humidityToLocationMapper = new(chunks[6]);

    public long ReduceSeedId(long seedId)
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

    public long ReduceLocationId(long locationId)
    {
        var humidityId = _humidityToLocationMapper.ReverseMap(locationId);
        var temperatureId = _temperatureToHumidityMapper.ReverseMap(humidityId);
        var lightId = _lightToTemperatureMapper.ReverseMap(temperatureId);
        var waterId = _waterToLightMapper.ReverseMap(lightId);
        var fertilizerId = _fertilizerToWaterMapper.ReverseMap(waterId);
        var soilId = _soilToFertilizerMapper.ReverseMap(fertilizerId);
        var seedId = _seedToSoilMapper.ReverseMap(soilId);

        return seedId;
    }

    public bool IsReducingReversible()
    {
        return _seedToSoilMapper.IsMappingReversible() &&
               _soilToFertilizerMapper.IsMappingReversible() &&
               _fertilizerToWaterMapper.IsMappingReversible() &&
               _waterToLightMapper.IsMappingReversible() &&
               _lightToTemperatureMapper.IsMappingReversible() &&
               _temperatureToHumidityMapper.IsMappingReversible() &&
               _humidityToLocationMapper.IsMappingReversible();
    }
}

public class Mapper
{
    private readonly List<MapEntry> _mapEntries;

    public Mapper(string[] chunk)
    {
        _mapEntries = Parser.ParseMap(chunk);
        _mapEntries.Sort();
    }

    public bool IsMappingReversible()
    {
        for (var i = 0; i < _mapEntries.Count; i++)
        {
            for (var j = i + 1; j < _mapEntries.Count; j++)
            {
                if (_mapEntries[i].IsOverlappingDestination(_mapEntries[j])) return false;
            }
        }

        return true;
    }

    public long Map(long source)
    {
        var entry = _mapEntries.FirstOrDefault(e => e.SourceStart <= source && e.SourceEnd >= source);
        if (entry == default) return source;
        return entry.DestinationStart + (source - entry.SourceStart);
    }

    public long ReverseMap(long destination)
    {
        var entry = _mapEntries.FirstOrDefault(e =>
            e.DestinationStart <= destination && e.DestinationEnd >= destination);

        if (entry == default) return destination;
        return entry.SourceStart + (destination - entry.DestinationStart);
    }
}

public readonly record struct MapEntry(long DestinationStart, long DestinationEnd, long SourceStart, long SourceEnd)
    : IComparable<MapEntry>
{
    public MapEntry(long destination, long source, long length) :
        this(
            destination,
            destination + length - 1,
            source,
            source + length - 1)
    {
    }

    public bool IsOverlappingDestination(MapEntry other)
    {
        return DestinationStart <= other.DestinationEnd && DestinationEnd >= other.DestinationStart;
    }

    public int CompareTo(MapEntry other)
    {
        return DestinationStart.CompareTo(other.DestinationStart);
    }
}

public readonly struct IdRange(long start, long length) : IComparable<IdRange>
{
    public long Start { get; } = start;
    public long End { get; } = start + length - 1;

    public int CompareTo(IdRange other)
    {
        return Start.CompareTo(other.Start);
    }
}

public static class Extensions
{
    public static bool Contains(this IEnumerable<IdRange> range, long id)
    {
        return range.Any(r => r.Start <= id && r.End >= id);
    }
}

public class YouAreFuckedException(string? message) : Exception(message);