using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private Card logicalCard;

    // For prefab identification (set these in inspector for each prefab)
    [Header("Card Identity (Set in Prefab)")]
    [SerializeField] private Suit suit;
    [SerializeField] private Rank rank;

    // For 3D models, you might want to control visibility or animations
    [SerializeField] private GameObject cardModel;
    [SerializeField] private Animator cardAnimator; // Optional: for flip animations

    private bool isRevealed = false;

    public Card LogicalCard => logicalCard; // read-only attribute. essentially, logicalCard is private attribute, with public function returnlogicalCard()
    public Suit Suit => suit;
    public Rank Rank => rank;

    public void Initialize(Card card)
    {
        logicalCard = card;
        // For 3D models, you might not need to change visuals since each prefab is unique
        // But you could trigger animations or effects here
    }

    public void Reveal()
    {
        isRevealed = true;
        // Could trigger flip animation or show the card
        if (cardAnimator != null)
            cardAnimator.SetBool("Revealed", true);
    }

    public void Hide()
    {
        isRevealed = false;
        // Could trigger hide animation or show card back
        if (cardAnimator != null)
            cardAnimator.SetBool("Revealed", false);
    }

    public void FlipCard()
    {
        isRevealed = !isRevealed;
        if (cardAnimator != null)
            cardAnimator.SetBool("Revealed", isRevealed);
    }
}