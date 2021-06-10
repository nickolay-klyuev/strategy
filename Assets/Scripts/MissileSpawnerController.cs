using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawnerController : MonoBehaviour
{
    public GameObject missile;

    private Vector3 targetPosition;
    private List<GameObject> createdMissiles = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
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
                    createdMissiles[i].transform.position = Vector3.MoveTowards(createdMissiles[i].transform.position, targetPosition, 10 * Time.deltaTime);
                    i++;
                }
            }
        }
    }

    public void SpawnMissile(GameObject target)
    {
        targetPosition = target.transform.position;
        GameObject createdMissile = Instantiate(missile, transform.position, transform.rotation);
        Debug.Log(createdMissiles);
        createdMissiles.Add(createdMissile);
        Debug.Log(createdMissiles.Count);
    }
}
