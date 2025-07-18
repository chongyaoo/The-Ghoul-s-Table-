using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class GamePlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;

    private GameInputManager gameInputManager;

    private GameInteractable lastGameInteractable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<GamePlayerLook>().cam;
        gameInputManager = GetComponent<GameInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<GameInteractable>() != null)
            {
                GameInteractable interactable = hitInfo.collider.GetComponent<GameInteractable>();
                lastGameInteractable = interactable;
                interactable.Hover();
                if (gameInputManager.OnFoot.Interact.triggered) //ok so this works
                {
                    interactable.BaseInteract();
                }
            }
        }
        else
        {
            if (lastGameInteractable)
                lastGameInteractable.NonHover();
        }
    }
}
