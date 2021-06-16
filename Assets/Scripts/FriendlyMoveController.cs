using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyMoveController : MonoBehaviour
{
    private MoveController moveController;

    private bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        moveController = GetComponent<MoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        isSelected = true;
    }

    public void SetIsSelected(bool selected)
    {
        isSelected = selected;
    }

    public bool GetIsSelected()
    {
        return isSelected;
    }

    public void MoveToPoint(Vector3 point)
    {
        moveController.MoveToPoint(point);
    }
}
