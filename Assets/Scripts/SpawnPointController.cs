using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public GameObject resourceSystem;

    public void SpawnObject(GameObject spawnObject)
    {
        int spawnObjectLimit = spawnObject.GetComponent<UnitProperties>().limit;

        if (GameObject.FindGameObjectsWithTag(spawnObject.name).Length < spawnObjectLimit) // only if unit limit allows
        {
            int cost = spawnObject.GetComponent<UnitProperties>().cost;
            if (resourceSystem.GetComponent<ResourceSystem>().SpendResource(cost))
            {
                Instantiate(spawnObject, transform.position, Quaternion.identity);
            }
        }
    }
}
