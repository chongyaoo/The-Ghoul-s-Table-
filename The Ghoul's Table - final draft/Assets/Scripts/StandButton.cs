using UnityEngine;
using UnityEngine.UI;

public class StandButton : MonoBehaviour
{
    [SerializeField] private Button standButton;
    [SerializeField] private BlackjackGameManager gameManager;

    public void Start()
    {
        standButton.onClick.AddListener(TriggerStand);
    }

    private void TriggerStand()
    {
        gameManager.PlayerStand();
    }

}
