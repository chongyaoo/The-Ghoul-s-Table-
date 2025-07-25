using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private BlackjackGameManager gameManager;
    [SerializeField] private Button mainMenuButton;
    public void Start()
    {
        mainMenuButton.onClick.AddListener(TriggerMainMenu); //trigger function inside 
    }

    private void TriggerMainMenu()
    {
        //here, i need to delete the json information and restart the game. ok this is done in the 
        gameManager.MainMenu();
    }
}
