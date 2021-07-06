using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsSelectTools : MonoBehaviour
{
    private bool isSelected = false;
    public bool GetIsSelected()
    {
        return isSelected;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameObject.ReferenceEquals(gameObject, StaticMethods.GetGameObjectByRaycast()))
            {
                isSelected = true;
            }
            else
            {
                isSelected = false;
            }
        }
    }
}
