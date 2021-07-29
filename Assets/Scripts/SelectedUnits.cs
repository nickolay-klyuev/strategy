using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnits : MonoBehaviour
{
    public static List<GameObject> selectedUnits = new List<GameObject>();

    // FIXME:
    // if unit was destroyed during selecting then this fuction dives null error
    public static void UnselectAll()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            foreach (GameObject unit in selectedUnits)
            {
                unit.GetComponent<FriendlyMoveController>().SetIsSelected(false);
            }
            selectedUnits = new List<GameObject>();
        }
    }
}
