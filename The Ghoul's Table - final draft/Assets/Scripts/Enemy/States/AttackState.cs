using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class AttackState : BaseState
{
    private float moveTimer; //enemy moves abit
    private float losePlayerTimer;
    public override void Enter()
    {

    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            enemy.Agent.speed = 6f;
            enemy.Agent.SetDestination(enemy.Player.transform.position);
            enemy.Agent.stoppingDistance = 2.5f;
            if (enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance) //dealer has reached Player
            {
                enemy.Agent.speed = 0f;
                Debug.Log("Dealer has shot you");
                TMP_Text statusText = enemy.Player.GetComponentInChildren<TMP_Text>();
                statusText.text = "You have been shot!";
                enemy.Player.GetComponent<CharacterController>().enabled = false;
                enemy.Player.GetComponent<InputManager>().OnDisable();
                Time.timeScale = 0.1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale; // ensures physics still work
                enemy.Player.GetComponent<GamePlayerLook>().PanCamera(enemy.transform);
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                //change to search state
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
    public override void Exit() 
    { 
    
    }
}
