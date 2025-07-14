using NUnit.Framework;
using TMPro;
using UnityEngine;
public class Chip : Interactable
{
    [SerializeField] private TMP_Text statusText;

    private void Start()
    {
        GameObject promptGO = GameObject.Find("PromptText");
        if (promptGO != null)
            statusText = promptGO.GetComponent<TMP_Text>();
    }
    protected override void Interact()
    {
        Debug.Log("Lock in Bets!");
        statusText.text = "Lock in Bets!";  
    }
}