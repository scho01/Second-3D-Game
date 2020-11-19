using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    public bool isGargoyle;
    private bool m_IsPlayerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
            m_IsPlayerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
            m_IsPlayerInRange = false;
    }

    void Start()
    {
        GetComponent<CapsuleCollider>().enabled = true;
    }

    void FixedUpdate()
    {
        if (m_IsPlayerInRange)
        {
            if (isGargoyle)
            {
                Vector3 direction = player.position - transform.position + Vector3.up;
                Ray ray = new Ray(transform.position, direction);
                RaycastHit raycastHit;
                if (Physics.Raycast(ray, out raycastHit))
                {
                    if (raycastHit.collider.transform == player)
                    {
                        gameEnding.CaughtPlayer();
                    }
                }
            }
            else
            {
                m_IsPlayerInRange = false;
                GetComponent<CapsuleCollider>().enabled = false;
                GetComponentInParent<WaypointPatrol>().SetPlayerFound();
            }
        }
    }
}
