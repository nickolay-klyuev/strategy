using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePointScript : MonoBehaviour
{
    private Collider2D[] unitsNearPoint;
    private bool canBeBuild = false;

    private MiniDescMenuScript showDescScript;

    // Start is called before the first frame update
    void Start()
    {
        showDescScript = GetComponent<MiniDescMenuScript>();
    }

    void OnMouseDown()
    {
        if (canBeBuild)
        {
            if (ResourceSystem.SpendResource(showDescScript.GetUnitPrefab().GetComponent<UnitProperties>().GetCost()))
            {
                Instantiate(showDescScript.GetUnitPrefab(), transform.position + new Vector3(0, 0, -1), Quaternion.identity);
            }
        }
    }

    void FixedUpdate()
    {
        // if enemies are near point, you can't build
        canBeBuild = false;
        unitsNearPoint = Physics2D.OverlapCircleAll(transform.position, 5f);
        foreach (Collider2D unit in unitsNearPoint)
        {
            UnitProperties properties = unit.GetComponent<UnitProperties>();
            if (properties != null && properties.unitType == "enemy")
            {
                canBeBuild = false;
                break;
            }
            else if (properties != null && properties.unitType == "friendly")
            {
                canBeBuild = true;
            }
        }

        if (canBeBuild)
        {
            showDescScript.AllowShowing();
        }
        else
        {
            showDescScript.ForbidShowing();
        }
    }
}
