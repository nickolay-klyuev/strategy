using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed = 1f;

    private bool isSelected = false;
    private bool isMoving = false;
    private Vector3 pointToMove;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // stop moving after reach the point
        if (transform.position == pointToMove)
        {
            isMoving = false;
        }

        // move
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointToMove, speed * Time.deltaTime);
        }
    }

    void OnMouseDown()
    {
        isSelected = true;
    }

    public void SetIsSelected(bool selected)
    {
        isSelected = selected;
    }

    public bool GetIsSelected()
    {
        return isSelected;
    }

    public bool GetIsMoving()
    {
        return isMoving;
    }

    public Vector3 GetPointToMove()
    {
        return pointToMove;
    }

    public void MoveToPoint(Vector3 point)
    {
        pointToMove = new Vector3(point.x, point.y, transform.position.z);
        isMoving = true;
    }
}
