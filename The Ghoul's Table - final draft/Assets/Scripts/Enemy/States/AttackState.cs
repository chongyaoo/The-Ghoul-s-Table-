using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.AI;


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
            //enemy.Agent.stoppingDistance = 2.5f;
            if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < 1.5f) //dealer has reached Player
            {
                Debug.Log("Dealer has shot you");
                TMP_Text statusText = enemy.Player.GetComponentInChildren<TMP_Text>();
                statusText.text = "You have been shot!";
                //enemy.Player.GetComponent<CharacterController>().enabled = false;
                enemy.Player.GetComponent<InputManager>().OnDisable();
                enemy.Player.GetComponent<InputManager>().enabled = false; //disables the LateUpdate() of the inputmanager, which overwrites the rotation of the camera to the input (which would be zero rotation called late every frame)
                Time.timeScale = 0.1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale; // ensures physics still work
                stateMachine.ChangeState(new StopState());
                enemy.Player.GetComponent<PlayerLook>().PanCamera(enemy.transform);
                enemy.PanEnemy();

            }
            enemy.LastKnownPos = enemy.Player.transform.position;
        }
        else //lost sight of player
        {
            enemy.Agent.SetDestination(enemy.LastKnownPos);
            losePlayerTimer += Time.deltaTime;
            if (enemy.Agent.remainingDistance < 0.2f)
            {
                //change to search state
                stateMachine.ChangeState(new SearchState());
            }
        }
    }
    public override void Exit() 
    { 
    
    }
}
