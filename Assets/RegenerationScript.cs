using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenerationScript : MonoBehaviour
{
    public float regenerationAmount = 10f;
    public float regenerationTime = 1f;

    private UnitProperties properties;
    private float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        properties = GetComponent<UnitProperties>();
        maxHealth = properties.health;

        StartCoroutine(Heal());
    }

    IEnumerator Heal()
    {
        yield return new WaitForSeconds(regenerationTime);

        if (properties.health <= maxHealth - regenerationAmount)
        {
            properties.health += regenerationAmount;
        }
        else if (properties.health > maxHealth - regenerationAmount && properties.health < maxHealth)
        {
            properties.health = maxHealth;
        }

        StartCoroutine(Heal());
    }
}
