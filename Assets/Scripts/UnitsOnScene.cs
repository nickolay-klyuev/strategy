using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsOnScene : MonoBehaviour
{
    public List<GameObject> initialUnits;

    private static List<GameObject> friendlyUnits = new List<GameObject>();
    private static List<GameObject> friendlyBuildings = new List<GameObject>();
    private static List<GameObject> enemyUnits = new List<GameObject>();
    private static List<GameObject> enemyBuildings = new List<GameObject>();
    private static List<GameObject> allUnits = new List<GameObject>();

    public static List<GameObject> GetAllUnits()
    {
        return allUnits;
    }

    public static int AllCount()
    {
        return allUnits.Count;
    }

    public static void ClearAllUnits()
    {
        allUnits = new List<GameObject>();
        friendlyUnits = new List<GameObject>();
        friendlyBuildings = new List<GameObject>();
        enemyUnits = new List<GameObject>();
        enemyBuildings = new List<GameObject>();
    }

    public static List<GameObject> GetUnits(string type)
    {
        switch (type)
        {
            case "friendly;building":
                return friendlyBuildings;
            case "friendly;unit":
                return friendlyUnits;
            case "enemy;building":
                return enemyBuildings;
            case "enemy;unit":
                return enemyUnits;
            default:
                return new List<GameObject>();
        }
    }

    public static void RemoveUnit(GameObject unit)
    {
        allUnits.Remove(unit);

        switch (GetUnitType(unit))
        {
            case "friendly;building":
                friendlyBuildings.Remove(unit);
                break;
            case "friendly;unit":
                friendlyUnits.Remove(unit);
                break;
            case "enemy;building":
                enemyBuildings.Remove(unit);
                break;
            case "enemy;unit":
                enemyUnits.Remove(unit);
                break;
            default:
                Debug.LogWarning("UnknownUnitType during removing");
                break;
        }
    }

    public static void AddUnit(GameObject unit)
    {
        allUnits.Add(unit);

        switch (GetUnitType(unit))
        {
            case "friendly;building":
                friendlyBuildings.Add(unit);
                break;
            case "friendly;unit":
                friendlyUnits.Add(unit);
                break;
            case "enemy;building":
                enemyBuildings.Add(unit);
                break;
            case "enemy;unit":
                enemyUnits.Add(unit);
                break;
            default:
                Debug.LogWarning("UnknownUnitType during adding");
                break;
        }
    }

    private void Awake() // need Awake to run before Start in MiniMapController
    {
        ClearAllUnits(); // clear static Lists after scene reload

        foreach (GameObject unit in initialUnits)
        {
            AddUnit(unit);
        }
    }

    private static string GetUnitType(GameObject unit)
    {
        UnitProperties properties = unit.GetComponent<UnitProperties>();
        if (properties == null)
        {
            properties = unit.GetComponentInChildren<UnitProperties>();
        }

        string unitType = "";

        if (properties != null && properties.unitType == "friendly")
        {
            unitType += "friendly";
        }
        else if (properties != null && properties.unitType == "enemy")
        {
            unitType += "enemy";
        }

        unitType += ";";

        if (properties.isBuilding == true)
        {
            unitType += "building";
        }
        else if (properties.isBuilding == false)
        {
            unitType += "unit";
        }

        return unitType;
    }
}
