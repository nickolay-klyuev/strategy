using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject mainCamera;
    public GameObject cameraIndicator;

    public static float miniMapHeight;
    public static float miniMapWidth;

    private List<GameObject> unitsWithIndicator = new List<GameObject>();

    private GameObject background;
    private RectTransform rectTransform;
    private List<RectTransform> indicatorsRT = new List<RectTransform>();
    private RectTransform cameraIndicatorRT;
    static private bool isMiniMapPointerDown = false;
    static public bool GetIsMiniMapPointerDown()
    {
        return isMiniMapPointerDown;
    }

    private PointerEventData onpointerDownEventDataGlobal;

    private float scale = 5;
    private float mapMargin = 10f;

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

        rectTransform.anchoredPosition = new Vector2(rectTransform.sizeDelta.x / 2 + mapMargin, - Screen.height + mapMargin + rectTransform.sizeDelta.y / 2);

        cameraIndicatorRT.anchorMin = new Vector2(0, 0);
        cameraIndicatorRT.anchorMax = new Vector2(0, 0);

        for (int i = 0; i < UnitsOnScene.AllCount(); i++)
        {
            GameObject currentUnit = UnitsOnScene.GetAllUnits()[i];
            AddIndicator(currentUnit);
        }
    }

    public void AddIndicator(GameObject unit)
    {
        UnitProperties unitProperties = unit.GetComponent<UnitProperties>();
        if (unitProperties == null)
        {
            unitProperties = unit.GetComponentInChildren<UnitProperties>();
        }

        unitsWithIndicator.Add(unit);
        GameObject createdIndicator = Instantiate(unitProperties.miniMapIndicator);
        if (unitProperties.unitType == "enemy")
        {
            createdIndicator.GetComponent<Image>().color = Color.red;
        }
        createdIndicator.transform.SetParent(gameObject.transform);
        indicatorsRT.Add(createdIndicator.GetComponent<RectTransform>());
        indicatorsRT[indicatorsRT.Count - 1].anchorMin = new Vector2(0, 0);
        indicatorsRT[indicatorsRT.Count - 1].anchorMax = new Vector2(0, 0);
    }

    private void Update()
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

        for (int i = 0; i < unitsWithIndicator.Count; i++)
        {
            // remove from List<> if unit is destroyed
            if (unitsWithIndicator[i] == null)
            {
                unitsWithIndicator.RemoveAt(i);
                Destroy(indicatorsRT[i].gameObject);
                indicatorsRT.RemoveAt(i);
                continue;
            }

            Transform core = unitsWithIndicator[i].transform.Find("Core");
            if (core != null)
            {
                indicatorsRT[i].anchoredPosition = new Vector2(core.position.x, core.position.y) * scale;
            }
            else
            {
                indicatorsRT[i].anchoredPosition = new Vector2(unitsWithIndicator[i].transform.position.x, unitsWithIndicator[i].transform.position.y) * scale;
            }
        }

        cameraIndicatorRT.anchoredPosition = mainCamera.transform.position * scale;
        float cameraSizeY = mainCamera.GetComponent<Camera>().orthographicSize * 2;
        float cameraSizeX = cameraSizeY * mainCamera.GetComponent<Camera>().aspect;
        cameraIndicatorRT.sizeDelta = new Vector2(cameraSizeX, cameraSizeY) * scale;
    }
}
