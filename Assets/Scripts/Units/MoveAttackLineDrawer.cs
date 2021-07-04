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
                attackMoveLine.SetPosition(1, drawObject.transform.position);
            }
            else
            {
                attackMoveLine.SetPosition(1, drawPosition);
            }
        }
        else
        {
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
        drawObject = null;
        letDraw = false;
    }
}
