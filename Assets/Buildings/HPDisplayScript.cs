using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDisplayScript : MonoBehaviour
{
    Text textDisplay;
    UnitProperties unitProperties;

    // Start is called before the first frame update
    void Start()
    {
        textDisplay = GetComponent<Text>();
        unitProperties = GetComponentInParent<PanelMetaData>().GetCallObject().GetComponent<UnitProperties>();

        DisplayHP();
    }

    private void FixedUpdate()
    {
        DisplayHP();
    }

    private void DisplayHP()
    {
        textDisplay.text = $"HP: {unitProperties.health};";
    }
}
