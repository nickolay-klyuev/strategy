using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorPowerDisplayScript : MonoBehaviour
{
    private Text displayText;
    private GeneratorScript generatorScript;

    // Start is called before the first frame update
    void Start()
    {
        displayText = GetComponent<Text>();
        generatorScript = GetComponentInParent<PanelMetaData>().GetCallObject().GetComponent<GeneratorScript>();

        DisplayPower();
    }

    private void FixedUpdate()
    {
        DisplayPower();
    }

    private void DisplayPower()
    {
        displayText.text = $"Power: {generatorScript.resourceAmount};";
    }
}
