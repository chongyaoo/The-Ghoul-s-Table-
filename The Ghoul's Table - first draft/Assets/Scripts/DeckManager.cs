using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private CardView[] allCardPrefabs = new CardView[52]; // Drag all 52 prefabs here
    [SerializeField] private Transform deckPosition; // Where cards are stacked
    [SerializeField] private float cardStackOffset = 0.001f; // Small offset between stacked cards

    private List<CardView> activeCards = new List<CardView>();

    //void Start()
    //{
    //    InitializeDeck();
    //}

    //private void InitializeDeck()
    //{
    //    logicalDeck = new Deck();
    //    logicalDeck.Shuffle();
    //}

    private CardView FindCardPrefab(Suit suit, Rank rank)
    {
        foreach (CardView prefab in allCardPrefabs)
        {
            if (prefab.Suit == suit && prefab.Rank == rank)
                return prefab;
        }
        Debug.LogError($"Could not find prefab for {rank} of {suit}");
        return null;
    }

    public CardView InstantiatePrefab (Suit suit, Rank rank, Card card) //so here i pass in the logical card too from the deck
    {
        CardView prefab = FindCardPrefab(suit, rank);
        if (prefab == null) return null;
        CardView cardView = Instantiate (prefab, deckPosition.position, deckPosition.rotation);
        cardView.Initialize(card);
        return cardView;
    }

    public CardView DrawCardPrefab(Card drawnCard)
    {
        // Find the matching prefab
        CardView prefab = FindCardPrefab(drawnCard.Suit, drawnCard.Rank);
        if (prefab == null) 
            return null;

        // Instantiate the specific card prefab 
        CardView cardView = Instantiate (prefab, deckPosition.position, deckPosition.rotation);
        cardView.Initialize(drawnCard);

        activeCards.Add(cardView);
        return cardView;
    }

    //private CardView LoadCardPrefab(Suit suit, Rank rank)
    //{
    //    // Assuming prefabs are named like "AceOfSpades", "TwoOfHearts", etc.
    //    // and stored in Resources/Cards/ folder
    //    string cardName = $"{rank}Of{suit}s"; // Note: plural for suit names
    //    GameObject prefab = Resources.Load<GameObject>($"Cards/{cardName}");

    //    if (prefab != null)
    //    {
    //        GameObject cardObj = Instantiate(prefab);
    //        return cardObj.GetComponent<CardView>();
    //    }

    //    Debug.LogError($"Could not find card prefab: {cardName}");
    //    return null;
    //}

    public void ReturnCard(CardView cardView)
    {
        // Return card to deck (for game mechanics that require it)
        cardView.gameObject.SetActive(false);
        // Reset position to deck
        cardView.transform.position = deckPosition.position;
    }

    //public int CardsRemaining()
    //{
    //    return logicalDeck.CardsRemaining();
    //}

    //public void Shuffle()
    //{
    //    logicalDeck.Shuffle();
    //}
}