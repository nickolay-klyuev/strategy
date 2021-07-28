using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverScript : MonoBehaviour
{
    private IsSelectedObjectController isSelectedScript;
    private bool isOnHover = false;
    public bool GetIsOnHover()
    {
        return isOnHover;
    }

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
        isOnHover = true;
        isSelectedScript.EnableSelectBox(extraScale, true);
    }

    private void OnMouseExit()
    {
        isOnHover = false;
        isSelectedScript.DisableSelectBox(extraScale, true);
    }
}
