using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniDescMenuScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject miniDescMenuPrefab;
    public bool isMouseCling  = true;

    private GameObject menuOnStage;
    private MiniDescInfoShowScript descInfoShowScript;
    private UnitProperties unitProperties;

    void Awake()
    {
        menuOnStage = Instantiate(miniDescMenuPrefab, GameObject.Find("Canvas").transform);

        descInfoShowScript = menuOnStage.GetComponent<MiniDescInfoShowScript>();
        unitProperties = GetComponent<UnitProperties>();

        if (descInfoShowScript != null)
        {
            descInfoShowScript.SetDescriptionText($"{unitProperties.GetUnitName()} \nMaxHP: {unitProperties.health}");

            if (GetComponent<RegenerationScript>() != null)
            {
                descInfoShowScript.AddDescriptionText("\n- regeneration");
            }
        }

        menuOnStage.SetActive(false);
    }

    private void Update()
    {
        if (menuOnStage.activeSelf && isMouseCling)
        {
            RectTransform menuTransform = menuOnStage.GetComponent<RectTransform>();
            menuTransform.position = new Vector2(Input.mousePosition.x + menuTransform.sizeDelta.x/2, Input.mousePosition.y + menuTransform.sizeDelta.y/2);
        }
    }

    private void OnMouseOver()
    {
        menuOnStage.SetActive(true);
    }

    private void OnMouseExit()
    {
        menuOnStage.SetActive(false);
    }

    // for UI elements
    public void OnPointerEnter(PointerEventData eventData)
    {
        menuOnStage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menuOnStage.SetActive(false);
    }
}
