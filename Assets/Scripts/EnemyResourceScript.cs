using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResourceScript : MonoBehaviour
{
    static private int unitsLimit = 15;

    static public int GetUnitsLimit()
    {
        return unitsLimit;
    }

    static private int resourceAmount = 1000;

    void FixedUpdate()
    {
        //Debug.Log(resourceAmount);
    }

    static public int GetResourceAmount()
    {
        return resourceAmount;
    }

    static public void GatherResource(int amount)
    {
        resourceAmount += amount;
    }

    static public bool SpendResource(int amount)
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
}
