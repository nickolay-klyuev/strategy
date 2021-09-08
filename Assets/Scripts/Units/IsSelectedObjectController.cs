using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSelectedObjectController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 initialScale;
    private bool isExternal = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale;

        if (GetComponentInParent<UnitProperties>().unitType == "enemy")
        {
            spriteRenderer.color = Color.red;
        }

        DisableSelectBox();
    }

    // Update is called once per frame
    void Update()
    {
        FriendlyMoveController friendlyMoveController = GetComponentInParent<FriendlyMoveController>();
        if (friendlyMoveController != null)
        {
            // maybe remove from this controller later
            if (friendlyMoveController.GetIsSelected())
            {
                EnableSelectBox();
            }
            else if (!isExternal)
            {
                DisableSelectBox();
            }
        }

        BuildingsSelectTools buildingsSelectTools = GetComponentInParent<BuildingsSelectTools>();
        if (buildingsSelectTools != null)
        {
            if (buildingsSelectTools.GetIsSelected())
            {
                EnableSelectBox();
            }
            else if (!isExternal)
            {
                DisableSelectBox();
            }
        }
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }

    public void EnableSelectBox(Vector3 extraScale = new Vector3(), bool external = false)
    {
        isExternal = external;
        spriteRenderer.transform.localScale = initialScale + extraScale;
        spriteRenderer.enabled = true;
    }

    public void DisableSelectBox(Vector3 extraScaleRemove = new Vector3(), bool external = false)
    {
        if (external)
        {
            isExternal = false;
        }
        spriteRenderer.transform.localScale = initialScale - extraScaleRemove;
        spriteRenderer.enabled = false;
    }
}
