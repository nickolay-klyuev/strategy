using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniDescMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject miniDescMenuPrefab;

    [SerializeField]
    private  bool isBuildDesc = false;

    [SerializeField]
    private GameObject unitPrefab;
    public GameObject GetUnitPrefab()
    {
        return unitPrefab;
    }

    private GameObject menuOnStage;
    private MiniDescInfoShowScript descInfoShowScript;
    private UnitProperties unitProperties;

    private bool canBeShowed = true;
    public void ForbidShowing()
    {
        canBeShowed = false;
    }
    public void AllowShowing()
    {
        canBeShowed = true;
    }

    void Awake()
    {
        menuOnStage = Instantiate(miniDescMenuPrefab, GameObject.Find("Canvas").transform);

        descInfoShowScript = menuOnStage.GetComponent<MiniDescInfoShowScript>();
        unitProperties = GetComponent<UnitProperties>();
        if (unitProperties == null)
        {
            unitProperties = unitPrefab.GetComponentInChildren<UnitProperties>();
        }

        if (descInfoShowScript != null)
        {
            descInfoShowScript.SetDescriptionText("");

            if (isBuildDesc)
            {
                descInfoShowScript.AddDescriptionText($"Costs: {unitProperties.cost}   Build time: {unitProperties.buildTime}\n");
            }

            descInfoShowScript.AddDescriptionText($"{unitProperties.GetUnitName()} \nMaxHP: {unitProperties.health}");

            if (GetComponent<RegenerationScript>() != null)
            {
                descInfoShowScript.AddDescriptionText("\n- regeneration");
            }

            descInfoShowScript.AddDescriptionText($"\n{unitProperties.GetUnitDesc()}");
        }

        menuOnStage.SetActive(false);
    }

    private void Update()
    {
        if (menuOnStage.activeSelf && isBuildDesc) // cling to mouse is desc for building
        {
            RectTransform menuTransform = menuOnStage.GetComponent<RectTransform>();
            menuTransform.position = new Vector2(Input.mousePosition.x + menuTransform.sizeDelta.x/2, Input.mousePosition.y + menuTransform.sizeDelta.y/2 + 5f);
        }
    }

    private void OnMouseOver()
    {
        if (canBeShowed)
        {
            menuOnStage.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        menuOnStage.SetActive(false);
    }

    // for UI elements
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canBeShowed)
        {
            menuOnStage.transform.SetAsLastSibling();
            menuOnStage.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menuOnStage.SetActive(false);
    }

    void OnDestroy()
    {
        Destroy(menuOnStage);
    }
}
