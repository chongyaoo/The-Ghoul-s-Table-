using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnBtn : MonoBehaviour
{
    [SerializeField] private Button returnButton;
    [SerializeField] private GameObject canvas;
    public void Start()
    {
        returnButton.onClick.AddListener(Return); //trigger function inside 
    }

    private void Return()
    {
        canvas.SetActive(false);
    }
}
