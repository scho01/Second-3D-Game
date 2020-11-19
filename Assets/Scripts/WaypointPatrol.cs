using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public GameObject player;
    public GameEnding gameEnding;
    private int m_CurrentWaypointIndex;
    private bool playerFound;
    private bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (!GetComponent<CapsuleCollider>().enabled)
            return;
        if (other == player.GetComponent<CapsuleCollider>())
            m_IsPlayerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!GetComponent<CapsuleCollider>().enabled)
            return;
        if (other == player.GetComponent<CapsuleCollider>())
            m_IsPlayerInRange = false;
    }

    public void SetPlayerFound()
    {
        playerFound = true;
        navMeshAgent.speed = 1.5f;
        GetComponent<CapsuleCollider>().enabled = true;
    }

    void Start()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        m_IsPlayerInRange = false;
        playerFound = false;
        navMeshAgent.speed = 1;
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void FixedUpdate()
    {
        if (m_IsPlayerInRange)
            gameEnding.CaughtPlayer();
        if (!playerFound)
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                if (waypoints.Length > 1)
                {
                    m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                    navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                }
            }
        }
        else
            navMeshAgent.SetDestination(player.transform.position);
    }
}
