using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadiusDrawer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private UnitProperties unitProperties;
    private FriendlyUnitsSelectionController fMoveController;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        unitProperties = transform.parent.GetComponentInChildren<UnitProperties>();
        fMoveController = transform.parent.GetComponentInChildren<FriendlyUnitsSelectionController>();

        //draw attack range radius
        lineRenderer.positionCount = 51;
        lineRenderer.useWorldSpace = false;
        CreatePoints();

        lineRenderer.enabled = false;
    }

    private void Update()
    {
        transform.position = unitProperties.transform.position;
    }

    private void FixedUpdate()
    {
        // show radius if selected
        if (fMoveController != null && fMoveController.GetIsSelected())
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }

        // little rotate attack range drawed radius
        transform.Rotate(new Vector3(0,0,1), Time.deltaTime * 10);
    }

    private void CreatePoints ()
    {
        float x;
        float y;
        //float z;

        float angle = 20f;

        for (int i = 0; i < (50 + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * unitProperties.attackRange;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * unitProperties.attackRange;

            lineRenderer.SetPosition(i, new Vector3(x,y,0) );

            angle += (360f / 50);
        }
    }
}
