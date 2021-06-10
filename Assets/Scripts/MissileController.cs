using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float attackPower = 10f;

    private string parentUnitType;

    public void SetParentUnitType(string type)
    {
        parentUnitType = type;
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
}
