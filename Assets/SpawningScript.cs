using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawningScript : MonoBehaviour
{
    public GameObject unitToSpawn;
    private int cost;

    // Start is called before the first frame update
    void Start()
    {
        cost = unitToSpawn.GetComponentInChildren<UnitProperties>().cost;
        GetComponentInChildren<Text>().text += "(" + cost + ")";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnUnit()
    {
        if (GameObject.FindGameObjectWithTag("ResourceSystem").GetComponent<ResourceSystem>().SpendResource(cost))
        {
            Instantiate(unitToSpawn, GetComponentInParent<PanelMetaData>().GetCallObject().transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Not enough resources");
        }
    }
}
