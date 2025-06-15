using System;
using Unity.VisualScripting;
using UnityEngine;

public class BlackjackGame
{
    private Deck deck;
    private Hand playerHand;
    private Hand dealerHand;
    private bool playerStood = false;

    public Hand GetPlayerHand()
    {
        return playerHand;
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
    }

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
    public bool PlayerHit()
    {
        if (playerStood) return false;
        if (playerHand.IsBust()) return false;
        if (deck.CardsRemaining() < 1) return false;

        playerHand.AddCard(deck.Draw());
        return true;
    }

    public void PlayerStand()
    {
        playerStood = true;
    }

    public void DealerPlay()
    {
        if (!playerStood)
            throw new InvalidOperationException("Player must stand before dealer plays.");
    }
};