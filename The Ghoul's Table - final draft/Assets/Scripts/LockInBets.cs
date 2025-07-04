using UnityEngine;
using UnityEngine.UI;
public class LockInBets : MonoBehaviour
{

    [SerializeField] private BlackjackGameManager gameManager;

    [SerializeField] private Button lockBetsButton;
    void Start()
    {
        lockBetsButton.onClick.AddListener(LockBets);
    }

    // Update is called once per frame
    void LockBets()
    {
        gameManager.PlayerLockBets();
    }

}
