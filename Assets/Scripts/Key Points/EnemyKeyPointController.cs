using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKeyPointController : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnObjects;

    private MiniMapController miniMapController;

    private bool isBuilding = false;

    // Start is called before the first frame update
    void Start()
    {
        miniMapController = GameObject.Find("Mini Map").GetComponent<MiniMapController>();
    }

    private void FixedUpdate()
    {
        List<GameObject> buildings = UnitsOnScene.GetUnits("enemy;building");
        List<GameObject> resBuildings = new List<GameObject>();
        foreach (GameObject building in buildings)
        {
            if (building.tag == "Resource Gatherer")
            {
                resBuildings.Add(building);
            }
        }

        if (!isBuilding && (EnemyResourceScript.GetResourceAmount() > 250 || resBuildings.Count >= 3) && UnitsOnScene.GetUnits("enemy;unit").Count < EnemyResourceScript.GetUnitsLimit())
        {
            if (EnemyResourceScript.SpendResource(spawnObjects[0].GetComponentInChildren<UnitProperties>().cost))
            {
                StartCoroutine(BuildUnit(spawnObjects[0]));
            }
        }
    }

    IEnumerator BuildUnit(GameObject buildObject)
    {   
        isBuilding = true;

        Vector2 position = transform.position;
        Vector2 colliderSize = GetComponent<CircleCollider2D>().bounds.size / 2;
        
        Vector2 placeToBuild;
        if (Random.Range(0f, 1f) > 0.5f)
        {
            float[] xColliderSize = {-colliderSize.x, colliderSize.x};
            placeToBuild = new Vector2(position.x + xColliderSize[Random.Range(0, xColliderSize.Length)], position.y + Random.Range(-colliderSize.y, colliderSize.y));
        }
        else
        {
            float[] yColliderSize = {-colliderSize.y, colliderSize.y};
            placeToBuild = new Vector2(position.x + Random.Range(-colliderSize.x, colliderSize.x), position.y + yColliderSize[Random.Range(0, yColliderSize.Length)]);
        }

        yield return new WaitForSeconds(spawnObjects[0].GetComponentInChildren<UnitProperties>().buildTime);

        GameObject currentUnit = Instantiate(buildObject, placeToBuild, Quaternion.identity);
        miniMapController.AddIndicator(currentUnit);
        UnitsOnScene.AddUnit(currentUnit);

        isBuilding = false;
    }
}
