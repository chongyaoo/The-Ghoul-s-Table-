using UnityEngine;

public class TriggerColt : MonoBehaviour
{
    [SerializeField] private BlackjackGameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OnAnimationEnd()
    {
        gameManager.ShootResult();
    }
}
