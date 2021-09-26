using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonController : MonoBehaviour
{
    public GameObject spawnObject;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<Text>().text = spawnObject.name;
        transform.Find("Cost").GetComponent<Text>().text = spawnObject.GetComponentInChildren<UnitProperties>().GetCost().ToString();
        transform.Find("Limit").GetComponent<Text>().text = $"/ {spawnObject.GetComponentInChildren<UnitProperties>().limit.ToString()}";
    }

    void FixedUpdate()
    {
        transform.Find("Units Amount").GetComponent<Text>().text = GameObject.FindGameObjectsWithTag(spawnObject.name).Length.ToString();
    }
}
