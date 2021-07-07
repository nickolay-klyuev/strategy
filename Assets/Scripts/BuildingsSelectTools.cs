using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsSelectTools : MonoBehaviour
{
    public GameObject buildMenuUI;

    private bool isSelected = false;
    public bool GetIsSelected()
    {
        return isSelected;
    }

    private void Start()
    {
        buildMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isSelected && GameObject.ReferenceEquals(gameObject, StaticMethods.GetGameObjectByRaycast()))
            {
                isSelected = true;
                buildMenuUI.SetActive(true);
            }
            else if (!buildMenuUI.GetComponent<IsPanelActive>().GetIsActive() && !buildMenuUI.GetComponentInChildren<BuildingScript>().GetIsBuilding())
            {
                bool isSomeBuildingActive = false;
                foreach (BuildingScript buildingScript in buildMenuUI.GetComponentsInChildren<BuildingScript>())
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
                    buildMenuUI.SetActive(false);
                }
            }
        }
    }
}
