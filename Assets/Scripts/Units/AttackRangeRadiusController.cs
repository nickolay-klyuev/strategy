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

        //draw attack range radius
        lineRenderer.positionCount = 51;
        lineRenderer.useWorldSpace = false;
        CreatePoints();
    }

    private void CreatePoints ()
    {
        float x;
        float y;
        //float z;

        float angle = 20f;

        for (int i = 0; i < (50 + 1); i++)
        {
            if ()
            {

            }
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * unitProperties.attackRange;
            y = Mathf.Cos (Mathf.Deg2Rad * angle) * unitProperties.attackRange;

            lineRenderer.SetPosition(i, new Vector3(x,y,0) );

            angle += (360f / 50);
        }
    }

    //trigger if something in radius of attack
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (ColliderResult(collider) && unitProperties.unitType == "enemy")
        {
            moveController.StopMoving();
        }

        if (ColliderResult(collider) && !attackController.GetIsAttacking() && (unitProperties.canFireWhileMoving || !moveController.GetIsMoving()))
        {
            StartAttack(collider);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (ColliderResult(collider) && unitProperties.unitType == "enemy")
        {
            moveController.StopMoving();
        }

        if (ColliderResult(collider) && !attackController.GetIsAttacking() && (unitProperties.canFireWhileMoving || !moveController.GetIsMoving()))
        {
            StartAttack(collider);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (ColliderResult(collider) && attackController.GetIsAttacking())
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
