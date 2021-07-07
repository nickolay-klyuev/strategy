using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    private int resourceAmount = 500;

    public void GatherResource(int amount)
    {
        resourceAmount += amount;
    }

    public bool SpendResource(int amount)
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

    public int GetResourceAmount()
    {
        return resourceAmount;
    }
}
