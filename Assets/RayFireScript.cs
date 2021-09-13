using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayFireScript : MonoBehaviour
{
    public float rayPower = 40f;

    private UnitProperties unitProperties;
    private CapsuleCollider2D thisCollider;

    void Start()
    {
        unitProperties = GetComponentInParent<UnitProperties>();
        thisCollider = GetComponent<CapsuleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        UnitProperties hitProperties = collider.GetComponentInChildren<UnitProperties>();
        if (hitProperties != null && ((hitProperties.unitType == "enemy" && unitProperties.unitType == "friendly") || 
            (hitProperties.unitType == "friendly" && unitProperties.unitType == "enemy")))
        {
            hitProperties.health -= rayPower;
        }
    }

    public void FireByRay()
    {
        thisCollider.size = new Vector2(thisCollider.size.x, unitProperties.attackRange);
        Debug.Log(thisCollider.size);
    }
}