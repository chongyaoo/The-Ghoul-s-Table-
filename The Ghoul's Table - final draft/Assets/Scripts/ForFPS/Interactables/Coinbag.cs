using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Coinbag : GameInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        promptText = GetComponentInChildren<TMP_Text>();
    }

    public override void Hover()
    {
        if (gameManager.GameState == GameState.Betting)
            promptText.text = "Bet 20 bucks!";
    }

    protected override void Interact()
    {
        if (gameManager.GameState == GameState.Betting)
            gameManager.PlayerBet();
    }
}
