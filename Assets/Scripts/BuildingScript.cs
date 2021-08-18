using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingScript : MonoBehaviour
{
    public GameObject buildPlace;
    public GameObject objectToBuild;
    public AudioClip buildSound;

    private GameObject activeBuildPlace;
    private MiniMapController miniMapController;
    private bool isBuilding = false;
    public bool GetIsBuilding()
    {
        return isBuilding;
    }

    // Start is called before the first frame update
    void Start()
    {
        miniMapController = GameObject.Find("Mini Map").GetComponent<MiniMapController>();
        GetComponentInChildren<Text>().text += "(" + objectToBuild.GetComponent<UnitProperties>().cost + ")";
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuilding)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeBuildPlace.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);

            if (Input.GetMouseButtonDown(0) && activeBuildPlace.GetComponent<BuildPlaceScript>().GetCanBeBuild())
            {
                if (GameObject.FindGameObjectWithTag("ResourceSystem").GetComponent<ResourceSystem>().SpendResource(objectToBuild.GetComponent<UnitProperties>().cost))
                {
                    // Build
                    isBuilding = false;
                    Vector3 buildingPosition = activeBuildPlace.transform.position;
                    
                    Destroy(activeBuildPlace);

                    GameObject building = Instantiate(objectToBuild, buildingPosition, Quaternion.identity);
                    miniMapController.AddIndicator(building);
                    UnitsOnScene.AddUnit(building);
                }

            }
            else if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Can't be build here");
            }
            else if (Input.GetMouseButtonDown(1))
            {
                // Cancel
                isBuilding = false;
                Destroy(activeBuildPlace);
            }
        }
    }

    public void StartBuild()
    {
        if (!isBuilding)
        {
            isBuilding = true;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            activeBuildPlace = Instantiate(buildPlace, new Vector3(mousePosition.x, mousePosition.y, 0), Quaternion.identity);
        }
    }
}
