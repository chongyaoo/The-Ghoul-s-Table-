using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;

public class InputManager : MonoBehaviour
{

    private TMP_Text promptText;

    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;
    public PlayerInput.OnFootActions OnFoot => onFoot;

    private PlayerMotor motor;
    private PlayerLook look;

    private bool isCaught = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();
        promptText = GetComponentInChildren<TMP_Text>();
    }

    void Start()
    {
        StartCoroutine(StartCounter());
    }

    private IEnumerator StartCounter()
    {
        int count = 45;
        while (count > 0 && !isCaught)
        {
            promptText.text = "Stay Alive! " + count;
            yield return new WaitForSeconds(1f);
            count--;
        }
        if (!isCaught)
            promptText.text = "You have made it!";
    }

    public void Caught()
    {
        isCaught = true;
    }
    
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable ()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
