using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawningScript : MonoBehaviour
{
    public GameObject unitToSpawn;
    private int cost;
    private float buildTime;
    /*private List<GameObject> unitsOnStage = new List<GameObject>();
    private int unitsLimit;*/
    //private string buttonText;

    private PanelMetaData metaData;
    private Text queueLengthText;
    //private MiniMapController miniMapController;

    // Start is called before the first frame update
    void Start()
    {
        //miniMapController = GameObject.Find("Mini Map").GetComponent<MiniMapController>();
        cost = unitToSpawn.GetComponentInChildren<UnitProperties>().cost;
        buildTime = unitToSpawn.GetComponentInChildren<UnitProperties>().buildTime;
        //GetComponentInChildren<Text>().text += $"({cost})";
        //buttonText = GetComponentInChildren<Text>().text;
        queueLengthText = GetComponentInChildren<Text>();
        queueLengthText.text = "";
    }

    private void FixedUpdate()
    {
        int queueLength = metaData.GetCallObject().GetComponent<ProductionScript>().GetQueueLength();
        if (queueLength == 0)
        {
            queueLengthText.text = "";
        }
        else
        {
            queueLengthText.text = queueLength.ToString();
        }

        /*for (int i = 0; i < unitsOnStage.Count; i++)
        {
            if (unitsOnStage[i] == null)
            {
                unitsOnStage.RemoveAt(i);
            }
        }*/

        // write limits near spawn button
        /*if (unitsLimit > 0)
        {
            GetComponentInChildren<Text>().text = buttonText + $"({unitsOnStage.Count}/{unitsLimit})";
        }*/
    }

    public void SpawnUnit()
    {
        metaData = GetComponentInParent<PanelMetaData>();

        // TODO: maybe improve and change this one
        //unitsLimit = metaData.GetCallObject().GetComponent<SpawnLimits>().unitsLimit;

        //if (unitsOnStage.Count < unitsLimit)
        //{
            if (GameObject.FindGameObjectWithTag("ResourceSystem").GetComponent<ResourceSystem>().SpendResource(cost))
            {
                //StartCoroutine(BuildUnit());
                metaData.GetCallObject().GetComponent<ProductionScript>().AddObjectInProdQueue(unitToSpawn);
            }
            else
            {
                Debug.Log("Not enough resources");
            }
        //}
        //else
        //{
        //    Debug.Log("Units limit for this building");
        //}
    }

    /*IEnumerator BuildUnit()
    {   
        Vector2 position = metaData.GetCallObject().transform.position;
        Vector2 colliderSize = metaData.GetCallObject().GetComponent<CircleCollider2D>().bounds.size / 2;
        
        Vector2 placeToBuild;
        if (Random.Range(0f, 1f) > 0.5f)
        {
            float[] xColliderSize = {-colliderSize.x, colliderSize.x};
            placeToBuild = new Vector2(position.x + xColliderSize[Random.Range(0, xColliderSize.Length)], position.y + Random.Range(-colliderSize.y, colliderSize.y));
        }
        else
        {
            float[] yColliderSize = {-colliderSize.y, colliderSize.y};
            placeToBuild = new Vector2(position.x + Random.Range(-colliderSize.x, colliderSize.x), position.y + yColliderSize[Random.Range(0, yColliderSize.Length)]);
        }

        yield return new WaitForSeconds(buildTime);

        GameObject currentUnit = Instantiate(unitToSpawn, placeToBuild, Quaternion.identity);
        //unitsOnStage.Add(currentUnit);
        miniMapController.AddIndicator(currentUnit);
        UnitsOnScene.AddUnit(currentUnit);
    }*/
}
