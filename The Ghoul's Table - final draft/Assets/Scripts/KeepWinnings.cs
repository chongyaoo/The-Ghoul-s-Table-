using UnityEngine;
using UnityEngine.UI;

public class KeepWinnings : MonoBehaviour
{
    [SerializeField] private Button keepWinningsBtn;
    [SerializeField] private BlackjackGameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keepWinningsBtn.onClick.AddListener(KeepWins);
    }

    // Update is called once per frame
    void KeepWins()
    {
        gameManager.KeepWinnings();
    }
}
