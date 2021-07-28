using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnits : MonoBehaviour
{
    public static List<GameObject> selectedUnits = new List<GameObject>();

    public static void UnselectAll()
    {
        foreach (GameObject unit in selectedUnits)
        {
            unit.GetComponent<FriendlyMoveController>().SetIsSelected(false);
        }
        selectedUnits = new List<GameObject>();
    }
}
