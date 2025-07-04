using TMPro;
using UnityEngine;

public class BetManager : MonoBehaviour
{
    private int playerWinnings = 1000;
    private int playerBet = 0;

    public int PlayerBet => playerBet;
    public int PlayerWinnings => playerWinnings;

    [SerializeField] private BlackjackGameManager gameManager;
    [SerializeField] private TMP_Text betsText;
    public void Start()
    {
        betsText.text = "Your Winnings: " + playerWinnings + "\nBets: " + playerBet;
    }
    public void PlayerBetOnce()
    {
        playerBet += 20;
        playerWinnings -= 20;
        betsText.text = "Your Winnings: " + playerWinnings + "\nBets: " + playerBet;
    }

    public void PlayerWin()
    {
        playerWinnings += playerBet * 2;
        playerBet = 0;
        betsText.text = "Your Winnings: " + playerWinnings + "\nBets: " + playerBet;
    }

    public void PlayerLoseOrShoot()
    { 
        playerBet = 0;
        betsText.text = "Your Winnings: " + playerWinnings + "\nBets: " + playerBet;
    }

    public void PlayerNatural()
    {
        playerWinnings += (playerBet/2) * 5;
        playerBet = 0;
        betsText.text = "Your Winnings: " + playerWinnings + "\nBets: " + playerBet;
    }

    public void Push()
    {
        playerWinnings += playerBet;
        playerBet = 0;
        betsText.text = "Your Winnings: " + playerWinnings + "\nBets: " + playerBet;
    }

    public void PlayerLostRoulette()
    {
        playerWinnings = 0;
        playerBet = 0;
        betsText.text = "Your Winnings: " + playerWinnings + "\nBets: " + playerBet;
    }

    public void PlayerWinRoulette()
    {
        playerWinnings += 5000;
        playerBet = 0;
        betsText.text = "Your Winnings: " + playerWinnings + "\nBets: " + playerBet;
    }
}
