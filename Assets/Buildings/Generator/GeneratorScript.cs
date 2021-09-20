using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public int resourceAmount = 10;
    public float gatherTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GatherResource", gatherTime, gatherTime);
    }

    private void GatherResource()
    {
        ResourceSystem.GatherResource(resourceAmount);
    }
}
