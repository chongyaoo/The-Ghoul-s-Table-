using UnityEngine;
using UnityEngine.UI;

public class Bet20Button : MonoBehaviour
{
    [SerializeField] private BlackjackGameManager gameManager;
    [SerializeField] private Button bet20Button;
    void Start()
    {
        bet20Button.onClick.AddListener(Bet20);
    }

    // Update is called once per frame
    void Bet20()
    {
        gameManager.PlayerBet();
    }
}
