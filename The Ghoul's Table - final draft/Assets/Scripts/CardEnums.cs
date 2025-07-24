using System.Collections.Generic;
using UnityEngine;
public enum Suit
{
    Hearts,
    Diamonds,
    Clubs,
    Spades
}

public enum Rank
{
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 14
}

public enum BlackjackOutcome
{
    None,
    PlayerBust,
    DealerBust,
    PlayerBlackjack,
    DealerBlackjack,
    PlayerWin,
    DealerWin,
    Push
}

public enum GameState
{
    Betting,
    Drawing,
    Waiting, 
    EndGameWin,
    EndGameLose,
    EndGamePush,
    EndGameShotFail,
    EndGameShotKill, 
    PlayerDied,
    DealerDied,
    Pause, 
    Shooting
}

public static class OutcomeText
{
    public static readonly Dictionary<BlackjackOutcome, string> OutcomeToText = new()
    {
        {BlackjackOutcome.PlayerBust, "You busted... a big one!" },
        {BlackjackOutcome.DealerBust, "He busted... a big one!" },
        {BlackjackOutcome.PlayerBlackjack, "You Blackjack!" },
        {BlackjackOutcome.DealerBlackjack, "Dealer Blackjack!" },
        {BlackjackOutcome.PlayerWin, "You win!" },
        {BlackjackOutcome.DealerWin, "Dealer win!" },
        {BlackjackOutcome.Push, "Push!" }
    };
}