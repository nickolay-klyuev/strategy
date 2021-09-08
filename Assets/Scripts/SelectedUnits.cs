using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnits : MonoBehaviour
{
    public static List<GameObject> selectedUnits = new List<GameObject>();

    public static void UnselectAll()
    {
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            foreach (GameObject unit in selectedUnits)
            {
                // skip if unit is already destroyed
                if (unit != null)
                {
                    unit.GetComponent<FriendlyUnitsSelectionController>().SetIsSelected(false);
                }
            }
            // clean all selected List
            selectedUnits = new List<GameObject>();
        }
    }
}
