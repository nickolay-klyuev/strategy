using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniDescInfoShowScript : MonoBehaviour
{
    public void SetDescriptionText(string desc)
    {
        GetComponentInChildren<Text>().text = desc;
    }

    public void AddDescriptionText(string desc)
    {
        GetComponentInChildren<Text>().text += desc;
    }
}
