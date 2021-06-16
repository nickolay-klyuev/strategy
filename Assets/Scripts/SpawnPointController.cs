using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public GameObject spawnObject;
    //public float spawnRate = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject()
    {
        Instantiate(spawnObject, transform.position, Quaternion.identity);
    }
}
