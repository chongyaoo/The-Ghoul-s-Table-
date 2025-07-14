using UnityEngine;

public class GameScenePlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    private float yRotation = 0f;

    public float xSensitivity = 23f;
    public float ySensitivity = 23f;

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //calculate camera rotation for looking around
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -30, 30);

        //apply to cam transform
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0); 

        

        //rotate cam to look left and right
        //transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);

        //rotating player to look left and right 
        yRotation += (mouseX * Time.deltaTime) * xSensitivity;
        yRotation = Mathf.Clamp(yRotation, -30, 30);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
       
    }
}
