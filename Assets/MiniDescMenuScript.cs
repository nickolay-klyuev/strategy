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
            menuOnStage.GetComponent<RectTransform>().position = Input.mousePosition;
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
