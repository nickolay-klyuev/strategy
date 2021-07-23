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
        attackController = transform.parent.GetComponent<AttackController>();
        circleCollider = GetComponent<CircleCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        moveController = transform.parent.GetComponent<MoveController>();

        //set attack range radius to collider 
        circleCollider.radius = unitProperties.attackRange;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        
        // all enemies will chase enemies
        if (ColliderResult(collider) && unitProperties.unitType == "enemy")
        {
            moveController.StartChasing(collider.gameObject);
        }

        if (ColliderResult(collider) && !attackController.GetIsAttacking() && (unitProperties.canFireWhileMoving || !moveController.GetIsMoving()))
        {
            StartAttack(collider);
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
        if (!unitProperties.canFireWhileMoving && moveController.GetIsMoving())
        {
            StopAttack();
        }
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

    private void StartAttack(Collider2D collider)
    {
        attackController.StartAttack(collider.gameObject);
    }

    private void StopAttack()
    {
        attackController.StopAttack();
    }
}
