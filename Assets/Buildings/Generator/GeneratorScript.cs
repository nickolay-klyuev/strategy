using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneratorScript : MonoBehaviour
{
    public int resourceAmount = 10;
    public float gatherTime = 10f;

    private UnitProperties properties;
    private TextMeshPro gatherAmountText;
    private Vector3 initTextPos;
    private bool textAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        properties = GetComponent<UnitProperties>();

        gatherAmountText = GetComponentInChildren<TextMeshPro>();
        if (gatherAmountText != null) // enemies generators don't have textMshPro TODO: maybe I will remove resources system for enemies
        {
            initTextPos = gatherAmountText.transform.position;
            gatherAmountText.gameObject.SetActive(false);
        }

        InvokeRepeating("GatherResource", gatherTime, gatherTime);
    }

    void FixedUpdate()
    {
        if (textAnimation)
        {
            gatherAmountText.transform.Translate(Vector3.up * Time.deltaTime);

            if (gatherAmountText.transform.position.y >= initTextPos.y + 1f)
            {
                textAnimation = false;
                gatherAmountText.gameObject.SetActive(false);
                gatherAmountText.transform.position = initTextPos;
            }
        }
    }

    private void GatherResource()
    {
        if (properties.unitType == "enemy")
        {
            EnemyResourceScript.GatherResource(resourceAmount);
        }
        else
        {
            ResourceSystem.GatherResource(resourceAmount);

            gatherAmountText.text = $"+{resourceAmount}";
            gatherAmountText.gameObject.SetActive(true);
            textAnimation = true;
        }
    }
}
