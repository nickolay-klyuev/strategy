using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawnerController : MonoBehaviour
{
    public GameObject missile;

    private Vector3 targetPosition;
    private List<GameObject> createdMissiles = new List<GameObject>();
    private string parentUnitType;
    
    // Start is called before the first frame update
    void Start()
    {
        parentUnitType = GetComponentInParent<UnitProperties>().unitType;
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
                    // TODO change hardcode 10 by normal mechanic
                    createdMissiles[i].transform.position = Vector3.MoveTowards(createdMissiles[i].transform.position, targetPosition, 10 * Time.deltaTime);
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
