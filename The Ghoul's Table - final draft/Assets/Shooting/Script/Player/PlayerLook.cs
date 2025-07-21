using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 23f;
    public float ySensitivity = 23f;

    public void ProcessLook (Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //calculate camera rotation for looking around
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        //apply to cam transform
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //rotate player to look left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    public void PanCamera(Transform enemy)
    {
        StartCoroutine(PanCameraToDealer(enemy));
    }

    public IEnumerator PanCameraToDealer(Transform dealerTransform)
    {
        Debug.Log("Panning to dealer now");
        Vector3 direction = dealerTransform.position - cam.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Debug.DrawRay(cam.transform.position, direction * 20f, Color.blue, 20f);

        Quaternion startRotation = cam.transform.rotation;

        float duration = 2.5f; // seconds (scaled by timeScale)
        float t = 0f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / duration;
            cam.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }
    }
}
