using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitProperties : MonoBehaviour
{
    public float health = 100f;
    public float attackRange = 10f;
    public float attackPower = 10f;
    public float attackSpeed = 10f; //attack every attackSpeed seconds

    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
