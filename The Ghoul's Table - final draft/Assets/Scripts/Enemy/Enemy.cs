using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;

    public float sightDistance = 20f;
    public float fieldofView = 85f;

    public float eyeHeight;

    private Vector3 lastKnownPos;
    public NavMeshAgent Agent => agent;
    public GameObject Player => player;

    public Vector3 LastKnownPos {get => lastKnownPos; set => lastKnownPos = value; } //can access and assign new value to it

    [SerializeField] private string currentState; //for debugging??

    public WayPath path;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialize();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight); //vector subtraction; direction from enemy to player
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward); //angle to player
                if (angleToPlayer >= -fieldofView && angleToPlayer <= fieldofView) //within field of vision
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast (ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player) //if enemy line of sight blocked by walls
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
