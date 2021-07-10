using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawningScript : MonoBehaviour
{
    public GameObject unitToSpawn;
    private int cost;
    private List<GameObject> unitsOnStage = new List<GameObject>();
    private int unitsLimit;
    private string buttonText;

    private PanelMetaData metaData;

    // Start is called before the first frame update
    void Start()
    {
        cost = unitToSpawn.GetComponentInChildren<UnitProperties>().cost;
        GetComponentInChildren<Text>().text += $"({cost})";
        buttonText = GetComponentInChildren<Text>().text;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < unitsOnStage.Count; i++)
        {
            if (unitsOnStage[i] == null)
            {
                unitsOnStage.RemoveAt(i);
            }
        }

        // write limits near spawn button
        if (unitsLimit > 0)
        {
            GetComponentInChildren<Text>().text = buttonText + $"({unitsOnStage.Count}/{unitsLimit})";
        }
    }

    public void SpawnUnit()
    {
        metaData = GetComponentInParent<PanelMetaData>();

        // TODO: maybe improve and change this one
        unitsLimit = metaData.GetCallObject().GetComponent<SpawnLimits>().unitsLimit;

        if (unitsOnStage.Count < unitsLimit)
        {
            if (GameObject.FindGameObjectWithTag("ResourceSystem").GetComponent<ResourceSystem>().SpendResource(cost))
            {
                GameObject currentUnit = Instantiate(unitToSpawn, metaData.GetCallObject().transform.position, Quaternion.identity);
                unitsOnStage.Add(currentUnit);
            }
            else
            {
                Debug.Log("Not enough resources");
            }
        }
        else
        {
            Debug.Log("Units limit for this building");
        }
    }
}
