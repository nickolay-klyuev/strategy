using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawnerController : MonoBehaviour
{
    public GameObject missile;

    private Vector3 targetPosition;
    private bool doSpawn = false;
    private List<GameObject> createdMissiles = new List<GameObject>();
    private string parentUnitType;
    private float parentAccuracy;
    private float parentAccuracyWhileMoving;
    private MoveController moveController;
    
    // Start is called before the first frame update
    void Start()
    {
        moveController = GetComponentInParent<MoveController>();
        parentUnitType = GetComponentInParent<UnitProperties>().unitType;
        parentAccuracy = GetComponentInParent<UnitProperties>().accuracy;
        parentAccuracyWhileMoving = GetComponentInParent<UnitProperties>().accuracyWhileMoving;

        // create 2 missiles, just in case
        for (int i = 0; i < 2; i++)
        {
            GameObject createdMissile = Instantiate(missile, transform.position, transform.rotation);
            createdMissile.GetComponent<MissileController>().SetParentUnitType(parentUnitType);
            createdMissile.SetActive(false);
            createdMissiles.Add(createdMissile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doSpawn)
        {
            for (int i = 0; i < createdMissiles.Count; i++)
            {
                if (createdMissiles[i].activeSelf)
                {
                    continue;
                }
                createdMissiles[i].transform.position = transform.position;
                createdMissiles[i].transform.rotation = transform.rotation;
                createdMissiles[i].SetActive(true);
                MissileController missileController = createdMissiles[i].GetComponent<MissileController>();
                if (!missileController.GetIsFlying())
                {
                    // change target possition by accuracy 
                    if (moveController.GetIsMoving())
                    {
                        targetPosition += new Vector3(Random.Range(-parentAccuracyWhileMoving, parentAccuracyWhileMoving), 
                                                        Random.Range(-parentAccuracyWhileMoving, parentAccuracyWhileMoving), 0);
                    }
                    else
                    {
                        targetPosition += new Vector3(Random.Range(-parentAccuracy, parentAccuracy), Random.Range(-parentAccuracy, parentAccuracy), 0);
                    }
                    missileController.LunchMissile(targetPosition);
                }
                doSpawn = false;
                break;
            }
        }
    }

    public void SpawnMissile(GameObject target)
    {
        if (target != null)
        {
            targetPosition = target.transform.position;
            doSpawn = true;
        }
    }
}
