using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverScript : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Color defaultColor;
    private IsSelectedObjectController isSelectedScript;
    private bool isOnHover = false;
    public bool GetIsOnHover()
    {
        return isOnHover;
    }

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        defaultColor = sprite.color;
        isSelectedScript = GetComponentInChildren<IsSelectedObjectController>();
        //isSelectedScript.DisableSelectBox(extraScale, true);
    }

    private Vector3 extraScale = new Vector3(0.1f, 0.1f, 0);
    private void OnMouseOver()
    {
        if (SettingsScript.IsTouch())
        {
            return;
        }

        isOnHover = true;
        sprite.color = Color.white;
        isSelectedScript.EnableSelectBox(extraScale, true);
    }

    private void OnMouseExit()
    {
        if (SettingsScript.IsTouch())
        {
            return;
        }

        isOnHover = false;
        sprite.color = defaultColor;
        isSelectedScript.DisableSelectBox(extraScale, true);
    }
}
