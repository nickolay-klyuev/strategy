using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttackLineDrawer : MonoBehaviour
{
    private FriendlyMoveController friendlyMoveController;
    private LineRenderer attackMoveLine;
    private bool letDraw = false;
    private GameObject drawObject;
    private Vector3 drawPosition;

    // Start is called before the first frame update
    void Start()
    {
        friendlyMoveController = GetComponent<FriendlyMoveController>();

        attackMoveLine = GetComponent<LineRenderer>();
        attackMoveLine.positionCount = 2;
        attackMoveLine.useWorldSpace = true;
        attackMoveLine.loop = false;
        attackMoveLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (letDraw && friendlyMoveController.GetIsSelected())
        {
            attackMoveLine.enabled = true;
            attackMoveLine.SetPosition(0, transform.position);
            if (drawObject != null)
            {
                drawObject.transform.GetComponentInChildren<IsSelectedObjectController>().EnableSelectBox(); // select box on enemies on

                attackMoveLine.SetPosition(1, drawObject.transform.position);
            }
            else
            {
                attackMoveLine.SetPosition(1, drawPosition);
            }
        }

        if (!letDraw || !friendlyMoveController.GetIsSelected() || 
            (!GetComponent<MoveController>().GetIsMoving() && !GetComponent<MoveController>().GetIsChasing())) // TODO: fix this mess and maybe figure something better 
        {
            if (drawObject != null && !drawObject.GetComponent<OnHoverScript>().GetIsOnHover())
            {
                drawObject.transform.GetComponentInChildren<IsSelectedObjectController>().DisableSelectBox(); // select box on enemies off
            }
            attackMoveLine.enabled = false;
        }
    }

    public void StartDraw(GameObject targetObject)
    {
        StopDraw();

        drawObject = targetObject;
        letDraw = true;
    }

    public void StartDraw(Vector3 targetPosition)
    {
        StopDraw();

        drawPosition = targetPosition;
        letDraw = true;
    }

    public void StopDraw()
    {
        if (drawObject != null)
        {
            drawObject.transform.GetComponentInChildren<IsSelectedObjectController>().DisableSelectBox(); // select box on enemies off
            drawObject = null;
        }
        letDraw = false;
    }
}
