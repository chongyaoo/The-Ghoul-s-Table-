using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionsBtn : MonoBehaviour
{
    [SerializeField] private Button instructionsButton;
    [SerializeField] private GameObject panel;
    public void Start()
    {
        instructionsButton.onClick.AddListener(TriggerMenu); //trigger function inside 
        panel.SetActive(false);
    }

    private void TriggerMenu()
    {
        panel.SetActive(true);
    }
}
