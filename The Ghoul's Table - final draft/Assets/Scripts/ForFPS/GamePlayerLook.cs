using UnityEngine;

public class GamePlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    private float yRotation = 0f;

    public float xSensitivity = 0.07f;
    public float ySensitivity = 0.07f;
    
    public void ProcessLook (Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= mouseY * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -40f, 40f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        yRotation += mouseX * xSensitivity;
        yRotation = Mathf.Clamp(yRotation, -45f, 45f);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
    }
}
