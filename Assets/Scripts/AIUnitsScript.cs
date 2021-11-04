using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUnitsScript : MonoBehaviour
{
    [SerializeField] private GameObject resourceBuildingPrefab;
    private int buildingCost;

    private List<ResourcePointScript> resourcePoints = new List<ResourcePointScript>();
    private MoveController move;

    // Start is called before the first frame update
    void Start()
    {
        buildingCost = resourceBuildingPrefab.GetComponent<UnitProperties>().cost;

        foreach(GameObject point in GameObject.FindGameObjectsWithTag("Resources Point"))
        {
            resourcePoints.Add(point.GetComponent<ResourcePointScript>());
        }

        move = GetComponent<MoveController>();
    }

    void FixedUpdate()
    {
        if (!move.GetIsChasing() && !move.GetIsMoving())
        {
            ResourcePointScript point = resourcePoints[Random.Range(0, resourcePoints.Count - 1)];

            if (EnemyResourceScript.GetResourceAmount() >= buildingCost && !point.DoesBuildingExist() && point.GetBuildingType() != "friendly")
            {
                move.MoveToPoint(point.transform.position);
            }
            else
            {
                List<GameObject> friendlyBuildings = UnitsOnScene.GetUnits("friendly;building");
                move.MoveToPoint(friendlyBuildings[Random.Range(0, friendlyBuildings.Count)].transform.position);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Resources Point")
        {
            move.StopMoving();
            collider.GetComponent<ResourcePointScript>().Build(resourceBuildingPrefab, "enemy");
        }
    }
}
