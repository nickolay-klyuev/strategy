using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBoxController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private bool isSelecting = false;
    private Vector3 initialMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 4;
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelecting)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 currentMousePosition = new Vector3(mousePosition.x, mousePosition.y, -1);
            lineRenderer.SetPosition(1, new Vector3(initialMousePosition.x, currentMousePosition.y, currentMousePosition.z));
            lineRenderer.SetPosition(2, currentMousePosition);
            lineRenderer.SetPosition(3, new Vector3(currentMousePosition.x, initialMousePosition.y, currentMousePosition.z));
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
        Vector3 iPos = new Vector3(0, 0, -1);
        lineRenderer.SetPositions(new Vector3[4]{iPos, iPos, iPos, iPos});
    }
}
