using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public GameObject spawnObject;
    public GameObject resourceSystem;

    private int spawnObjectLimit;

    // Start is called before the first frame update
    void Start()
    {
        spawnObjectLimit = spawnObject.GetComponent<UnitProperties>().limit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject()
    {
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
