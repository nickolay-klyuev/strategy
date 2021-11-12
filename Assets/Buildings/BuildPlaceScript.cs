using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlaceScript : MonoBehaviour
{
    private bool canBeBuild = true;
    private bool inBuildRadius = false;
    public bool GetCanBeBuild()
    {
        return canBeBuild;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Build Radius"))
        {
            inBuildRadius = true;
            AllowBuild();
        }

        if (collider.name != "Attack Range Collider" && !collider.CompareTag("Build Radius"))
        {
            ForbidBuild();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (!collider.CompareTag("Build Radius") && inBuildRadius)
        {
            AllowBuild();
        }
        else
        {
            inBuildRadius = false;
            ForbidBuild();
        }
    }

    private void AllowBuild()
    {
        canBeBuild = true;
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void ForbidBuild()
    {
        canBeBuild = false;
        GetComponent<SpriteRenderer>().color = Color.red;
    }
}
