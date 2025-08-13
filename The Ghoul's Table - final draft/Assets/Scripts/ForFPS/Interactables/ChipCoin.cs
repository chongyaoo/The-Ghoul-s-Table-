using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class ChipCoin : GameInteractable
{
    public override void Hover()
    {
        if (gameManager.GameState == GameState.Betting)
        {
            promptText.text = "Lock in bets";
        }
        else if (gameManager.GameState == GameState.EndGameWin)
        {
            promptText.text = "Keep Winnings";
        }
    }

    protected override void Interact()
    {
        if (gameManager.GameState == GameState.Betting)
            gameManager.PlayerLockBets();
        else if (gameManager.GameState == GameState.EndGameWin)
            gameManager.KeepWinnings();
    }

}
