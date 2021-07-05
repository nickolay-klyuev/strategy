using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public GameObject buildPlace;
    private GameObject activeBuildPlace;
    private bool isBuilding = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
                // Build
                isBuilding = false;
                Vector3 buildingPosition = activeBuildPlace.transform.position;
                
                Destroy(activeBuildPlace);
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
