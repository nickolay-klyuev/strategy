using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float attackPower = 10f;
    public float flySpeed = 10f;

    private string parentUnitType;
    private Vector3 targetPosition;
    private bool isFlying = false;

    public void SetParentUnitType(string type)
    {
        parentUnitType = type;
    }

    public bool GetIsFlying()
    {
        return isFlying;
    }

    void Update()
    {
        if (isFlying)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, flySpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<UnitProperties>() != null)
        {
            if ((collider.GetComponent<UnitProperties>().unitType == "enemy" && parentUnitType == "friendly") || 
                (collider.GetComponent<UnitProperties>().unitType == "friendly" && parentUnitType == "enemy"))
            {
                collider.GetComponent<UnitProperties>().health -= attackPower;
                Destroy(gameObject);
            }
        }
    }

    public void LunchMissile(Vector3 position)
    {
        targetPosition = position;
        isFlying = true;
    }
}
