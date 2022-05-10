using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogPartController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        UnitProperties properties = collider.transform.parent.parent.GetComponent<UnitProperties>();
        if (properties != null && properties.unitType == "friendly")
        {
            gameObject.SetActive(false);
        }
    }
}
