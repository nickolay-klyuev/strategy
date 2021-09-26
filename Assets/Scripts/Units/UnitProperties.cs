using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProperties : MonoBehaviour
{
    [SerializeField]
    private string unitName = "unknown";
    public string GetUnitName()
    {
        return unitName;
    }

    public float health = 100f;
    public float attackRange = 10f;
    public float attackSpeed = 10f; // attack every attackSpeed seconds
    public float accuracy = 10f;
    public float accuracyWhileMoving = 10f;
    public bool canFireWhileMoving = false;
    public bool autoAim = false;
    [SerializeField] private int cost = 50;
    public int GetCost()
    {
        return cost;
    }

    public float buildTime = 3f;
    public int limit = 1;
    public string unitType; // enemy, friendly
    public bool isBuilding = false;
    public bool hasDeathAnimation = false;
    public GameObject miniMapIndicator;

    [SerializeField]
    private string unitDescription = "no description";
    public string GetUnitDesc()
    {
        return unitDescription;
    }
}
