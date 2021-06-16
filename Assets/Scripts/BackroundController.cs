using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackroundController : MonoBehaviour
{
    private FriendlyMoveController[] friendlyMoveControllers;
    private SelectBoxController selectBoxController;

    // Start is called before the first frame update
    void Start()
    {
        selectBoxController = FindObjectOfType<SelectBoxController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            selectBoxController.StopSelecting();
        }
    }

    void OnMouseOver()
    {
        friendlyMoveControllers = FindObjectsOfType<FriendlyMoveController>();
        foreach(FriendlyMoveController friendlyMoveController in friendlyMoveControllers)
        {
            if (Input.GetMouseButtonDown(0))
            {
                friendlyMoveController.SetIsSelected(false);
                selectBoxController.StartSelecting();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (friendlyMoveController.GetIsSelected())
                {
                    friendlyMoveController.MoveToPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
        }
    }
}
