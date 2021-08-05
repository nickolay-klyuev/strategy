using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniMapController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject mainCamera;
    public GameObject friendlyUnitIndicator;
    public GameObject cameraIndicator;

    public static float miniMapHeight;
    public static float miniMapWidth;

    private List<Transform> unitsCores = new List<Transform>();
    private GameObject background;
    private RectTransform rectTransform;
    private List<RectTransform> indicatorsRT = new List<RectTransform>();
    private RectTransform cameraIndicatorRT;
    private bool isMiniMapPointerDown = false;
    private PointerEventData onpointerDownEventDataGlobal;

    private float scale = 10;

    public void OnPointerDown(PointerEventData pointerEventData) // move camera by clicking mini map
    {
        onpointerDownEventDataGlobal = pointerEventData;
        isMiniMapPointerDown = true;
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isMiniMapPointerDown = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        background = GameObject.Find("Background");
        rectTransform = transform.GetComponent<RectTransform>();
        cameraIndicatorRT = cameraIndicator.GetComponent<RectTransform>();

        Vector2 bgSize = background.GetComponent<SpriteRenderer>().size;
        rectTransform.sizeDelta = bgSize * scale;

        miniMapWidth = rectTransform.sizeDelta.x;
        miniMapHeight = rectTransform.sizeDelta.y;

        rectTransform.anchoredPosition = new Vector2(rectTransform.sizeDelta.x / 2, rectTransform.sizeDelta.y / -2);

        cameraIndicatorRT.anchorMin = new Vector2(0, 0);
        cameraIndicatorRT.anchorMax = new Vector2(0, 0);

        for (int i = 0; i < UnitsOnScene.GetAllUnits().Count; i++)
        {
            unitsCores.Add(UnitsOnScene.GetAllUnits()[i].transform.Find("Core"));
            
            GameObject indicator = Instantiate(friendlyUnitIndicator);
            indicator.transform.SetParent(gameObject.transform);
            indicatorsRT.Add(indicator.GetComponent<RectTransform>());
            indicatorsRT[i].anchorMin = new Vector2(0, 0);
            indicatorsRT[i].anchorMax = new Vector2(0, 0);
        }
    }

    private void FixedUpdate()
    {
        if (isMiniMapPointerDown)
        {
            float pointerX = onpointerDownEventDataGlobal.position.x;
            float pointerY = onpointerDownEventDataGlobal.position.y;
            if (pointerX > 0 && pointerX < rectTransform.sizeDelta.x &&
                pointerY > transform.position.y - rectTransform.sizeDelta.y / 2 && 
                pointerY < Screen.height) // move camera only if you click on mini map panel
            {
                mainCamera.transform.position = new Vector3(onpointerDownEventDataGlobal.position.x / scale, 
                                                    (onpointerDownEventDataGlobal.position.y - (transform.position.y - rectTransform.sizeDelta.y / 2)) / scale,
                                                    mainCamera.transform.position.z);
            }
        }

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

        cameraIndicatorRT.anchoredPosition = mainCamera.transform.position * scale;
        float cameraSizeY = mainCamera.GetComponent<Camera>().orthographicSize * 2;
        float cameraSizeX = cameraSizeY * mainCamera.GetComponent<Camera>().aspect;
        cameraIndicatorRT.sizeDelta = new Vector2(cameraSizeX, cameraSizeY) * scale;
    }
}
