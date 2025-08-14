using UnityEngine;
using System;
using System.Collections;

public class TestCases
{
    //Test case: Player Blackjack
    //public bool StartingRound() //this is testcase, put inside BlackjackGame
    //{
    //    playerHand = new Hand(); //initializing hand again?
    //    dealerHand = new Hand(); //initializing hand again?
    //    playerStood = false;

    //    for (int i = 0; i < 2; i++)
    //    {
    //        if (deck.CardsRemaining() < 1) return false;
    //        if (i == 0)
    //        {
    //            Card firstdrawn = deck.DrawCard(Suit.Hearts, Rank.Ace);

    //        }
    //        else
    //        {
    //            Card firstdrawn = deck.DrawCard(Suit.Hearts, Rank.King);
    //        }
    //        Debug.Log("Player has drawn " + firstdrawn);
    //        playerHand.AddCard(firstdrawn);
    //        if (deck.CardsRemaining() < 1) return false;
    //        Card seconddrawn = deck.Draw();
    //        Debug.Log("Dealer has drawn " + seconddrawn);
    //        dealerHand.AddCard(seconddrawn);
    //    }

    //    return true;
    //}

    //Test case: Hitting specific card (Hit 21)
    //public Card PlayerHitCard(Suit suit, Rank rank)
    //{
    //    if (playerStood) return null;
    //    if (playerHand.IsBust()) return null;
    //    if (deck.CardsRemaining() < 1) return null;

    //    Card drawn = deck.DrawCard(suit, rank);
    //    playerHand.AddCard(drawn);
    //    return drawn;
    //} //In gameManager, under function PlayerDrawAnimation(), change the line "Card? drawnCard = game.PlayerHitCard();" => Card? drawnCard = game.PlayerHitCard();


    //Test case: Dealer Blackjack 
    //public bool StartingRound() //this is testcase, put inside BlackjackGame
    //{
    //    playerHand = new Hand(); //initializing hand again?
    //    dealerHand = new Hand(); //initializing hand again?
    //    playerStood = false;

    //    for (int i = 0; i < 2; i++)
    //    {
    //        if (deck.CardsRemaining() < 1) return false;
    //        Card firstdrawn = deck.Draw();
    //        Debug.Log("Player has drawn " + firstdrawn);
    //        playerHand.AddCard(firstdrawn);
    //        if (deck.CardsRemaining() < 1) return false;
    //        if (i == 0)
    //        {
    //            Card seconddrawn = deck.DrawCard(Suit.Hearts, Rank.Ace);

    //        }
    //        else
    //        {
    //            Card seconddrawn = deck.DrawCard(Suit.Hearts, Rank.King);
    //        }
    //        Debug.Log("Dealer has drawn " + seconddrawn);
    //        dealerHand.AddCard(seconddrawn);
    //    }

    //    return true;
    //}


    //Test case: Both Blackjack
    //public bool StartingRound() //this is testcase, put inside BlackjackGame
    //{
    //    playerHand = new Hand(); //initializing hand again?
    //    dealerHand = new Hand(); //initializing hand again?
    //    playerStood = false;

    //    for (int i = 0; i < 2; i++)
    //    {
    //        if (deck.CardsRemaining() < 1) return false;
    //        if (i == 0)
    //        {
    //            Card firstdrawn = deck.DrawCard(Suit.Hearts, Rank.Ace);

    //        }
    //        else
    //        {
    //            Card firstdrawn = deck.DrawCard(Suit.Hearts, Rank.King);
    //        }
    //        Debug.Log("Player has drawn " + firstdrawn);
    //        playerHand.AddCard(firstdrawn);
    //        if (deck.CardsRemaining() < 1) return false;
    //        if (i == 0)
    //        {
    //            Card seconddrawn = deck.DrawCard(Suit.Spades, Rank.Ace);

    //        }
    //        else
    //        {
    //            Card seconddrawn = deck.DrawCard(Suit.Spades, Rank.King);
    //        }
    //        Debug.Log("Dealer has drawn " + seconddrawn);
    //        dealerHand.AddCard(seconddrawn);
    //    }

    //    return true;
    //}

    //Test case: Double Aces
    //public bool StartingRound() //this is testcase, put inside BlackjackGame
    //{
    //    playerHand = new Hand(); //initializing hand again?
    //    dealerHand = new Hand(); //initializing hand again?
    //    playerStood = false;

    //    for (int i = 0; i < 2; i++)
    //    {
    //        if (deck.CardsRemaining() < 1) return false;
    //        if (i == 0)
    //        {
    //            Card firstdrawn = deck.DrawCard(Suit.Hearts, Rank.Ace);

    //        }
    //        else
    //        {
    //            Card firstdrawn = deck.DrawCard(Suit.Spades, Rank.Ace);
    //        }
    //        Debug.Log("Player has drawn " + firstdrawn);
    //        playerHand.AddCard(firstdrawn);
    //        if (deck.CardsRemaining() < 1) return false;
    //        Card seconddrawn = deck.Draw();
    //        Debug.Log("Dealer has drawn " + seconddrawn);
    //        dealerHand.AddCard(seconddrawn);
    //    }

    //    return true;
    //}

    //Test case: Soft 17 
    //public bool StartingRound() //this is testcase, put inside BlackjackGame
    //{
    //    playerHand = new Hand(); //initializing hand again?
    //    dealerHand = new Hand(); //initializing hand again?
    //    playerStood = false;

    //    for (int i = 0; i < 2; i++)
    //    {
    //        if (deck.CardsRemaining() < 1) return false;
    //        Card firstdrawn = deck.Draw();
    //        Debug.Log("Player has drawn " + firstdrawn);
    //        playerHand.AddCard(firstdrawn);
    //        if (deck.CardsRemaining() < 1) return false;
    //        if (i == 0)
    //        {
    //            Card seconddrawn = deck.DrawCard(Suit.Hearts, Rank.Ace);

    //        }
    //        else
    //        {
    //            Card seconddrawn = deck.DrawCard(Suit.Hearts, Rank.Six);
    //        }
    //        Debug.Log("Dealer has drawn " + seconddrawn);
    //        dealerHand.AddCard(seconddrawn);
    //    }

    //    return true;
    //}


}
