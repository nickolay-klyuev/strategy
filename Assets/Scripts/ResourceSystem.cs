using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    private int resourceAmount = 100;
    private int gatherAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GatherResource", 1f, 1f);
    }

    private void GatherResource()
    {
        resourceAmount += gatherAmount;
    }

    public bool SpendResource(int amount)
    {
        if (amount > resourceAmount)
        {
            return false;
        }
        else
        {
            resourceAmount -= amount;
            return true;
        }
    }

    public int GetResourceAmount()
    {
        return resourceAmount;
    }
}
