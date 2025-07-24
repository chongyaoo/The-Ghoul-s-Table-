using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewGameButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private BlackjackGameManager gameManager;
    [SerializeField] private Button newGameButton;
    public void Start()
    {
        newGameButton.onClick.AddListener(TriggerNewGame); //trigger function inside 
    }

    private void TriggerNewGame()
    {
        gameManager.RestartGame();
    }
}
