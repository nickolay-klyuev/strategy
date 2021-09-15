using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDescMenuScript : MonoBehaviour
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
    }

    // Start is called before the first frame update
    void Start()
    {
        menuOnStage.SetActive(false);   
    }

    private void Update()
    {
        if (menuOnStage.activeSelf && isMouseCling)
        {
            RectTransform menuTransform = menuOnStage.GetComponent<RectTransform>();
            menuTransform.position = new Vector2(Input.mousePosition.x + menuTransform.sizeDelta.x/2, Input.mousePosition.y);
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
}
