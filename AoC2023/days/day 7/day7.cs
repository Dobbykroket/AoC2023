using System.Net;
using System.Security.Cryptography;
using AoC2023.tools;

namespace AoC2023.days.day_7;

public class Day7: Day
{
    private struct hand
    {
        public int[] cards;
        public int bet;

        public hand(int[] cards, int bet)
        {
            this.cards = cards;
            this.bet = bet;
        }
    }
    private int parseFaceCards(char card)
    {
        switch (card)
        {
            case 'T':
            {
                return 10;
            }
            case 'J':
            {
                return 11;
            }
            case 'Q':
            {
                return 12;
            }
            case 'K':
            {
                return 13;
            }
            case 'A':
            {
                return 14;
            }
            default:
            {
                return 0;
            }
        }
    }

    private int rankHandType(int[] inputhand)
    {
        int[] hand = (int[]) inputhand.Clone();
        Array.Sort(hand);
        List<int> cardCounts = new List<int>();
        int lastcheckedcard = -1;
        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i] == lastcheckedcard)
            {
                cardCounts[^1] += 1;
                continue;
            }

            lastcheckedcard = hand[i];
            cardCounts.Add(1);
        }

        cardCounts = cardCounts.OrderByDescending(i => i).ToList();

        if (cardCounts[0] == 5)
        {
            return 0;
        }

        if (cardCounts[0] == 4)
        {
            return 1;
        }

        if (cardCounts[0] == 3 && cardCounts[1] == 2)
        {
            return 2;
        }

        if (cardCounts[0] == 3)
        {
            return 3;
        }

        if (cardCounts[0] == 2 && cardCounts[1] == 2)
        {
            return 4;
        }

        if (cardCounts[0] == 2)
        {
            return 5;
        }

        if (cardCounts[0] == 1)
        {
            return 6;
        }

        return -1;
    }
    
    public Day7()
    {
        this.Directory = "day 7";
    }
    public override void Run()
    {
        StreamReader data = LoadData();
        string? line;
        List<hand>[] handsLists = {new List<hand>(), new List<hand>(), new List<hand>(), new List<hand>(), new List<hand>(), new List<hand>(), new List<hand>()}; 
        while ((line = data.ReadLine()) != null)
        {
            string[] input = line.Split(" ");
            List<int> cards = new List<int>();
            foreach (char c in input[0])
            {
                if (char.IsDigit(c))
                {
                    cards.Add(int.Parse(c.ToString()));
                }
                else
                {
                    cards.Add(parseFaceCards(c));
                }
            }
            int[] hand = cards.ToArray();
            handsLists[rankHandType(hand)].Add(new hand(hand, int.Parse(input[1])));
        }

        for (int i = 0; i < handsLists.Length; i++)
        {
            handsLists[i] = handsLists[i]
                .OrderByDescending(arr => arr.cards[0])
                .ThenByDescending(arr => arr.cards[1])
                .ThenByDescending(arr => arr.cards[2])
                .ThenByDescending(arr => arr.cards[3])
                .ThenByDescending(arr => arr.cards[4])
                .ToList();
        }

        List<hand> allHands = new List<hand>();
        foreach (List<hand> hands in handsLists)
        {
            allHands.AddRange(hands);
        }

        allHands.Reverse();
        int sum = 0;

        for (int i = 0; i < allHands.Count; i++)
        {
            sum += allHands[i].bet * (i + 1);
        }
        Console.WriteLine(sum);
    }
}