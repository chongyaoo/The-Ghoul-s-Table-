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
    public NavMeshAgent Agent => agent;
    public GameObject Player => player;

    private Vector3 lastKnownPos;
    public Vector3 LastKnownPos {get => lastKnownPos; set => lastKnownPos = value; } //can access and assign new value to it

    [SerializeField] private string currentState; //for debugging??

    public WayPath path;

    //void Start()
    //{

    //    stateMachine = GetComponent<StateMachine>();
    //    agent = GetComponent<NavMeshAgent>();
    //    stateMachine.Initialize();
    //}

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false; // disable it temporarily
        }

        int count = path.waypoints.Count;
        int enemySpawnIndex = Random.Range(0, count);
        int playerSpawnIndex;
        do
        {
            playerSpawnIndex = Random.Range(0, count);
        } while (enemySpawnIndex == playerSpawnIndex);

        Transform enemySpawnPoint = path.waypoints[enemySpawnIndex];
        Transform playerSpawnPoint = path.waypoints[playerSpawnIndex];

        //agent.Warp(enemySpawnPoint.position);
        transform.position = enemySpawnPoint.position; //tbh this doesnt really work. but i really can't figure out why.
        
        player.transform.position = playerSpawnPoint.position;

        if (controller != null)
        {
            controller.enabled = true; // re-enable after moving
        }

        Debug.Log("The count is " + count);
        Debug.Log("playerspawnpoint is " + playerSpawnIndex);
        Debug.Log("dealerspawnpoint is " + enemySpawnIndex);

        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialize();
        StartCoroutine(StartCounter());
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

    public void PanEnemy()
    {
        StartCoroutine(PanDealerToPlayer());
    }

    public IEnumerator PanDealerToPlayer()
    {
        Agent.updateRotation = false;
        Vector3 direction = player.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Vector3 targetEuler = targetRotation.eulerAngles;

        Quaternion startRotation = transform.rotation;
        Vector3 initialEuler = startRotation.eulerAngles;

        targetEuler.x = initialEuler.x;

        targetRotation = Quaternion.Euler(targetEuler); //fixing the x component (vertical component) to have no rotation (no yaw). i think navmeshagent only allows plane rotation (y-z) along the floor plane, which updates after the coroutine panning, causing it to snap back downwards (revert the x direction). hence, we do not allow rotation in the x direction at all.

        Debug.DrawRay(transform.position, direction * 20f, Color.green, 20f);

        float duration = 1.5f; // seconds (scaled by timeScale)
        float t = 0f;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / duration;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }
        
        transform.rotation = targetRotation;
    }

    private IEnumerator StartCounter()
    {
        int count = 45;
        while (count > 0)
        {
            yield return new WaitForSeconds(1f);
            count--;
        }
        Player.GetComponent<InputManager>().OnDisable();
        Player.GetComponent<InputManager>().enabled = false; //eitherways, after 45seconds whether caught ornot i will still disable the player and the enemy movements.S
        Agent.speed = 0;
        Agent.isStopped = true;
        Agent.velocity = Vector3.zero;
    }
}
