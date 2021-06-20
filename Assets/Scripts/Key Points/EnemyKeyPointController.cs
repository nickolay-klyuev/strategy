using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKeyPointController : MonoBehaviour
{
    public GameObject spawnObject;

    private Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.Find("Spawn Point");
        InvokeRepeating("Spawn", 1f, 1f);
    }

    private void Spawn()
    {
        if (GameObject.FindGameObjectsWithTag(spawnObject.name).Length < spawnObject.GetComponent<UnitProperties>().limit)
        {
            Instantiate(spawnObject, spawnPoint.position, Quaternion.identity);
        }
    }
}
