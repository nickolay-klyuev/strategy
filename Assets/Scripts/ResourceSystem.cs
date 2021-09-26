using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    static private int resourceAmount = 1000;

    static public void GatherResource(int amount)
    {
        resourceAmount += amount;
    }

    static public bool SpendResource(int amount)
    {
        if (amount > resourceAmount)
        {
            Debug.Log("Not enough resources");
            return false;
        }
        else
        {
            resourceAmount -= amount;
            return true;
        }
    }

    static public int GetResourceAmount()
    {
        return resourceAmount;
    }

    void OnDestroy()
    {
        resourceAmount = 200;
    }
}
