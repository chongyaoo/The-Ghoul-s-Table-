using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class BlackjackGameManager : MonoBehaviour
{
    public Transform playerArea;
    public Transform dealerArea;
    public TMP_Text statusText;
    public DeckManager deckManager;
    public Transform testingArea;

    private BlackjackGame game;
    [SerializeField] private Button hitButton;
    [SerializeField] private Button standButton;

    [SerializeField] private float cardOffset = 3f; // offset between x position of cards; it is 3f in the inspector now too. 

    IEnumerator WaitTenSeconds()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("10 seconds passed. they should now work");
    }
    void Start()
    {
        game = new BlackjackGame(); // Use seed if needed
        DisableButton();
        StartRound();
        StartCoroutine(WaitTenSeconds());
        EnableButton(); //put this under IEnumerator for it to wait 
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Card card = game.DrawCard();
            CardView drawnCard = deckManager.DrawCardPrefab(card);
            Debug.Log("Keyspace pressed");
            if (drawnCard != null)
            {
                Debug.Log($"Drew: {drawnCard.Rank} of {drawnCard.Suit}");
                // Move card to a visible position
                drawnCard.transform.position = testingArea.position;
            }
        } //this is for testing purposes

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Debug.Log("Player Hit!");
        //    Card? drawnCard = game.PlayerHit();
        //    if (drawnCard == null)
        //    {
        //        Debug.Log("null");
        //    }
        //    CardView cardView = deckManager.DrawCardPrefab(drawnCard);
        //    Debug.Log($"Drew: {drawnCard.Rank} of {drawnCard.Suit}");
        //    int playerHandCount = game.GetPlayerHandCount();
        //    Vector3 position = playerArea.position;
        //    position.x += (playerHandCount - 1) * cardOffset;
        //    cardView.transform.position = position;
        //}  //this was for debugging purposes
    }

    public void StartRound()
    {
        ClearTable();
        bool started = game.StartRound(); // calling BlackjackGame.StartRound()
        if (!started)
        {
            statusText.text = "Deck exhausted.";
            return;
        }

        DisplayHand(game.GetPlayerHand(), playerArea);
        DisplayHand(game.GetDealerHand(), dealerArea, hideSecondCard: true);

        statusText.text = "Your turn!";
    }

    public void PlayerHit()
    {
        Debug.Log("Player Hit!");
        Card? drawnCard = game.PlayerHit();
        if (drawnCard == null) return;
        CardView cardView = deckManager.DrawCardPrefab(drawnCard);
        int playerHandCount = game.GetPlayerHandCount();
        Vector3 position = playerArea.position;
        position.x += (playerHandCount - 1) * cardOffset;
        cardView.transform.position = position;

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
        DisplayHand(game.GetPlayerHand(), playerArea);
    //    var outcome = game.GetOutcome(); // You'll need to add this method
    //    statusText.text = $"Outcome: {outcome}";
    }

    void DisplayHand(Hand hand, Transform area, bool hideSecondCard = false)
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
}
