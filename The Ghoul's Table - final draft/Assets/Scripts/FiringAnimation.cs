using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class FiringAnimation : MonoBehaviour
{
    [SerializeField] private Animator coltAnimator;

    private Enemy enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    public void TriggerFire()
    {
        StartCoroutine(ShootingBullet());
    }

    private bool Shoot()
    {
        float chance = UnityEngine.Random.value;
        if (chance < (1f / 6f))
            return true;
        return false;
    }
    private IEnumerator ShootingBullet()
    {
        Debug.Log("Shooting now");
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f; // Restore physics deltaTime as well
        coltAnimator.SetTrigger("shootbullet");
        yield return new WaitForSeconds(1f); //waiting for shooting animation to finish
        TMP_Text promptText = enemy.Player.GetComponentInChildren<TMP_Text>();
        if (Shoot())
        {
            promptText.text = "You have been killed!";
            SaveManager.UpdateKilled();
        }
        else
        {
            promptText.text = "You've made it off... this time round.";
        }
        yield return new WaitForSeconds(3f);
        enemy.EndGame(); //returning back to gamescene
    }
}
