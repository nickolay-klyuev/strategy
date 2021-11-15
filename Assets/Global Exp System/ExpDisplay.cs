using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpDisplay : MonoBehaviour
{
    private Text expText;
    private RectTransform expBar;
    private float expBarMaxWidth;

    // Start is called before the first frame update
    void Start()
    {
        expText = GetComponentInChildren<Text>();
        expBar = transform.GetChild(1).transform.GetChild(0).GetComponent<RectTransform>();
        expBarMaxWidth = expBar.sizeDelta.x;
    }

    void FixedUpdate()
    {
        expText.text = $"Exp: {GlobalExpSystem.GetExp().ToString()}";
        expBar.sizeDelta = new Vector2(expBarMaxWidth * GlobalExpSystem.GetRateToNextLvl(), expBar.sizeDelta.y);
    }
}
