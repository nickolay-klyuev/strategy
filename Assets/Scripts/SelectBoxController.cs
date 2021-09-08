using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBoxController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private PolygonCollider2D polygonCollider;
    private bool isSelecting = false;
    private Vector3 initialMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();
        lineRenderer.positionCount = 4;
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectedUnits.UnselectAll();
            StartSelecting();
        }

        if (isSelecting)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 currentMousePosition = new Vector3(mousePosition.x, mousePosition.y, -1);
            lineRenderer.SetPosition(1, new Vector3(initialMousePosition.x, currentMousePosition.y, currentMousePosition.z));
            lineRenderer.SetPosition(2, currentMousePosition);
            lineRenderer.SetPosition(3, new Vector3(currentMousePosition.x, initialMousePosition.y, currentMousePosition.z));

            Vector2[] colliderPath = new Vector2[4]{
                new Vector2(initialMousePosition.x, initialMousePosition.y),
                new Vector2(initialMousePosition.x, currentMousePosition.y),
                new Vector2(currentMousePosition.x, currentMousePosition.y),
                new Vector2(currentMousePosition.x, initialMousePosition.y)
            };
            polygonCollider.SetPath(0, colliderPath);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        FriendlyUnitsSelectionController friendlyMoveController = collider.transform.GetComponent<FriendlyUnitsSelectionController>();
        if (friendlyMoveController != null)
        {
            // check if already contains to avoid a bug when you can move unselected units
            if (!SelectedUnits.selectedUnits.Contains(collider.gameObject))
            {
                SelectedUnits.selectedUnits.Add(collider.gameObject);
                friendlyMoveController.SetIsSelected(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        FriendlyUnitsSelectionController friendlyMoveController = collider.transform.GetComponent<FriendlyUnitsSelectionController>();
        if (friendlyMoveController != null && friendlyMoveController.GetIsSelected())
        {
            SelectedUnits.selectedUnits.Remove(collider.gameObject);
            friendlyMoveController.SetIsSelected(false);
        }
    }

    public void StartSelecting()
    {
        isSelecting = true;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        initialMousePosition = new Vector3(mousePosition.x, mousePosition.y, -1);

        lineRenderer.SetPosition(0, initialMousePosition);
    }

    public void StopSelecting()
    {
        isSelecting = false;
        Vector3 iPos3 = new Vector3(0, 0, -1);
        Vector2 iPos2 = new Vector2(0, 0);
        lineRenderer.SetPositions(new Vector3[4]{iPos3, iPos3, iPos3, iPos3});
        polygonCollider.SetPath(0, new Vector2[4]{iPos2, iPos2, iPos2, iPos2});
    }
}
