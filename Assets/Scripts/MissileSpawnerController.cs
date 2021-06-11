using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawnerController : MonoBehaviour
{
    public GameObject missile;

    private Vector3 targetPosition;
    private List<GameObject> createdMissiles = new List<GameObject>();
    private string parentUnitType;
    private float parentAccuracy; 
    
    // Start is called before the first frame update
    void Start()
    {
        parentUnitType = GetComponentInParent<UnitProperties>().unitType;
        parentAccuracy = GetComponentInParent<UnitProperties>().accuracy;
    }

    // Update is called once per frame
    void Update()
    {
        if (createdMissiles.Count > 0)
        {
            for (int i = 0; i < createdMissiles.Count;)
            {
                if (createdMissiles[i] == null)
                {
                    createdMissiles.RemoveAt(i);
                }
                else
                {
                    MissileController missileController = createdMissiles[i].GetComponent<MissileController>();
                    if (!missileController.GetIsFlying())
                    {
                        // change target possition by accuracy 
                        targetPosition += new Vector3(Random.Range(-parentAccuracy, parentAccuracy), Random.Range(-parentAccuracy, parentAccuracy), 0);
                        missileController.LunchMissile(targetPosition);
                    }
                    i++;
                }
            }
        }
    }

    public void SpawnMissile(GameObject target)
    {
        if (target != null)
        {
            targetPosition = target.transform.position;
            GameObject createdMissile = Instantiate(missile, transform.position, transform.rotation);
            createdMissile.GetComponent<MissileController>().SetParentUnitType(parentUnitType);
            createdMissiles.Add(createdMissile);
        }
    }
}
