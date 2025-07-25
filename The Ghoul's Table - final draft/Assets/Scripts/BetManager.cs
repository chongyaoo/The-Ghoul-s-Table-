using TMPro;
using UnityEngine;

public class BetManager : MonoBehaviour
{
    private int playerWinnings = 1000;
    private int playerBet = 0;

    private float yoffSet = 0.15f;
    private int numChips = 0;

    [SerializeField] private Transform chipArea;
    [SerializeField] private Transform chipPlacing;
    [SerializeField] private TMP_Text chipAreaText;
    [SerializeField] private GameObject chip;
    public int PlayerBet => playerBet;
    public int PlayerWinnings => playerWinnings;

    [SerializeField] private BlackjackGameManager gameManager;
    [SerializeField] private TMP_Text betsText;
    public void Start()
    {
        betsText.text = "Your Winnings: $" + playerWinnings + "\nBets: $" + playerBet;
    }

    public void Restart(int winnings)
    {
        playerWinnings = winnings;
        playerBet = 0;
        betsText.text = "Your Winnings: $" + playerWinnings + "\nBets: $" + playerBet;
    }
    public void PlayerBetOnce()
    {
        playerBet += 20;
        playerWinnings -= 20;
        betsText.text = "Your Winnings: $" + playerWinnings + "\nBets: $" + playerBet;

        Vector3 randomOffset = new Vector3(Random.Range(-0.01f, 0.01f), numChips * yoffSet, Random.Range(-0.01f, 0.01f));
        Vector3 spawnPosition = chipArea.position + randomOffset;

        GameObject newChip = Instantiate(chip, spawnPosition, Quaternion.identity, chipPlacing);
        newChip.transform.position = spawnPosition;

        GameInteractable chipInteractable = newChip.GetComponentInChildren<GameInteractable>();
        chipInteractable.SetPromptText(chipAreaText); //setting the prompttext TMP_Text at runtime
        chipInteractable.SetGameManager(gameManager);

        numChips++;
    }

    public void PlayerWin()
    {
        playerWinnings += playerBet * 2;
        playerBet = 0;
        betsText.text = "Your Winnings: $" + playerWinnings + "\nBets: $" + playerBet;
        foreach (Transform child in chipPlacing)
            Destroy (child.gameObject);
        numChips = 0;
    }

    public void PlayerLoseOrShoot()
    { 
        playerBet = 0;
        betsText.text = "Your Winnings: $" + playerWinnings + "\nBets: $" + playerBet;
        foreach (Transform child in chipPlacing)
            Destroy(child.gameObject);
        numChips = 0;
    }

    public void PlayerNatural()
    {
        playerWinnings += (playerBet/2) * 5;
        playerBet = 0;
        betsText.text = "Your Winnings: $" + playerWinnings + "\nBets: $" + playerBet;
        foreach (Transform child in chipPlacing)
            Destroy(child.gameObject);
        numChips = 0;
    }

    public void Push()
    {
        playerWinnings += playerBet;
        playerBet = 0;
        betsText.text = "Your Winnings: $" + playerWinnings + "\nBets: $" + playerBet;
        foreach (Transform child in chipPlacing)
            Destroy(child.gameObject);
        numChips = 0;
    }

    public void PlayerLostRoulette()
    {
        playerWinnings = 0;
        playerBet = 0;
        betsText.text = "Your Winnings: $" + playerWinnings + "\nBets: $" + playerBet;
    }

    public void PlayerWinRoulette()
    {
        playerWinnings += 5000;
        playerBet = 0;
        betsText.text = "Your Winnings: $" + playerWinnings + "\nBets: $" + playerBet;
    }
}
