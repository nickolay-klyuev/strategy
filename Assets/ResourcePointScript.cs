using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePointScript : MonoBehaviour
{
    private Collider2D[] unitsNearPoint;
    private bool canBeBuild = false;

    private MiniDescMenuScript showDescScript;
    private MiniMapController miniMap;

    private GameObject resourceBuilding;
    private bool doesBuildingExist = false;
    public bool DoesBuildingExist()
    {
        return doesBuildingExist;
    }

    private string buildingType;
    public string GetBuildingType()
    {
        return buildingType;
    }

    // Start is called before the first frame update
    void Start()
    {
        showDescScript = GetComponent<MiniDescMenuScript>();
        miniMap = GameObject.Find("Mini Map").GetComponent<MiniMapController>();
    }

    void OnMouseDown()
    {
        if (canBeBuild)
        {
            Build();
        }
    }

    public void Build(GameObject building = null, string type = "friendly")
    {
        if (type == "friendly")
        {
            if (!doesBuildingExist && ResourceSystem.SpendResource(showDescScript.GetUnitPrefab().GetComponent<UnitProperties>().cost))
            {
                buildingType = type;
                resourceBuilding = Instantiate(showDescScript.GetUnitPrefab(), transform.position + new Vector3(0, 0, -1), Quaternion.identity);
            }
        }
        else
        {
            if (!doesBuildingExist && EnemyResourceScript.SpendResource(building.GetComponent<UnitProperties>().cost))
            {
                buildingType = type;
                resourceBuilding = Instantiate(building, transform.position + new Vector3(0, 0, -1), Quaternion.identity);
            }
        }
        UnitsOnScene.AddUnit(resourceBuilding);
        miniMap.AddIndicator(resourceBuilding);
    }

    void FixedUpdate()
    {
        if (resourceBuilding != null)
        {
            doesBuildingExist = true;
        }
        else
        {
            doesBuildingExist = false;
            buildingType = null;
        }

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
