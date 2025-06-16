using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable enable


public class BlackjackGameManager : MonoBehaviour
{
    public Transform playerArea = null!;
    public Transform dealerArea = null!;
    public TMP_Text statusText = null!;
    public DeckManager deckManager = null!;

    private BlackjackGame game = null!;
    [SerializeField] private Button hitButton = null!;
    [SerializeField] private Button standButton = null!;

    [SerializeField] private float cardOffset; // offset between x position of cards; it is 3f in the inspector now too. 
    [SerializeField] private float cardzOffset = 0.2f;

    IEnumerator StartRoundAnimations(BlackjackGame game)
    {
        yield return new WaitForSeconds(1f);
        Hand playerHand = game.GetPlayerHand();
        Hand dealerHand = game.GetDealerHand(); //right now, both hands should have only 2 cards
        Vector3 playerPosition = playerArea.position;
        Vector3 dealerPosition = dealerArea.position;
        for (int i = 0; i < 2; i++) //dealer's first card should be face up
        {
            Card playerDrawnCard = playerHand.GetCards()[i];
            CardView playerCardView = deckManager.DrawCardPrefab(playerDrawnCard);
            playerPosition.x += i * cardOffset;
            playerPosition.z -= i * cardzOffset;
            LeanTween.move(playerCardView.gameObject, playerPosition, 1.0f).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.rotate(playerCardView.gameObject, new Vector3(0, 180, 0), 1.0f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(1f);
            Card dealerDrawnCard = dealerHand.GetCards()[i];
            CardView dealerCardView = deckManager.DrawCardPrefab(dealerDrawnCard);
            dealerPosition.x -= i * cardOffset;
            dealerPosition.z += i * cardzOffset;
            LeanTween.move(dealerCardView.gameObject, dealerPosition, 1.0f).setEase(LeanTweenType.easeInOutQuad);
            LeanTween.rotate(dealerCardView.gameObject, new Vector3(0, 0, 0), 1.0f).setEase(LeanTweenType.easeInOutQuad);
            yield return new WaitForSeconds(1f);
        }
        EnableButton();
        statusText.text = "Your turn!";
    }

    IEnumerator PlayerDrawAnimation(BlackjackGame game)
    {
        Card? drawnCard = game.PlayerHit();
        if (drawnCard == null) yield break; //this breaks the coroutine immediately
        CardView cardView = deckManager.DrawCardPrefab(drawnCard);
        int playerHandCount = game.GetPlayerHandCount();
        Vector3 position = playerArea.position;
        position.x += (playerHandCount - 1) * cardOffset;
        position.z -= (playerHandCount - 1) * cardzOffset;
        LeanTween.move(cardView.gameObject, position, 1.0f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.rotate(cardView.gameObject, new Vector3(0, 180, 0), 1.0f).setEase(LeanTweenType.easeInOutQuad);
        yield return new WaitForSeconds(1f);
        EnableButton();
    }

    IEnumerator GameStart()
    {
        statusText.text = "Game Starting!";
        yield return new WaitForSeconds(1f);
        statusText.text = "Drawing cards...";
    }
    void Start()
    {
        game = new BlackjackGame(); // Use seed if needed
        DisableButton();
        StartRound();
    }

    void EnableButton()
    {
        hitButton.interactable = true;
        standButton.interactable = true;
    }

    private void DisableButton()
    {
        hitButton.interactable = false;
        standButton.interactable = false;
    }

    public void StartRound()
    {
        ClearTable();
        StartCoroutine(GameStart());
        bool started = game.StartRound(); // calling BlackjackGame.StartRound()
        if (!started)
        {
            statusText.text = "Deck exhausted.";
            return;
        }
        DisableButton();
        StartCoroutine(StartRoundAnimations(game));
    }

    public void PlayerHit()
    {
        Debug.Log("Player Hit!");
        DisableButton();
        StartCoroutine(PlayerDrawAnimation(game));

        if (game.GetPlayerHand().IsBust())
        {
            statusText.text = "Nice big bust!";
            EndGame();
        }
    }

    public void PlayerStand()
    {
        game.PlayerStand();
        statusText.text = "Dealer's turn!";
        game.DealerPlay();
        EndGame();
    }

    void EndGame()
    {
        DisplayHand(game.GetDealerHand(), dealerArea);
        //DisplayHand(game.GetPlayerHand(), playerArea);
        //    var outcome = game.GetOutcome(); // You'll need to add this method
        //    statusText.text = $"Outcome: {outcome}";
    }

    void DisplayHand(Hand hand, Transform area)
    {

        foreach (Transform child in area)
            Destroy(child.gameObject);

        List<Card> cards = hand.GetCards();
        for (int i = 0; i < cards.Count; i++)
        {
            CardView cardView = deckManager.DrawCardPrefab(cards[i]); //then, need to transform to the correct position
            Vector3 position = area.position;
            position.x += i * cardOffset;
            cardView.transform.position = position;

            //if (hideSecondCard && i == 1)
            //    cardView.cardText.text = "Hidden";
            //else
            //    cardView.SetCard(cards[i]);
        }
    }

    void ClearTable()
    {
        foreach (Transform child in playerArea)
            Destroy(child.gameObject);
        foreach (Transform child in dealerArea)
            Destroy(child.gameObject);
    }

    //void Update()
    //{
    //    //if (Input.GetKeyDown(KeyCode.Space)) 
    //    //{
    //    //    Card card = game.DrawCard();
    //    //    CardView drawnCard = deckManager.DrawCardPrefab(card);
    //    //    Debug.Log("Keyspace pressed");
    //    //    if (drawnCard != null)
    //    //    {
    //    //        Debug.Log($"Drew: {drawnCard.Rank} of {drawnCard.Suit}");
    //    //        // Move card to a visible position
    //    //        drawnCard.transform.position = testingArea.position;
    //    //    }
    //    //} //this is for testing purposes

    //    //if (Input.GetKeyDown(KeyCode.Q))
    //    //{
    //    //    Debug.Log("Player Hit!");
    //    //    Card? drawnCard = game.PlayerHit();
    //    //    if (drawnCard == null)
    //    //    {
    //    //        Debug.Log("null");
    //    //    }
    //    //    CardView cardView = deckManager.DrawCardPrefab(drawnCard);
    //    //    Debug.Log($"Drew: {drawnCard.Rank} of {drawnCard.Suit}");
    //    //    int playerHandCount = game.GetPlayerHandCount();
    //    //    Vector3 position = playerArea.position;
    //    //    position.x += (playerHandCount - 1) * cardOffset;
    //    //    cardView.transform.position = position;
    //    //}  //this was for debugging purposes
    //}

}
