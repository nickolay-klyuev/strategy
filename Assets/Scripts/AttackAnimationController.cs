using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimationController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        lineRenderer.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Animate(GameObject target)
    {
        lineRenderer.SetPosition(0, transform.position + new Vector3(0, 0, -1));
        lineRenderer.SetPosition(1, target.transform.position + new Vector3(0, 0, -1));
    }
}
