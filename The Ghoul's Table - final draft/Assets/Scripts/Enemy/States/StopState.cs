using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StopState : BaseState
{
    public override void Enter()
    {
        enemy.Agent.speed = 0;
        enemy.Agent.isStopped = true;
        enemy.Agent.velocity = Vector3.zero;
    }

    public override void Perform()
    {

    }
    public override void Exit() 
    { 
    
    }
}
