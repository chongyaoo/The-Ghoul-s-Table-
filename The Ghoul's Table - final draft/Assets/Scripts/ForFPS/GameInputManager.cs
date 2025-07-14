using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    private GamePlayerInput playerInput;
    private GamePlayerInput.OnFootActions onFoot;

    public GamePlayerInput.OnFootActions OnFoot => onFoot;

    private GamePlayerLook look;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = new GamePlayerInput();
        onFoot = playerInput.OnFoot;
        look = GetComponent<GamePlayerLook>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
