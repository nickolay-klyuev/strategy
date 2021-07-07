using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlaceScript : MonoBehaviour
{
    private bool canBeBuild = true;
    public bool GetCanBeBuild()
    {
        return canBeBuild;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        Debug.Log(collider);
        canBeBuild = false;
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnTriggerExit2D()
    {
        canBeBuild = true;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
