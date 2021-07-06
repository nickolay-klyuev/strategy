using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverScript : MonoBehaviour
{
    private IsSelectedObjectController isSelectedScript;

    // Start is called before the first frame update
    void Start()
    {
        isSelectedScript = GetComponentInChildren<IsSelectedObjectController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 extraScale = new Vector3(0.1f, 0.1f, 0);
    private void OnMouseOver()
    {
        isSelectedScript.EnableSelectBox(extraScale, true);
    }

    private void OnMouseExit()
    {
        isSelectedScript.DisableSelectBox(extraScale, true);
    }
}
