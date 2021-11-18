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

    static private int skillPoints = 10;
    static public int GetSkillPoints()
    {
        return skillPoints;
    }
    static public bool SpendSkillPoint()
    {
        if (skillPoints > 0)
        {
            skillPoints--;
            return true;
        }
        else
        {
            Debug.Log("You don't have any skill points!");
            return false;
        }
    }

    static private float rateCoeff = (float)globalExp;

    static public float GetRateToNextLvl() // 1 = 100%
    {
        return ((float)globalExp - rateCoeff) / ((float)expForNextLvl - rateCoeff);
    }

    void FixedUpdate()
    {
        if (globalExp >= expForNextLvl)
        {
            currentLvl++;
            skillPoints++;
            expForNextLvl *= 2;
            rateCoeff = (float)globalExp;
        }
    }
}
