using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveController : MonoBehaviour
{
    public float speed = 1f;

    private bool isMoving = false;
    private bool isChasing = false;
    public bool GetIsChasing()
    {
        return isChasing;
    }
    
    private GameObject chasingTarget;
    public GameObject GetChasingTarget()
    {
        return chasingTarget;
    }

    private Vector3 pointToMove;
    private UnitProperties unitProperties;
    private AttackController attackController;
    private MoveAttackLineDrawer moveLineDrawer;
    private NavMeshAgent agent;
    private Transform thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;

        unitProperties = GetComponent<UnitProperties>();
        attackController = GetComponent<AttackController>();
        if (attackController == null)
        {
            attackController = GetComponentInChildren<AttackController>();
        }

        moveLineDrawer = GetComponent<MoveAttackLineDrawer>();

        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && !isChasing && agent != null) // clear nav mesh moving
        {
            agent.ResetPath();
        }

        // move
        if (isMoving)
        {
            // stop after reach point to move
            float stopRange = .2f;
            if (thisTransform.position.x < pointToMove.x + stopRange && thisTransform.position.x > pointToMove.x - stopRange &&
                thisTransform.position.y < pointToMove.y + stopRange && thisTransform.position.y > pointToMove.y - stopRange)
            {
                isMoving = false;
            }

            if (agent != null)
            {
                agent.SetDestination(pointToMove);
            }
            else
            {
                thisTransform.position = Vector3.MoveTowards(thisTransform.position, pointToMove, speed * Time.deltaTime);
            }
        }

        // chase
        if (isChasing && chasingTarget != null)
        {
            Transform targetTransform = chasingTarget.transform;
            Vector3 targetPosition = targetTransform.position;
            Vector3 vectorToTarget = thisTransform.position - targetPosition;
            float rangeToTarget = CalculateVectorRange(vectorToTarget);
            
            if (rangeToTarget > unitProperties.attackRange - unitProperties.attackRange * 0.1f)
            {
                if (agent != null)
                {
                    agent.SetDestination(targetPosition);
                }
                else
                {
                    thisTransform.position = Vector3.MoveTowards(thisTransform.position, targetPosition, speed * Time.deltaTime);
                }

                if (rangeToTarget <= unitProperties.attackRange && !attackController.GetIsAttacking())
                {
                    attackController.StartAttack(chasingTarget);
                }
                else if (rangeToTarget > unitProperties.attackRange && attackController.GetIsAttacking())
                {
                    attackController.StopAttack();
                }
            }
            else
            {
                if (agent != null)
                {
                    agent.ResetPath();
                }
            }
        }
        else if (chasingTarget == null && isChasing)
        {
            isChasing = false;
            attackController.StopAttack();
        }

        // rotate FIXME: this part moved to NavMeshAgentScript because it doesn't work here, I have no fucking idea why.
        /*if (GetChasingTarget() != null && GetIsChasing() && !attackController.GetIsAttacking())
        {
            if (agent != null)
            {
                UnitRotateController.RotateToPoint(agent.path.corners[1], thisTransform);
            }
            else
            {
                UnitRotateController.RotateToPoint(GetChasingTarget().transform.position, thisTransform);
            }
        }*/

        if (GetIsMoving())
        {
            if (agent != null)
            {
                UnitRotateController.RotateToPoint(agent.path.corners[1], thisTransform);
            }
            else
            {
                UnitRotateController.RotateToPoint(GetPointToMove(), thisTransform);
            }
        }
    }

    private float CalculateVectorRange(Vector3 vector)
    {
        return Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2));
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public Vector3 GetPointToMove()
    {
        return pointToMove;
    }

    public void MoveToPoint(Vector3 point)
    {
        pointToMove = new Vector3(point.x, point.y, thisTransform.position.z);

        if (moveLineDrawer != null)
        {
            moveLineDrawer.StartDraw(pointToMove);
        }

        isChasing = false;
        isMoving = true;
    }

    public void StopMoving()
    {
        if (moveLineDrawer != null)
        {
            moveLineDrawer.StopDraw();
        }

        isMoving = false;
    }

    public bool StartChasing(GameObject target)
    {
        chasingTarget = target;

        if (moveLineDrawer != null)
        {
            moveLineDrawer.StartDraw(target);
        }

        isMoving = false;
        isChasing = true;
        return isChasing;
    }

    public bool StopChasing()
    {
        if (moveLineDrawer != null) // skip for enemies
        {
            moveLineDrawer.StopDraw();
        }

        isChasing = false;
        return !isChasing;
    }
}
