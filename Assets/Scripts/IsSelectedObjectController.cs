using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSelectedObjectController : MonoBehaviour
{
    private string unitType;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        unitType = GetComponentInParent<UnitProperties>().unitType;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // maybe remove from this controller later
        if (GetComponentInParent<MoveController>().GetIsSelected() && unitType == "friendly")
        {
            spriteRenderer.enabled = true;
        }
        else if (unitType == "friendly")
        {
            spriteRenderer.enabled = false;
        }
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
