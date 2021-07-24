using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        unitProperties = GetComponent<UnitProperties>();
        attackController = GetComponent<AttackController>();
        moveLineDrawer = GetComponent<MoveAttackLineDrawer>();
    }

    // Update is called once per frame
    void Update()
    {
        // move
        if (isMoving)
        {
            // stop after reach point to move
            float stopRange = .2f;
            if (transform.position.x < pointToMove.x + stopRange && transform.position.x > pointToMove.x - stopRange &&
                transform.position.y < pointToMove.y + stopRange && transform.position.y > pointToMove.y - stopRange)
            {
                isMoving = false;
            }

            transform.position = Vector3.MoveTowards(transform.position, pointToMove, speed * Time.deltaTime);
        }

        // chase
        if (isChasing && chasingTarget != null)
        {
            Vector3 vectorToTarget = transform.position - chasingTarget.transform.position;
            float rangeToTarget = CalculateVectorRange(vectorToTarget);
            
            if (rangeToTarget > unitProperties.attackRange - unitProperties.attackRange * 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, chasingTarget.transform.position, speed * Time.deltaTime);
                if (rangeToTarget <= unitProperties.attackRange && !attackController.GetIsAttacking())
                {
                    attackController.StartAttack(chasingTarget);
                }
                else if (rangeToTarget > unitProperties.attackRange && attackController.GetIsAttacking())
                {
                    attackController.StopAttack();
                }
            }
        }
        else if (chasingTarget == null)
        {
            isChasing = false;
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
        pointToMove = new Vector3(point.x, point.y, transform.position.z);

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
        moveLineDrawer.StopDraw();

        isChasing = false;
        return !isChasing;
    }
}
