using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsSelectTools : MonoBehaviour
{
    public GameObject buildMenuUI;
    private GameObject thisMenu;

    private bool isSelected = false;
    public bool GetIsSelected()
    {
        return isSelected;
    }

    private void Start()
    {
        thisMenu = Instantiate(buildMenuUI);
        thisMenu.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        thisMenu.GetComponent<RectTransform>().anchoredPosition = buildMenuUI.GetComponent<RectTransform>().anchoredPosition;

        thisMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isSelected && GameObject.ReferenceEquals(gameObject, StaticMethods.GetGameObjectByRaycast()))
            {
                isSelected = true;
                thisMenu.SetActive(true);
            }
            else if (!thisMenu.GetComponent<IsPanelActive>().GetIsActive() && !thisMenu.GetComponentInChildren<BuildingScript>().GetIsBuilding())
            {
                bool isSomeBuildingActive = false;
                foreach (BuildingScript buildingScript in thisMenu.GetComponentsInChildren<BuildingScript>())
                {
                    if (buildingScript.GetIsBuilding())
                    {
                        isSomeBuildingActive = true;
                        break;
                    }
                }

                if (!isSomeBuildingActive)
                {
                    isSelected = false;
                    thisMenu.SetActive(false);
                }
            }
        }
    }
}
