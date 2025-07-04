using Unity.VisualScripting;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BlackjackAnimations : MonoBehaviour
{
    [SerializeField] private float cardOffset = 0.5f; //offset between x position of cards
    [SerializeField] private float cardzOffset = 0.2f;

    [SerializeField] private BlackjackGameManager gameManager;
    public IEnumerator StartRoundAnimations(List<CardView> initialCards)
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 2; i++) //dealer's first card should be face up
        {
            Vector3 playerPosition = gameManager.PlayerArea.position;
            Vector3 dealerPosition = gameManager.DealerArea.position;
            CardView playerCardView = initialCards[i * 2];
            playerPosition.x += i * cardOffset;
            playerPosition.z -= i * cardzOffset;
            LeanTween.move(playerCardView.gameObject, playerPosition, 1.0f).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.rotate(playerCardView.gameObject, new Vector3(0, 180, 0), 1.0f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(1f);
            CardView dealerCardView = initialCards[i * 2 + 1];
            dealerPosition.x -= i * cardOffset;
            dealerPosition.z += i * cardzOffset;
            LeanTween.move(dealerCardView.gameObject, dealerPosition, 1.0f).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.move(dealerCardView.gameObject, dealerPosition, 1.0f).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.rotate(dealerCardView.gameObject, new Vector3(0, 0, 0), 1.0f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator PlayerDrawAnimation(CardView cardDrawn, int playerHandCount)
    {
        Vector3 playerPosition = gameManager.PlayerArea.position;
        playerPosition.x += (playerHandCount - 1) * cardOffset;
        playerPosition.z -= (playerHandCount - 1) * cardzOffset;
        LeanTween.move(cardDrawn.gameObject, playerPosition, 1.0f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.rotate(cardDrawn.gameObject, new Vector3(0, 180, 0), 1.0f).setEase(LeanTweenType.easeInOutQuad);
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator DealerDrawAnimation(List<CardView> cardsDrawn, int numCardDrawn)
    {
        for (int i = 0; i < numCardDrawn; i++)
        {
            Vector3 dealerPosition = gameManager.DealerArea.position;
            CardView cardDrawn = cardsDrawn[i];
            dealerPosition.x -= (i + 2) * cardOffset;
            dealerPosition.z += (i + 2) * cardzOffset;
            LeanTween.move(cardDrawn.gameObject, dealerPosition, 1.0f).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.rotate(cardDrawn.gameObject, new Vector3(0, 0, 0), 1.0f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator DealerFlipAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (Transform child in gameManager.DealerArea)
        {
            LeanTween.rotate(child.gameObject, new Vector3(0, 180, 0), 1.0f).setEase(LeanTweenType.easeInOutQuad);
        }
        yield return new WaitForSeconds(1f);
    }
}
