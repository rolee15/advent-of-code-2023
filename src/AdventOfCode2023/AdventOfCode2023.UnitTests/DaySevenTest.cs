using AdventOfCode2023.DaySeven;
using FluentAssertions;

namespace AdventOfCode2023.UnitTests;

public class DaySevenTest
{
    [Fact]
    public void Card_GivenInvalidLabel_ShouldThrowArgumentException()
    {
        var act = () =>
        {
            _ = new Card { Label = '1' };
        };
        
        act.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void Hand_GivenInvalidNumberOfCards_ShouldThrowArgumentException()
    {
        var act = () =>
        {
            _ = new Hand<Card>([
                new Card { Label = '2' },
                new Card { Label = '3' },
                new Card { Label = '5' },
                new Card { Label = '9' }
            ]);
        };
        
        act.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void HandType_ShouldBeHighCard()
    {
        var hand = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '3' },
            new Card { Label = '5' },
            new Card { Label = '9' },
            new Card { Label = 'K' }
        ]);

        hand.Type.Should().Be(HandType.HighCard);
    }
    
    [Fact]
    public void HandType_ShouldBeOnePair()
    {
        var hand = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '2' },
            new Card { Label = '5' },
            new Card { Label = '9' },
            new Card { Label = 'K' }
        ]);

        hand.Type.Should().Be(HandType.OnePair);
    }
    
    [Fact]
    public void HandType_ShouldBeTwoPairs()
    {
        var hand = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '2' },
            new Card { Label = '5' },
            new Card { Label = '5' },
            new Card { Label = 'K' }
        ]);

        hand.Type.Should().Be(HandType.TwoPairs);
    }
    
    [Fact]
    public void HandType_ShouldBeThreeOfAKind()
    {
        var hand = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '2' },
            new Card { Label = '2' },
            new Card { Label = '5' },
            new Card { Label = 'K' }
        ]);

        hand.Type.Should().Be(HandType.ThreeOfAKind);
    }
    
    [Fact]
    public void HandType_ShouldBeFullHouse()
    {
        var hand = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '2' },
            new Card { Label = '2' },
            new Card { Label = '5' },
            new Card { Label = '5' }
        ]);

        hand.Type.Should().Be(HandType.FullHouse);
    }
    
    [Fact]
    public void HandType_ShouldBeFourOfAKind()
    {
        var hand = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '2' },
            new Card { Label = '2' },
            new Card { Label = '2' },
            new Card { Label = '5' }
        ]);

        hand.Type.Should().Be(HandType.FourOfAKind);
    }
    
    [Fact]
    public void HandType_ShouldBeFiveOfAKind()
    {
        var hand = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '2' },
            new Card { Label = '2' },
            new Card { Label = '2' },
            new Card { Label = '2' }
        ]);

        hand.Type.Should().Be(HandType.FiveOfAKind);
    }
    
    [Fact]
    public void HandCompareTo_ShouldBeEqual_WhenTwoHandsAreEqual()
    {
        var hand1 = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '3' },
            new Card { Label = '5' },
            new Card { Label = '9' },
            new Card { Label = 'K' }
        ]);
        
        var hand2 = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '3' },
            new Card { Label = '5' },
            new Card { Label = '9' },
            new Card { Label = 'K' }
        ]);

        hand1.CompareTo(hand2).Should().Be(0);
    }
    
    [Fact]
    public void HandCompareTo_ShouldBeLessThan_WhenLastCardIsLessThanSecondHand()
    {
        var hand1 = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '3' },
            new Card { Label = '5' },
            new Card { Label = '9' },
            new Card { Label = 'K' }
        ]);
        
        var hand2 = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '3' },
            new Card { Label = '5' },
            new Card { Label = '9' },
            new Card { Label = 'A' }
        ]);

        hand1.CompareTo(hand2).Should().BeNegative();
    }
    
    [Fact]
    public void HandCompareTo_ShouldBeGreaterThan_WhenLastCardIsGreaterThanSecondHand()
    {
        var hand1 = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '3' },
            new Card { Label = '5' },
            new Card { Label = '9' },
            new Card { Label = 'A' }
        ]);
        
        var hand2 = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '3' },
            new Card { Label = '5' },
            new Card { Label = '9' },
            new Card { Label = 'K' }
        ]);

        hand1.CompareTo(hand2).Should().BePositive();
    }
    
    [Fact]
    public void HandCompareTo_ShouldBeLessThan_WhenFirstCardIsLessThanSecondHand()
    {
        var hand1 = new Hand<Card>([
            new Card { Label = '2' },
            new Card { Label = '3' },
            new Card { Label = '5' },
            new Card { Label = '8' },
            new Card { Label = 'A' }
        ]);
        
        var hand2 = new Hand<Card>([
            new Card { Label = '9' },
            new Card { Label = '3' },
            new Card { Label = '5' },
            new Card { Label = '8' },
            new Card { Label = 'K' }
        ]);

        hand1.CompareTo(hand2).Should().BeNegative();
    }
    
    [Fact]
    public void HandCompareTo_ShouldBeGreaterThan_WhenFirstCardIsGreaterThanSecondHand()
    {
        var hand1 = new Hand<Card>([
            new Card { Label = 'T' },
            new Card { Label = '3' },
            new Card { Label = '5' },
            new Card { Label = '8' },
            new Card { Label = 'K' }
        ]);
        
        var hand2 = new Hand<Card>([
            new Card { Label = '9' },
            new Card { Label = '3' },
            new Card { Label = '5' },
            new Card { Label = '8' },
            new Card { Label = 'A' }
        ]);

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
    
    [Fact]
    public void HandJoker_GivenInvalidNumberOfCards_ShouldThrowArgumentException()
    {
        var act = () =>
        {
            _ = new Hand<CardJoker>([
                new CardJoker { Label = '2' },
                new CardJoker { Label = '3' },
                new CardJoker { Label = '5' },
                new CardJoker { Label = '9' }
            ]);
        };
        
        act.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void HandJokerType_GivenNoJokerAndHighCard_ShouldBeHighCard()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '9' },
            new CardJoker { Label = 'T' }
        ]);

        hand.Type.Should().Be(HandType.HighCard);
    }
    
    [Fact]
    public void HandJokerType_GivenNoJokerAndOnePair_ShouldBeOnePair()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '2' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '9' },
            new CardJoker { Label = 'T' }
        ]);

        hand.Type.Should().Be(HandType.OnePair);
    }
    
    [Fact]
    public void HandJokerType_GivenNoJokerAndTwoPairs_ShouldBeTwoPairs()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '2' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = 'T' }
        ]);

        hand.Type.Should().Be(HandType.TwoPairs);
    }
    
    [Fact]
    public void HandJokerType_GivenNoJokerAndThreeOfAKind_ShouldBeThreeOfAKind()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '2' },
            new CardJoker { Label = '2' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = 'T' }
        ]);

        hand.Type.Should().Be(HandType.ThreeOfAKind);
    }
    
    [Fact]
    public void HandJokerType_GivenNoJokerAndFullHouse_ShouldBeFullHouse()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '2' },
            new CardJoker { Label = '2' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '5' }
        ]);

        hand.Type.Should().Be(HandType.FullHouse);
    }
    
    [Fact]
    public void HandJokerType_GivenNoJokerAndFourOfAKind_ShouldBeFourOfAKind()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '2' },
            new CardJoker { Label = '2' },
            new CardJoker { Label = '2' },
            new CardJoker { Label = '5' }
        ]);

        hand.Type.Should().Be(HandType.FourOfAKind);
    }
    
    [Fact]
    public void HandJokerType_GivenOneJokerAndHighCard_ShouldBeOnePair()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '9' },
            new CardJoker { Label = 'J' }
        ]);

        hand.Type.Should().Be(HandType.OnePair);
    }
    
    [Fact]
    public void HandJokerType_GivenOneJokerAndOnePair_ShouldBeThreeOfAKind()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '4' },
            new CardJoker { Label = 'J' }
        ]);

        hand.Type.Should().Be(HandType.ThreeOfAKind);
    }
    
    [Fact]
    public void HandJokerType_GivenOneJokerAndTwoPairs_ShouldBeFullHouse()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '2' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = 'J' }
        ]);

        hand.Type.Should().Be(HandType.FullHouse);
    }
    
    [Fact]
    public void HandJokerType_GivenOneJokerAndThreeOfAKind_ShouldBeFourOfAKind()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = 'J' }
        ]);

        hand.Type.Should().Be(HandType.FourOfAKind);
    }
    
    [Fact]
    public void HandJokerType_GivenOneJokerAndFourOfAKind_ShouldBeFiveOfAKind()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '3' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = 'J' }
        ]);

        hand.Type.Should().Be(HandType.FiveOfAKind);
    }
    
    [Fact]
    public void HandJokerType_GivenTwoJokersAndHighCard_ShouldBeThreeOfAKind()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' }
        ]);

        hand.Type.Should().Be(HandType.ThreeOfAKind);
    }
    
    [Fact]
    public void HandJokerType_GivenTwoJokersAndOnePair_ShouldBeFourOfAKind()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' }
        ]);

        hand.Type.Should().Be(HandType.FourOfAKind);
    }
    
    [Fact]
    public void HandJokerType_GivenTwoJokersAndThreeOfAKind_ShouldBeFiveOfAKind()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '3' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' }
        ]);

        hand.Type.Should().Be(HandType.FiveOfAKind);
    }
    
    [Fact]
    public void HandJokerType_GivenThreeJokersAndHighCard_ShouldBeFourOfAKind()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' }
        ]);

        hand.Type.Should().Be(HandType.FourOfAKind);
    }
    
    [Fact]
    public void HandJokerType_GivenThreeJokersAndOnePair_ShouldBeFiveOfAKind()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '3' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' }
        ]);

        hand.Type.Should().Be(HandType.FiveOfAKind);
    }
    
    [Fact]
    public void HandJokerType_GivenFourJokersAndHighCard_ShouldBeFiveOfAKind()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = '3' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' }
        ]);

        hand.Type.Should().Be(HandType.FiveOfAKind);
    }
    
    [Fact]
    public void HandJokerType_GivenFiveJokers_ShouldBeFiveOfAKind()
    {
        var hand = new Hand<CardJoker>([
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' }
        ]);

        hand.Type.Should().Be(HandType.FiveOfAKind);
    }
    
    [Fact]
    public void HandJokerCompareTo_ShouldBeEqual_WhenTwoHandsAreEqual()
    {
        var hand1 = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '9' },
            new CardJoker { Label = 'K' }
        ]);
        
        var hand2 = new Hand<CardJoker>([
            new CardJoker { Label = '2' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '9' },
            new CardJoker { Label = 'K' }
        ]);

        hand1.CompareTo(hand2).Should().Be(0);
    }
    
    [Fact]
    public void HandJokerCompareTo_GivenNoJokerAndHighCard_ShouldBeGreaterThan_WhenSecondHandOnePair()
    {
        var hand1 = new Hand<CardJoker>([
            new CardJoker { Label = 'T' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '8' },
            new CardJoker { Label = 'K' }
        ]);
        
        var hand2 = new Hand<CardJoker>([
            new CardJoker { Label = '9' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '8' },
            new CardJoker { Label = 'A' }
        ]);

        hand1.CompareTo(hand2).Should().BePositive();
    }
    
    [Fact]
    public void HandJokerCompareTo_GivenOneJoker_ShouldBeEqual_WhenTwoHandsAreEqual()
    {
        var hand1 = new Hand<CardJoker>([
            new CardJoker { Label = 'J' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '9' },
            new CardJoker { Label = 'K' }
        ]);
        
        var hand2 = new Hand<CardJoker>([
            new CardJoker { Label = 'J' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '9' },
            new CardJoker { Label = 'K' }
        ]);

        hand1.CompareTo(hand2).Should().Be(0);
    }
    
    [Fact]
    public void HandJokerCompareTo_GivenOneJoker_ShouldBeLessThan_WhenLastCardIsLessThanSecondHand()
    {
        var hand1 = new Hand<CardJoker>([
            new CardJoker { Label = 'J' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '9' },
            new CardJoker { Label = 'A' }
        ]);
        
        var hand2 = new Hand<CardJoker>([
            new CardJoker { Label = 'J' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '9' },
            new CardJoker { Label = 'K' }
        ]);

        hand1.CompareTo(hand2).Should().BePositive();
    }
    
    [Fact]
    public void HandJokerCompareTo_GivenOneJoker_ShouldBeGreaterThan_WhenLastCardIsGreaterThanSecondHand()
    {
        var hand1 = new Hand<CardJoker>([
            new CardJoker { Label = 'J' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '9' },
            new CardJoker { Label = 'K' }
        ]);
        
        var hand2 = new Hand<CardJoker>([
            new CardJoker { Label = 'J' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '9' },
            new CardJoker { Label = 'A' }
        ]);

        hand1.CompareTo(hand2).Should().BeNegative();
    }
    
    [Fact]
    public void HandJokerCompareTo_GivenOneJokerAndHighCard_ShouldBeLessThan_WhenSecondHandOnePair()
    {
        var hand1 = new Hand<CardJoker>([
            new CardJoker { Label = 'J' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '8' },
            new CardJoker { Label = 'A' }
        ]);
        
        var hand2 = new Hand<CardJoker>([
            new CardJoker { Label = '3' },
            new CardJoker { Label = '3' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '8' },
            new CardJoker { Label = 'K' }
        ]);

        hand1.CompareTo(hand2).Should().BeNegative();
    }
    
    [Fact]
    public void HandJokerCompareTo_GivenThreeHandsWithJokers_ShouldBeGreaterThan_WhenSameTypes()
    {
        var hand1 = new Hand<CardJoker>([
            new CardJoker { Label = 'T' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = '5' }
        ]);
        
        var hand2 = new Hand<CardJoker>([
            new CardJoker { Label = 'K' },
            new CardJoker { Label = 'T' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'T' }
        ]);
        
        var hand3 = new Hand<CardJoker>([
            new CardJoker { Label = 'Q' },
            new CardJoker { Label = 'Q' },
            new CardJoker { Label = 'Q' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'A' }
        ]);

        hand1.CompareTo(hand2).Should().BeNegative();
        hand1.CompareTo(hand3).Should().BeNegative();
        hand2.CompareTo(hand3).Should().BePositive();
    }

    [Fact]
    public void HandJokerCompareTo_GivenThreeHands_ShouldBeGreater_WhenDifferentTypes()
    {
        
        var hand1 = new Hand<CardJoker>([
            new CardJoker { Label = 'J' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = '5' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = '5' }
        ]);
        
        var hand2 = new Hand<CardJoker>([
            new CardJoker { Label = 'K' },
            new CardJoker { Label = 'T' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'T' }
        ]);
        
        var hand3 = new Hand<CardJoker>([
            new CardJoker { Label = 'Q' },
            new CardJoker { Label = 'Q' },
            new CardJoker { Label = 'Q' },
            new CardJoker { Label = 'J' },
            new CardJoker { Label = 'Q' }
        ]);

        hand1.CompareTo(hand2).Should().BePositive();
        hand1.CompareTo(hand3).Should().BeNegative();
        hand2.CompareTo(hand3).Should().BeNegative();
    }
    
    [Fact]
    public void PartTwo()
    {
        const string input = """
                             32T3K 765
                             T55J5 684
                             KK677 28
                             KTJJT 220
                             QQQJA 483
                             """;
        
        var result = Solution.PartTwo(input.Split(Environment.NewLine));
        
        result.Should().Be(5905);
    }
}