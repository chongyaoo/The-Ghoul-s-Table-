using UnityEngine;

public class Card
{
    public Suit Suit { get; private set; }
    public Rank Rank { get; private set; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }   

    public int GetValue()
    {
        if (Rank >= Rank.Two && Rank <= Rank.Ten)
        {
            return (int)Rank;
        }
        else if (Rank == Rank.Jack || Rank == Rank.Queen || Rank == Rank.King) {
            return 10;
        }
        else
        {
            return 11;
        }
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}