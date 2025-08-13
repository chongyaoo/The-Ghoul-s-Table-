using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class StandCoin: GameInteractable
{
    private void Start()
    {
        promptText = GetComponentInChildren<TMP_Text>();
    }

    public override void Hover()
    {
        if (gameManager.GameState == GameState.Waiting)
            promptText.text = "Stand";
    }

    protected override void Interact()
    {
        if (gameManager.GameState == GameState.Waiting)
            gameManager.PlayerStand();
    }
}
