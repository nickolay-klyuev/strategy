using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackroundController : MonoBehaviour
{
    private MoveController[] moveControllers;
    private SelectBoxController selectBoxController;

    // Start is called before the first frame update
    void Start()
    {
        moveControllers = FindObjectsOfType<MoveController>();
        selectBoxController = FindObjectOfType<SelectBoxController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        foreach(MoveController moveController in moveControllers)
        {
            if (Input.GetMouseButtonDown(0))
            {
                moveController.SetIsSelected(false);
                selectBoxController.StartSelecting();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (moveController.GetIsSelected())
                {
                    moveController.MoveToPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                selectBoxController.StopSelecting();
            }
        }
    }
}
