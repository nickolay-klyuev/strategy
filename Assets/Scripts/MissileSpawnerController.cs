using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawnerController : MonoBehaviour
{
    public GameObject missile;

    private GameObject createdMissile;
    private Vector3 targetPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (createdMissile != null)
        {
            createdMissile.transform.position = Vector3.MoveTowards(createdMissile.transform.position, targetPosition, 10 * Time.deltaTime);
        }
    }

    public void SpawnMissile(GameObject target)
    {
        targetPosition = target.transform.position;
        createdMissile = Instantiate(missile, transform.position, transform.rotation);
    }
}
