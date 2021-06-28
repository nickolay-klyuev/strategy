using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    private UnitProperties unitProperties;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponentInChildren<UnitProperties>() != null)
        {
            unitProperties = GetComponentInChildren<UnitProperties>();
        }
        else
        {
            unitProperties = GetComponent<UnitProperties>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (unitProperties.health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
