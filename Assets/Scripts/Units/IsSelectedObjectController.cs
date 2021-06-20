using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsSelectedObjectController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
                spriteRenderer.enabled = true;
            }
            else
            {
                spriteRenderer.enabled = false;
            }
        }
    }

    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
