using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolState : BaseState
{
    public int waypointIndex;
    int count;
    float minDist = float.PositiveInfinity;
    int minWayPoint;
    public override void Enter()
    {
        count = enemy.path.waypoints.Count;
        for (int i = 0; i < count; i++) //locating the nearest waypoint to go back to patrolling
        {
            float dist = Vector3.Distance(enemy.transform.position, enemy.path.waypoints[i].position);
            if (dist < minDist)
            {
                minWayPoint = i;
                minDist = dist;
            }
        }
        waypointIndex = minWayPoint;
        Debug.Log("THE MINWAYPOINT IS " + minWayPoint);
        enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
    }

    public override void Perform()
    {
        PatrolCycle();
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    public override void Exit()
    {

    }

    public void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f) //<0.2f from the target waypoint
        {
            if (waypointIndex < enemy.path.waypoints.Count - 1)
                waypointIndex++;
            else
                waypointIndex = 0;
            enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
        }
    }
}
