using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionScript : MonoBehaviour
{
    private MiniMapController miniMapController;
    private List<GameObject> prodactionObjectsQueue = new List<GameObject>();

    public int GetQueueLength()
    {
        return prodactionObjectsQueue.Count;
    }

    private float currentBuildTime;
    private float startBuildTime;

    private bool isWorking = false;

    public void AddObjectInProdQueue(GameObject objectToProd)
    {
        prodactionObjectsQueue.Add(objectToProd);
    }

    void Start()
    {
        miniMapController = GameObject.Find("Mini Map").GetComponent<MiniMapController>();
    }

    void FixedUpdate()
    {
        if (!isWorking && prodactionObjectsQueue.Count > 0)
        {
            isWorking = true;
            startBuildTime = Time.realtimeSinceStartup;
            currentBuildTime = prodactionObjectsQueue[0].GetComponentInChildren<UnitProperties>().buildTime;
            StartCoroutine(BuildUnit(prodactionObjectsQueue[0]));
        }
    }

    public float GetProductionPercentage()
    {
        return Mathf.Round((Time.realtimeSinceStartup - startBuildTime) * 100 / currentBuildTime);
    }

    IEnumerator BuildUnit(GameObject buildObject)
    {   
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

        yield return new WaitForSeconds(currentBuildTime);

        GameObject currentUnit = Instantiate(buildObject, placeToBuild, Quaternion.identity);
        //unitsOnStage.Add(currentUnit);
        miniMapController.AddIndicator(currentUnit);
        UnitsOnScene.AddUnit(currentUnit);

        prodactionObjectsQueue.RemoveAt(0);
        isWorking = false;
    }
}
