using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BlackjackGameManager : MonoBehaviour
{
    public Transform playerArea;
    public Transform dealerArea;
    public TMP_Text statusText;
    public DeckManager deckManager;
    public Transform testingArea;

    private BlackjackGame game;

    void Start()
    {
        game = new BlackjackGame(); // Use seed if needed
        StartRound();
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
        }
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

        //statusText.text = "Your turn!";
    }

    public void PlayerHit()
    {
        if (!game.PlayerHit()) return;
        //DisplayHand(game.GetPlayerHand(), playerArea);

        //if (game.GetPlayerHand().IsBust())
        //{
        //    EndGame();
        //}
    }

    public void PlayerStand()
    {
        game.PlayerStand();
        game.DealerPlay();
        //EndGame();
    }

    //void EndGame()
    //{
    //    DisplayHand(game.GetDealerHand(), dealerArea);
    //    var outcome = game.GetOutcome(); // You'll need to add this method
    //    statusText.text = $"Outcome: {outcome}";
    //}

    void DisplayHand(Hand hand, Transform area, bool hideSecondCard = false)
    {
        foreach (Transform child in area)
            Destroy(child.gameObject);

        List<Card> cards = hand.GetCards();
        for (int i = 0; i < cards.Count; i++)
        {
            CardView cardView = deckManager.DrawCardPrefab(cards[i]); //then, need to transform to the correct position
            cardView.transform.position = area.position;

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
