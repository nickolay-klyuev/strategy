using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyUnitsSelectionController : MonoBehaviour
{
    private bool isSelected = false;

    void OnMouseDown()
    {
        SelectedUnits.UnselectAll();
    }

    void OnMouseUp()
    {
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
