using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuStart : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenuButton.onClick.AddListener(TriggerGame);
    }

    // Update is called once per frame
    void TriggerGame()
    {
        SaveManager.DeleteSaved();
        SceneManager.LoadScene("GameScene");
    }
}
