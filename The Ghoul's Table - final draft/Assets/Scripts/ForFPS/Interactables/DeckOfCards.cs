using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class DeckOfCards : GameInteractable
{
    private void Start()
    {
        promptText = GetComponentInChildren<TMP_Text>();
    }

    public override void Hover()
    {
        if (gameManager.GameState == GameState.Waiting)
            promptText.text = "Hit";
    }

    protected override void Interact()
    {
        if (gameManager.GameState == GameState.Waiting)
            gameManager.PlayerHit();
    }
}
