using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMetaData : MonoBehaviour
{
    private GameObject callObject;

    public void SetCallObject(GameObject caller)
    {
        callObject = caller;
    }

    public GameObject GetCallObject()
    {
        return callObject;
    }
}
