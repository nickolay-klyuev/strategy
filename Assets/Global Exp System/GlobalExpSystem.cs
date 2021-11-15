using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalExpSystem : MonoBehaviour
{
    static private int globalExp = 0;
    static public void AddExp(int amount)
    {
        globalExp += amount;
    }
    static public int GetExp()
    {
        return globalExp;
    }

    static private int expForNextLvl = 1000;
    static private int currentLvl = 0;
    static public int GetCurrentLvl()
    {
        return currentLvl;
    }

    static public float GetRateToNextLvl() // 1 = 100%
    {
        return (float)globalExp / (float)expForNextLvl;
    }

    void FixedUpdate()
    {
        if (globalExp >= expForNextLvl)
        {
            currentLvl++;
            expForNextLvl *= 2;
        }
    }
}
