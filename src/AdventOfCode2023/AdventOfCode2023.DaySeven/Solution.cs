namespace AdventOfCode2023.DaySeven;

public static class Solution
{
    public static int PartOne(IEnumerable<string> input)
    {
        return CalculateHandBidValues<Card>(input);
    }

    public static int PartTwo(IEnumerable<string> input)
    {
        return CalculateHandBidValues<CardJoker>(input);
    }

    private static int CalculateHandBidValues<T>(IEnumerable<string> input) where T : struct, ICard
    {
        var listOfCards = ParseInput<T>(input);
        return listOfCards.Select((pair, i) => (i + 1) * pair.Value).Sum();
    }

    private static SortedDictionary<Hand<T>, int> ParseInput<T>(IEnumerable<string> input) where T : struct, ICard
    {
        var dictionary = new SortedDictionary<Hand<T>, int>();
        foreach (var line in input)
        {
            var tokens = line.Split(' ');
            var cards = tokens[0].ToCharArray().Select(x => new T { Label = x }).ToList();
            dictionary.Add(new Hand<T>(cards), int.Parse(tokens[1]));
        }

        return dictionary;
    }
}

public readonly struct Hand<T> : IComparable<Hand<T>> where T : ICard
{
    public Hand(List<T> cards)
    {
        if (cards.Count != 5)
        {
            throw new ArgumentException("A hand must contain exactly five cards", nameof(cards));
        }

        Cards = cards;
        Type = CalculateType();
    }

    private List<T> Cards { get; }

    public HandType Type { get; }

    public int CompareTo(Hand<T> other)
    {
        if (Type != other.Type) return Type.CompareTo(other.Type);

        for (var i = 0; i < Cards.Count; i++)
        {
            if (Cards[i].Value != other.Cards[i].Value)
            {
                return Cards[i].Value.CompareTo(other.Cards[i].Value);
            }
        }

        return 0;
    }

    private HandType CalculateType()
    {
        var dictionary = new Dictionary<T, int>();
        foreach (var card in Cards.Where(card => !dictionary.TryAdd<T, int>(card, 1)))
        {
            dictionary[card]++;
        }

        var jokerCount = Cards.Count(x => x.IsJoker);
        return dictionary.Count switch
        {
            5 => jokerCount > 0 ? HandType.OnePair : HandType.HighCard,
            4 => jokerCount > 0 ? HandType.ThreeOfAKind : HandType.OnePair,
            3 when jokerCount == 0 => dictionary.Values.Any(x => x == 3) ? HandType.ThreeOfAKind : HandType.TwoPairs,
            3 when jokerCount == 1 => dictionary.Values.Any(x => x == 3) ? HandType.FourOfAKind : HandType.FullHouse,
            3 when jokerCount > 1 => HandType.FourOfAKind,
            2 when jokerCount == 0 => dictionary.Values.Any(x => x == 4) ? HandType.FourOfAKind : HandType.FullHouse,
            _ => HandType.FiveOfAKind
        };
    }
}

public enum HandType
{
    HighCard,
    OnePair,
    TwoPairs,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind
}

public interface ICard
{
    int Value { get; }
    bool IsJoker { get; }
    char Label { init; }
}

public readonly struct Card : ICard
{
    public int Value { get; private init; }
    public bool IsJoker => false;

    public char Label
    {
        init => Value = MapLabel(value);
    }

    private static int MapLabel(char label)
    {
        return label switch
        {
            '2' => 2,
            '3' => 3,
            '4' => 4,
            '5' => 5,
            '6' => 6,
            '7' => 7,
            '8' => 8,
            '9' => 9,
            'T' => 10,
            'J' => 11,
            'Q' => 12,
            'K' => 13,
            'A' => 14,
            _ => throw new ArgumentException($"Invalid label: {label}", nameof(label))
        };
    }
}

public readonly struct CardJoker : ICard
{
    public int Value => IsJoker ? 1 : Card.Value;
    public bool IsJoker => _label == 'J';

    public char Label
    {
        init
        {
            _label = value;
            Card = MapLabel(value);
        }
    }

    private readonly char _label;

    private Card Card { get; init; }

    private static Card MapLabel(char label)
    {
        return new Card { Label = label };
    }
}