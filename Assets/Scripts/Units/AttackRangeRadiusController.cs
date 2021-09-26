using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeRadiusController : MonoBehaviour
{
    private UnitProperties unitProperties;
    private CircleCollider2D circleCollider;
    private LineRenderer lineRenderer;
    private AttackController attackController;
    private MoveController moveController;


    // Start is called before the first frame update
    void Start()
    {
        unitProperties = transform.parent.GetComponent<UnitProperties>();
        if (unitProperties == null)
        {
            unitProperties = transform.parent.GetComponentInParent<UnitProperties>();
        }

        attackController = transform.parent.GetComponent<AttackController>();
        circleCollider = GetComponent<CircleCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        moveController = transform.parent.GetComponent<MoveController>();
        if (moveController == null)
        {
            moveController = transform.parent.GetComponentInParent<MoveController>();
        }

        //set attack range radius to collider 
        circleCollider.radius = unitProperties.attackRange;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        
        // all enemies will chase enemies
        if (ColliderResult(collider) && unitProperties.unitType == "enemy" && !moveController.GetIsChasing())
        {
            moveController.StartChasing(collider.gameObject);
        }

        if (moveController.GetIsMoving() && !unitProperties.canFireWhileMoving)
        {
            attackController.StopAttack();
        }

        if (ColliderResult(collider) && !attackController.GetIsAttacking() && 
            (unitProperties.canFireWhileMoving || !moveController.GetIsMoving()))
        {
            if (moveController.GetIsChasing())
            {
                StartAttack(moveController.GetChasingTarget());
            }
            else
            {
                StartAttack(collider.gameObject);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (ColliderResult(collider) && attackController.GetIsAttacking() && 
            GameObject.ReferenceEquals(attackController.GetTargetGameobject(), collider.gameObject))
        {
            StopAttack();
        }
    }

    void FixedUpdate()
    {
        /*if (!unitProperties.canFireWhileMoving && moveController.GetIsMoving())
        {
            StopAttack();
        }*/
    }

    // used to change behave for enemies or friendly units
    private bool ColliderResult(Collider2D collider)
    {
        UnitProperties colliderUnitProperties = collider.GetComponent<UnitProperties>();
        if (colliderUnitProperties != null)
        {
            return
                (colliderUnitProperties.unitType == "enemy" && unitProperties.unitType == "friendly") || 
                (colliderUnitProperties.unitType == "friendly" && unitProperties.unitType == "enemy");
        }
        else
        {
            return false;
        }
    }

    private void StartAttack(GameObject target)
    {
        attackController.StartAttack(target);
    }

    private void StopAttack()
    {
        attackController.StopAttack();
    }
}
