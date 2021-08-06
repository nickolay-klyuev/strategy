using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKeyPointController : MonoBehaviour
{
    public GameObject spawnObject;

    private Transform spawnPoint;
    private MiniMapController miniMapController;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.Find("Spawn Point");
        miniMapController = GameObject.Find("Mini Map").GetComponent<MiniMapController>();
        InvokeRepeating("Spawn", 5f, 5f);
    }

    private void Spawn()
    {
        if (GameObject.FindGameObjectsWithTag(spawnObject.name).Length < spawnObject.GetComponent<UnitProperties>().limit)
        {
            GameObject newUnit = Instantiate(spawnObject, spawnPoint.position, Quaternion.identity);
            miniMapController.AddIndicator(newUnit, UnitsOnScene.AllCount());
            UnitsOnScene.AddUnit(newUnit);
        }
    }
}
