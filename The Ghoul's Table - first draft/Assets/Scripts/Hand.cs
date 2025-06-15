using System.Collections.Generic;
using UnityEngine;

public class Hand
{
    private List<Card> cards = new List<Card>();

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public int NumCards()
    {
        return cards.Count;
    }
    
    public List<Card> GetCards()
    {
        return cards;
    }

    public (int Total, bool IsSoft) GetValue()
    {
        int total = 0;
        int aceCount = 0;
        foreach (Card c in cards)
        {
            int v = c.GetValue();
            total += v;
            if (c.Rank == Rank.Ace)
                aceCount++;
        }
        bool isSoft = false;
        while (total > 21 && aceCount > 0)
        {
            total -= 10;
            aceCount--;
        }
        if (aceCount > 0)
            isSoft = true;
        return (total, isSoft);
    }

    public bool IsNaturalBlackjack()
    {
        if (cards.Count != 2) return false;
        var (total, _) = GetValue();
        return (total == 21);
    }

    public bool IsBust()
    {
        var (total, _) = GetValue();
        return total > 21;
    }

    public override string ToString()
    {
        string s = "";
        foreach (Card c in cards)
            s += c.ToString() + ", ";
        var (val, isSoft) = GetValue();
        s += $"(Total: {val}" + (isSoft ? " soft)" : ")");
        return s;
    }
}