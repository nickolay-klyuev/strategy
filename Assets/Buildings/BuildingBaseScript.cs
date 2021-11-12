using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingBaseScript : MonoBehaviour
{
    [SerializeField] private GameObject objectToBuild;

    private MiniMapController miniMapController;
    private UnitProperties buildingProperties;
    private float buildTime;
    private float startBuildTime;
    private float endBuildTime;
    private TextMeshPro buildProcessText;

    // Start is called before the first frame update
    void Start()
    {
        miniMapController = GameObject.Find("Mini Map").GetComponent<MiniMapController>();
        buildProcessText = GetComponentInChildren<TextMeshPro>();
        buildProcessText.text = "0%";

        buildingProperties = objectToBuild.GetComponent<UnitProperties>();
        buildTime = buildingProperties.buildTime;
        startBuildTime = Time.realtimeSinceStartup;
        endBuildTime = startBuildTime + buildTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        buildProcessText.text = $"{Mathf.Round((Time.realtimeSinceStartup - startBuildTime) * 100 / buildTime)}%";

        if (Time.realtimeSinceStartup >= endBuildTime)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        GameObject building = Instantiate(objectToBuild, transform.position, Quaternion.identity);
        miniMapController.AddIndicator(building);
        UnitsOnScene.AddUnit(building);
    }
}
