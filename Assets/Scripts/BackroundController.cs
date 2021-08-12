using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackroundController : MonoBehaviour
{
    public AudioClip rightBtnClickSound;

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
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x > MiniMapController.miniMapWidth || 
                Input.mousePosition.y < Screen.height - MiniMapController.miniMapHeight) // ignore selection if mouse is on minimap
            {
                SelectedUnits.UnselectAll();
                selectBoxController.StartSelecting();
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            GetComponent<AudioSource>().PlayOneShot(rightBtnClickSound, 1f);
            foreach (GameObject unit in SelectedUnits.selectedUnits)
            {
                unit.GetComponent<MoveController>().MoveToPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
}
