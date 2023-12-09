using AdventOfCode2023.DaySeven;
using FluentAssertions;

namespace AdventOfCode2023.UnitTests;

public class DaySevenTest
{
    [Fact]
    public void HandType_ShouldBeHighCard()
    {
        var hand = new Hand(new[]
        {
            new Card('2'),
            new Card('3'),
            new Card('5'),
            new Card('9'),
            new Card('K')
        });

        hand.Type.Should().Be(HandType.HighCard);
    }
    
    [Fact]
    public void HandType_ShouldBeOnePair()
    {
        var hand = new Hand(new[]
        {
            new Card('2'),
            new Card('2'),
            new Card('5'),
            new Card('9'),
            new Card('K')
        });

        hand.Type.Should().Be(HandType.OnePair);
    }
    
    [Fact]
    public void HandType_ShouldBeTwoPairs()
    {
        var hand = new Hand(new[]
        {
            new Card('2'),
            new Card('2'),
            new Card('5'),
            new Card('5'),
            new Card('K')
        });

        hand.Type.Should().Be(HandType.TwoPairs);
    }
    
    [Fact]
    public void HandType_ShouldBeThreeOfAKind()
    {
        var hand = new Hand(new[]
        {
            new Card('2'),
            new Card('2'),
            new Card('2'),
            new Card('5'),
            new Card('K')
        });

        hand.Type.Should().Be(HandType.ThreeOfAKind);
    }
    
    [Fact]
    public void HandType_ShouldBeFullHouse()
    {
        var hand = new Hand(new[]
        {
            new Card('2'),
            new Card('2'),
            new Card('2'),
            new Card('5'),
            new Card('5')
        });

        hand.Type.Should().Be(HandType.FullHouse);
    }
    
    [Fact]
    public void HandType_ShouldBeFourOfAKind()
    {
        var hand = new Hand(new[]
        {
            new Card('2'),
            new Card('2'),
            new Card('2'),
            new Card('2'),
            new Card('5')
        });

        hand.Type.Should().Be(HandType.FourOfAKind);
    }
    
    [Fact]
    public void HandType_ShouldBeFiveOfAKind()
    {
        var hand = new Hand(new[]
        {
            new Card('2'),
            new Card('2'),
            new Card('2'),
            new Card('2'),
            new Card('2')
        });

        hand.Type.Should().Be(HandType.FiveOfAKind);
    }
    
    [Fact]
    public void HandCompareTo_ShouldBeEqual_WhenTwoHandsAreEqual()
    {
        var hand1 = new Hand(new[]
        {
            new Card('2'),
            new Card('3'),
            new Card('5'),
            new Card('9'),
            new Card('K')
        });
        
        var hand2 = new Hand(new[]
        {
            new Card('2'),
            new Card('3'),
            new Card('5'),
            new Card('9'),
            new Card('K')
        });

        hand1.CompareTo(hand2).Should().Be(0);
    }
    
    [Fact]
    public void HandCompareTo_ShouldBeLessThan_WhenLastCardIsLessThanSecondHand()
    {
        var hand1 = new Hand(new[]
        {
            new Card('2'),
            new Card('3'),
            new Card('5'),
            new Card('9'),
            new Card('K')
        });
        
        var hand2 = new Hand(new[]
        {
            new Card('2'),
            new Card('3'),
            new Card('5'),
            new Card('9'),
            new Card('A')
        });

        hand1.CompareTo(hand2).Should().BeNegative();
    }
    
    [Fact]
    public void HandCompareTo_ShouldBeGreaterThan_WhenLastCardIsGreaterThanSecondHand()
    {
        var hand1 = new Hand(new[]
        {
            new Card('2'),
            new Card('3'),
            new Card('5'),
            new Card('9'),
            new Card('A')
        });
        
        var hand2 = new Hand(new[]
        {
            new Card('2'),
            new Card('3'),
            new Card('5'),
            new Card('9'),
            new Card('K')
        });

        hand1.CompareTo(hand2).Should().BePositive();
    }
    
    [Fact]
    public void HandCompareTo_ShouldBeLessThan_WhenFirstCardIsLessThanSecondHand()
    {
        var hand1 = new Hand(new[]
        {
            new Card('2'),
            new Card('3'),
            new Card('5'),
            new Card('8'),
            new Card('A')
        });
        
        var hand2 = new Hand(new[]
        {
            new Card('9'),
            new Card('3'),
            new Card('5'),
            new Card('8'),
            new Card('K')
        });

        hand1.CompareTo(hand2).Should().BeNegative();
    }
    
    [Fact]
    public void HandCompareTo_ShouldBeGreaterThan_WhenFirstCardIsGreaterThanSecondHand()
    {
        var hand1 = new Hand(new[]
        {
            new Card('T'),
            new Card('3'),
            new Card('5'),
            new Card('8'),
            new Card('K')
        });
        
        var hand2 = new Hand(new[]
        {
            new Card('9'),
            new Card('3'),
            new Card('5'),
            new Card('8'),
            new Card('A')
        });

        hand1.CompareTo(hand2).Should().BePositive();
    }

    [Fact]
    public void PartOne()
    {
        const string input = """
                             32T3K 765
                             T55J5 684
                             KK677 28
                             KTJJT 220
                             QQQJA 483
                             """;
        
        var result = Solution.PartOne(input.Split(Environment.NewLine));
        
        result.Should().Be(6440);
    }
}