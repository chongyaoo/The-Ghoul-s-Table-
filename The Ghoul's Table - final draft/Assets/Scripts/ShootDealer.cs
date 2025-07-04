using UnityEngine;
using UnityEngine.UI;

public class ShootDealer : MonoBehaviour
{
    [SerializeField] private Button shootDealerBtn;
    [SerializeField] private BlackjackGameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootDealerBtn.onClick.AddListener(Shoot);
    }

    // Update is called once per frame
    void Shoot()
    {
        gameManager.ShootDealer();
    }
}
