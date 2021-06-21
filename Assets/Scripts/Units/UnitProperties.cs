using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProperties : MonoBehaviour
{
    public float health = 100f;
    public float attackRange = 10f;
    public float attackSpeed = 10f; // attack every attackSpeed seconds
    public float accuracy = 10f;
    public float accuracyWhileMoving = 10f;
    public bool canFireWhileMoving = false;
    public int cost = 50;
    public int limit = 1;
    public string unitType; // enemy, friendly

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
