using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public int resourceAmount = 10;
    public float gatherTime = 10f;

    private UnitProperties properties;

    // Start is called before the first frame update
    void Start()
    {
        properties = GetComponent<UnitProperties>();

        InvokeRepeating("GatherResource", gatherTime, gatherTime);
    }

    private void GatherResource()
    {
        if (properties.unitType == "enemy")
        {
            EnemyResourceScript.GatherResource(resourceAmount);
        }
        else
        {
            ResourceSystem.GatherResource(resourceAmount);
        }
    }
}
