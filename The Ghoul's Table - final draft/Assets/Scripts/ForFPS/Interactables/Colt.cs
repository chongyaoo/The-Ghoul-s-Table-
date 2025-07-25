using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class Colt : GameInteractable
{
    private Animator coltAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        //for the colt, all 4 children are attached this Colt interactable script. Use the inspector to attach the TMP_Text
        promptText = transform.parent.GetComponentInChildren<TMP_Text>();
        coltAnimator =  GetComponentInParent<Animator>(); //because the script is attached to the childobjects, not the parent colt 
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
        {
            gameManager.ShootDealer();
            StartCoroutine(ShootDealer());
        }
    }

    private IEnumerator ShootDealer()
    {
        yield return new WaitForSeconds(1f);
        coltAnimator.SetTrigger("LoadBullet");
    }
}
