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
        List<FriendlyMoveController> selectedFMoveControllers = new List<FriendlyMoveController>();

        foreach (FriendlyMoveController friendlyMoveController in friendlyMoveControllers)
        {
            if (friendlyMoveController.GetIsSelected())
            {
                selectedFMoveControllers.Add(friendlyMoveController);
            }
        }

        int controllersCount = selectedFMoveControllers.Count;

        if (Input.GetMouseButtonDown(0))
        {
            selectBoxController.StartSelecting();
        }

        FriendlyMoveController previousFMController = selectedFMoveControllers[0];
        foreach (FriendlyMoveController selectedFMoveController in selectedFMoveControllers)
        {
            if (Input.GetMouseButtonDown(0))
            {
                selectedFMoveController.SetIsSelected(false);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Vector3 vector = previousFMController.transform.position - selectedFMoveController.transform.position;
                previousFMController = selectedFMoveController;

                for (int i = 0, j = 0; i < 1 && j < 100; j++) // TODO: change and improve later
                {
                    vector *= 0.9f;

                    float vectorLength = Mathf.Sqrt(Mathf.Pow(vector.x, 2) + Mathf.Pow(vector.y, 2));

                    if (vectorLength <= 3f && vectorLength >= 2f)
                    {
                        i++;
                    }
                    else
                    {
                        vector *= 1.1f;
                    }
                }

                selectedFMoveController.MoveToPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition) - vector);
            }
        }
    }
}
