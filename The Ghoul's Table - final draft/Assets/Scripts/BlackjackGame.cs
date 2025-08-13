using System;
using System.ComponentModel;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;

public class BlackjackGame
{
    private readonly Deck deck;
    private Hand playerHand;
    private Hand dealerHand;
    private bool playerStood = false;
    private string firstdrawn;

    public Hand GetPlayerHand()
    {
        return playerHand;
    }

    public int GetPlayerHandCount()
    {
        return playerHand.NumCards();
    }

    public int GetDealerHandCount()
    {
        return dealerHand.NumCards();
    }

    public Hand GetDealerHand()
    {
        return dealerHand;
    }
    public BlackjackGame(int rngSeed = -1)
    {
        deck = new Deck(rngSeed);
        deck.Shuffle();
        playerHand = new Hand();
        dealerHand = new Hand();
    } // call this again for a new Blackjack game

    public bool StartRound()
    {
        playerHand = new Hand(); //initializing hand again?
        dealerHand = new Hand(); //initializing hand again?
        playerStood = false;

        for (int i = 0; i < 2; i++)
        {
            if (deck.CardsRemaining() < 1) return false;
            Card firstdrawn = deck.Draw();
            Debug.Log("Player has drawn " + firstdrawn);
            playerHand.AddCard(firstdrawn);
            if (deck.CardsRemaining() < 1) return false;
            Card seconddrawn = deck.Draw();
            Debug.Log("Dealer has drawn " + seconddrawn);
            dealerHand.AddCard(seconddrawn);
        }

        return true;
    }

    public Card DrawCard()
    {
        Card drawn = deck.Draw();
        Debug.Log("Testing draw " + drawn);
        return drawn;
    }
    public Card PlayerHit()
    {
        if (playerStood) return null;
        if (playerHand.IsBust()) return null;
        if (deck.CardsRemaining() < 1) return null;

        Card drawn = deck.Draw();
        playerHand.AddCard(drawn);
        return drawn;
    }

    public Card DealerHit()
    {
        if (dealerHand.IsBust()) return null;
        if (deck.CardsRemaining() < 1) return null;

        Card drawn = deck.Draw();
        dealerHand.AddCard(drawn);
        return drawn;
    }

    public void PlayerStand()
    {
        playerStood = true;
    }

    public bool DealerPlay()
    {
        bool dealerplayed = false;
        if (!playerStood)
            throw new InvalidOperationException("Player must stand before dealer plays.");
        (int total, bool isSoft) = dealerHand.GetValue();
        while (total < 17 || (total == 17 && isSoft)) //dealer hits on soft17
        {
            DealerHit();
            dealerplayed = true;
            (total, isSoft) = dealerHand.GetValue();
        }
        return dealerplayed;
    }

    public BlackjackOutcome DetermineOutcome()
    {
        if (playerHand.IsNaturalBlackjack() && dealerHand.IsNaturalBlackjack())
            return BlackjackOutcome.Push;
        else if (playerHand.IsNaturalBlackjack())
            return BlackjackOutcome.PlayerBlackjack;
        else if (dealerHand.IsNaturalBlackjack())
            return BlackjackOutcome.DealerBlackjack;
        (int playerTotal, bool playerIsSoft) = playerHand.GetValue();
        (int dealerTotal, bool dealerIsSoft) = dealerHand.GetValue();
        if (playerTotal > 21)
            return BlackjackOutcome.PlayerBust;
        else if (dealerTotal > 21)
            return BlackjackOutcome.DealerBust;
        else if (dealerTotal < playerTotal)
            return BlackjackOutcome.PlayerWin;
        else if (dealerTotal > playerTotal)
            return BlackjackOutcome.DealerWin;
        return BlackjackOutcome.Push;
    }
};