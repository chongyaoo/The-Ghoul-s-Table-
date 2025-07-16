using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class Colt : GameInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //for the colt, all 4 children are attached this Colt interactable script. Use the inspector to attach the TMP_Text
        promptText = transform.parent.GetComponentInChildren<TMP_Text>();
    }

    public override void Hover()
    {
        Debug.Log("Colt can be detected");
        if (gameManager.GameState == GameState.EndGameWin)
            promptText.text = "Shoot Dealer!";
    }

    protected override void Interact()
    {
        if (gameManager.GameState == GameState.EndGameWin)
            gameManager.ShootDealer();
    }
}
