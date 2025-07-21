using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FiringAnimation : MonoBehaviour
{
    [SerializeField] private Animator coltAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void TriggerFire()
    {
        StartCoroutine(ShootingBullet());
    }

    private IEnumerator ShootingBullet()
    {
        Debug.Log("Shooting now");
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f; // Restore physics deltaTime as well
        coltAnimator.SetTrigger("shootbullet");
    }
}
