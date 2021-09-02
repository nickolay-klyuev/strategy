using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDescMenuScript : MonoBehaviour
{
    public GameObject canvas;
    public GameObject miniDescMenuPrefab;

    private GameObject menuOnStage;

    // Start is called before the first frame update
    void Start()
    {
        menuOnStage = Instantiate(miniDescMenuPrefab, canvas.transform);
        menuOnStage.SetActive(false);
    }

    private void Update()
    {
        if (menuOnStage.activeSelf)
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
