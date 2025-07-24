using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class GameInteractable : MonoBehaviour
{
    public string promptMessage;
    protected TMP_Text promptText;
    [SerializeField] protected BlackjackGameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void BaseInteract()
    {
        Interact();
    }

    public void SetPromptText(TMP_Text input)
    {
        promptText = input;
    }

    public void SetGameManager(BlackjackGameManager input)
    {
        gameManager = input;
    }

    public virtual void Hover()
    {
        //template function to be overwritten
    }

    public virtual void NonHover()
    {
        promptText.text = "";
    }
    protected virtual void Interact()
    {
        //template function to be overwritten by child classes
    }
}
