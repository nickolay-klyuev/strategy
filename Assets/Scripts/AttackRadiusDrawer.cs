using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadiusDrawer : MonoBehaviour
{
    [SerializeField] private bool isBuilding = false;
    [SerializeField] private bool isRotating = true;

    private float actualRadius;

    private LineRenderer lineRenderer;
    private UnitProperties unitProperties;
    private FriendlyUnitsSelectionController fMoveController;
    private BuildingsSelectTools bSelectTools;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (isBuilding)
        {
            unitProperties = transform.parent.GetComponent<UnitProperties>();
            bSelectTools = transform.parent.GetComponent<BuildingsSelectTools>();
        }
        else
        {
            unitProperties = transform.parent.GetComponentInChildren<UnitProperties>();
            fMoveController = transform.parent.GetComponentInChildren<FriendlyUnitsSelectionController>();
        }

        if (isBuilding)
        {
            actualRadius = transform.parent.GetComponentInChildren<CircleCollider2D>().radius;
        }
        else
        {
            actualRadius = unitProperties.attackRange;
        }

        //draw attack range radius
        lineRenderer.positionCount = 51;
        lineRenderer.useWorldSpace = false;
        CreatePoints();

        lineRenderer.enabled = false;
    }

    private void FixedUpdate()
    {
        transform.position = unitProperties.transform.position;

        // show radius if selected
        if ((fMoveController != null && fMoveController.GetIsSelected()) ||
            (bSelectTools != null && bSelectTools.GetIsSelected()))
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }

        // little rotate attack range drawed radius
        if (isRotating)
        {
            transform.Rotate(new Vector3(0,0,1), Time.deltaTime * 10);
        }
    }

    private void CreatePoints ()
    {
        float x;
        float y;
        //float z;

        float angle = 20f;

        for (int i = 0; i < (50 + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * actualRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * actualRadius;

            lineRenderer.SetPosition(i, new Vector3(x,y,0) );

            angle += (360f / 50);
        }
    }
}
