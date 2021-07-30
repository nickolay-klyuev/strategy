using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapController : MonoBehaviour, IPointerClickHandler
{
    public GameObject mainCamera;
    public List<GameObject> initialUnits;
    public GameObject friendlyUnitIndicator;

    private List<Transform> unitsCores = new List<Transform>();
    private GameObject background;
    private RectTransform rectTransform;
    private List<RectTransform> indicatorsRT = new List<RectTransform>();
    private RectTransform cameraIndicator;

    private float scale = 10;

    public void OnPointerClick(PointerEventData pointerEventData) // move camera by clicking mini map
    {
        mainCamera.transform.position = new Vector3(pointerEventData.position.x / scale, 
                                                    (pointerEventData.position.y - (transform.position.y - rectTransform.sizeDelta.y / 2)) / scale,
                                                    mainCamera.transform.position.z);
    }

    // Start is called before the first frame update
    private void Start()
    {
        background = GameObject.Find("Background");
        rectTransform = transform.GetComponent<RectTransform>();
        cameraIndicator = transform.Find("Camera indicator").GetComponent<RectTransform>();

        Vector2 bgSize = background.GetComponent<SpriteRenderer>().size;
        rectTransform.sizeDelta = bgSize * scale;

        rectTransform.anchoredPosition = new Vector2(rectTransform.sizeDelta.x / 2, rectTransform.sizeDelta.y / -2);

        cameraIndicator.anchorMin = new Vector2(0, 0);
        cameraIndicator.anchorMax = new Vector2(0, 0);

        for (int i = 0; i < initialUnits.Count; i++)
        {
            unitsCores.Add(initialUnits[i].transform.Find("Core"));
            
            GameObject indicator = Instantiate(friendlyUnitIndicator);
            indicator.transform.SetParent(gameObject.transform);
            indicatorsRT.Add(indicator.GetComponent<RectTransform>());
            indicatorsRT[i].anchorMin = new Vector2(0, 0);
            indicatorsRT[i].anchorMax = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < unitsCores.Count; i++)
        {
            Transform core = unitsCores[i];
            // remove from List<> if unit is destroyed
            if (core == null)
            {
                unitsCores.RemoveAt(i);
                Destroy(indicatorsRT[i].gameObject);
                indicatorsRT.RemoveAt(i);
                continue;
            }

            indicatorsRT[i].anchoredPosition = new Vector2(core.position.x, core.position.y) * scale;
        }

        cameraIndicator.anchoredPosition = mainCamera.transform.position * scale;
        float cameraSizeY = mainCamera.GetComponent<Camera>().orthographicSize * 2;
        float cameraSizeX = cameraSizeY * mainCamera.GetComponent<Camera>().aspect;
        cameraIndicator.sizeDelta = new Vector2(cameraSizeX, cameraSizeY) * scale;
    }
}
