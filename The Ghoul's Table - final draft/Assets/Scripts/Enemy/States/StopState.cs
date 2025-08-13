using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StopState : BaseState
{

    private GameObject colt;

    FiringAnimation anim;
    public override void Enter()
    {
        enemy.Agent.speed = 0;
        enemy.Agent.isStopped = true;
        enemy.Agent.velocity = Vector3.zero;
        colt = enemy.transform.Find("Colt").gameObject;
        anim = colt.GetComponent<FiringAnimation>();
        anim.TriggerFire();
    }

    public override void Perform()
    {
        
    }
    public override void Exit() 
    { 
    
    }
}
