namespace AdventOfCode2023.DaySeven;

public static class Solution
{
    public static int PartOne(string[] input)
    {
        var listOfCards = ParseInput(input);
        var total = 0;
        for (var i = 1; i < listOfCards.Count; i++)
        {
            total += listOfCards.Values.ElementAt(i) * i;
        }
        return total;
    }

    private static SortedDictionary<Hand, int> ParseInput(string[] input)
    {
        var dictionary = new SortedDictionary<Hand, int>();
        foreach (var line in input)
        {
            var tokens = line.Split(' ');
            var cards = tokens[0].ToCharArray().Select(x => new Card(x)).ToArray();
            dictionary.Add(new Hand(cards), int.Parse(tokens[1]));
        }

        return dictionary;
    }

    public static int PartTwo(string[] input)
    {
        return 0;
    }
}

public readonly struct Hand : IComparable<Hand>
{
    public Hand(Card[] cards)
    {
        if (cards.Length != 5)
        {
            throw new ArgumentException("A hand must contain exactly five cards", nameof(cards));
        }

        Cards = cards;
        Type = CalculateType();
    }

    private Card[] Cards { get; }

    public HandType Type { get; }
    
    public int CompareTo(Hand other)
    {
        if (Type != other.Type) return Type.CompareTo(other.Type);
        
        for (var i = 0; i < Cards.Length; i++)
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
        var dictionary = new Dictionary<Card, int>();
        foreach (var card in Cards)
        {
            if (!dictionary.TryAdd(card, 1))
            {
                dictionary[card]++;
            }
        }

        return dictionary.Count switch
        {
            1 => HandType.FiveOfAKind,
            2 => dictionary.Values.Any(x => x == 4) ? HandType.FourOfAKind : HandType.FullHouse,
            3 => dictionary.Values.Any(x => x == 3) ? HandType.ThreeOfAKind : HandType.TwoPairs,
            4 => HandType.OnePair,
            _ => HandType.HighCard
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

public readonly struct Card(char label)
{
    public int Value { get; } = MapLabel(label);
    
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