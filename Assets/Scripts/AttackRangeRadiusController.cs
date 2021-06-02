using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeRadiusController : MonoBehaviour
{
    private UnitProperties unitProperties;
    private CircleCollider2D circleCollider;
    private AttackController attackController;

    // Start is called before the first frame update
    void Start()
    {
        unitProperties = transform.parent.GetComponent<UnitProperties>();
        attackController = transform.parent.GetComponent<AttackController>();
        circleCollider = GetComponent<CircleCollider2D>();

        //set attack range radius to collider 
        circleCollider.radius = unitProperties.attackRange;
    }

    //trigger if something in radius of attack
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (ColliderResult(collider))
        {
            attackController.StartAttack(collider.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (ColliderResult(collider) && !attackController.GetIsAttacking())
        {
            attackController.StartAttack(collider.gameObject);
        }
    }

    void OnTriggerExit2D()
    {
        attackController.StopAttack();
    }

    // used to change behave for enemies or friendly units
    private bool ColliderResult(Collider2D collider)
    {
        return
            (collider.transform.GetComponent<UnitProperties>().unitType == "enemy" && unitProperties.unitType == "friendly") || 
            (collider.transform.GetComponent<UnitProperties>().unitType == "friendly" && unitProperties.unitType == "enemy");
    }
}
