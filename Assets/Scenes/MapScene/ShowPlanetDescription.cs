using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPlanetDescription : MonoBehaviour
{
    [SerializeField] private GameObject descToShow;
    
    private GameObject descOnStage;

    // Start is called before the first frame update
    void Start()
    {
        descOnStage = Instantiate(descToShow, GameObject.Find("Canvas").transform);
        descOnStage.SetActive(false);
    }

    private void Update()
    {
        RectTransform menuTransform = descOnStage.GetComponent<RectTransform>();
        menuTransform.position = new Vector2(Input.mousePosition.x + menuTransform.sizeDelta.x/2, Input.mousePosition.y + menuTransform.sizeDelta.y/2 + 5f);
    }

    private void OnMouseOver()
    {
        descOnStage.SetActive(true);
    }

    private void OnMouseExit()
    {
        descOnStage.SetActive(false);
    }
}
