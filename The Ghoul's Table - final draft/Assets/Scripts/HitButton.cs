using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class HitButton : MonoBehaviour
{
    [SerializeField] private Button hitButton;
    [SerializeField] private BlackjackGameManager gameManager;
    private void Start()
    {
        hitButton.onClick.AddListener(TriggerHit);
    }

    public void TriggerHit()
    {
        gameManager.PlayerHit();
    }

}
