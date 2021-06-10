using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float attackPower = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<UnitProperties>() != null)
        {
            if (collider.GetComponent<UnitProperties>().unitType == "enemy")
            {
                collider.GetComponent<UnitProperties>().health -= attackPower;
                Destroy(gameObject);
            }
        }
    }
}
