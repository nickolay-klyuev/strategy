using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverloadScript : MonoBehaviour
{
    public void DoOverload()
    {
        GameObject generatorStageObject = GetComponentInParent<PanelMetaData>().GetCallObject();

        generatorStageObject.GetComponent<UnitProperties>().health -= 100;

        int resourceAmount = generatorStageObject.GetComponent<GeneratorScript>().resourceAmount;
        generatorStageObject.GetComponent<GeneratorScript>().resourceAmount += (int)((float)resourceAmount * 0.1f);
    }
}
