using System;
using System.Collections.Generic;

public class Deck
{
    private List<Card> cards;
    private Random rng;

    public Deck(int seed = -1)
    {
        rng = (seed < 0) ? new Random() : new Random(seed);
        InitializeDeck();
    }

    private void InitializeDeck()
    {
        cards = new List<Card>(52);
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                cards.Add(new Card(suit, rank));
            }
        }
    }

    public void Shuffle()
    {
        int n = cards.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            var temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }

    public Card Draw()
    {
        if (cards.Count == 0)
            throw new InvalidOperationException("Cannot draw from an empty deck.");

        Card top = cards[0];
        cards.RemoveAt(0);
        return top;
    }

    public int CardsRemaining()
    {
        return cards.Count;
    }
}