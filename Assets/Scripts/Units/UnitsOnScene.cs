using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsOnScene : MonoBehaviour
{
    public List<GameObject> initialUnits;

    private List<GameObject> friendlyUnits = new List<GameObject>();
    private List<GameObject> friendlyBuildings = new List<GameObject>();
    private List<GameObject> enemyUnits = new List<GameObject>();
    private List<GameObject> enemyBuildings = new List<GameObject>();

    private void Start()
    {
        foreach (GameObject unit in initialUnits)
        {
            UnitProperties properties = unit.GetComponent<UnitProperties>();
            if (properties.unitType == "friendly")
            {
                if (properties.isBuilding == true)
                {
                    friendlyBuildings.Add(unit);
                }
                else
                {
                    friendlyUnits.Add(unit);
                }
            }
            else if (properties.unitType == "enemy")
            {
                if (properties.isBuilding == true)
                {
                    enemyBuildings.Add(unit);
                }
                else
                {
                    enemyUnits.Add(unit);
                }
            }
        }
    }
}
