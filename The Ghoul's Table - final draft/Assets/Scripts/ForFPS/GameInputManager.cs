using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    [SerializeField] private BlackjackGameManager gameManager;
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
        onFoot.Pause.performed += ctx => gameManager.HandlePause();
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

    public void EnableInputs(bool enable)
    {
        if (enable)
        {
            onFoot.Look.Enable();
            onFoot.Interact.Enable();
        }
        else
        {
            onFoot.Look.Disable();
            onFoot.Interact.Disable();
        }
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
