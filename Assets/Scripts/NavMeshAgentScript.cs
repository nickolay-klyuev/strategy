using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private MoveController move;
    private AttackController attack;

    void Awake()
    {
        agent = gameObject.AddComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Start()
    {
        move = GetComponent<MoveController>();
        attack = GetComponent<AttackController>();
        if (attack == null)
        {
            attack = GetComponentInChildren<AttackController>();
        }
    }

    void Update()
    {
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        // FIXME: This part should be in MoveController, but fucking agent returs only 1 corner if it runs from MoveController
        // rotate
        /*if (move.GetChasingTarget() != null && move.GetIsChasing() && !attack.GetIsAttacking())
        {
            if (agent != null)
            {
                UnitRotateController.RotateToPoint(agent.path.corners[1], transform);
            }
            else
            {
                UnitRotateController.RotateToPoint(move.GetChasingTarget().transform.position, transform);
            }
        }*/
        // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    }
}
