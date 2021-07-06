using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public int resourceAmount = 10;
    public float gatherTime = 10f;

    private GameObject resourceSystem;

    // Start is called before the first frame update
    void Start()
    {
        resourceSystem = GameObject.Find("ResourceSystem");

        InvokeRepeating("GatherResource", gatherTime, gatherTime);
    }

    private void GatherResource()
    {
        resourceSystem.GetComponent<ResourceSystem>().GatherResource(resourceAmount);
    }
}
