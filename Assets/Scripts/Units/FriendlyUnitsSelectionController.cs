using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyUnitsSelectionController : MonoBehaviour
{
    private bool isSelected = false;

    void OnMouseUp()
    {
        SelectedUnits.UnselectAll();
        isSelected = true;
        SelectedUnits.selectedUnits.Add(gameObject);
    }

    public void SetIsSelected(bool selected)
    {
        isSelected = selected;
    }

    public bool GetIsSelected()
    {
        return isSelected;
    }
}
