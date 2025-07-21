using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SearchState : BaseState
{
    private float searchTimer = 0;
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.LastKnownPos);
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState()); //if resights player, start attacking again
        }
        if (enemy.Agent.remainingDistance < 0.2f) //wah i cant believe i debugged for 2 hours just for this line
        {
            //immediately start searching
            enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere));
        }
        searchTimer += Time.deltaTime;
        if (searchTimer > 2f)
        {
            stateMachine.ChangeState(new PatrolState());
        }
    }
    public override void Exit() 
    { 

    }

}
