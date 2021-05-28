using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeRadiusController : MonoBehaviour
{
    private UnitProperties unitProperties;
    private CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        unitProperties = transform.parent.GetComponent<UnitProperties>();
        circleCollider = GetComponent<CircleCollider2D>();

        //set attack range radius to collider 
        circleCollider.radius = unitProperties.attackRange;
    }
}
