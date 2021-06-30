using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed = 1f;

    private bool isMoving = false;
    private Vector3 pointToMove;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // stop after reach point to move
        float stopRange = .2f;
        if (transform.position.x < pointToMove.x + stopRange && transform.position.x > pointToMove.x - stopRange &&
            transform.position.y < pointToMove.y + stopRange && transform.position.y > pointToMove.y - stopRange)
        {
            isMoving = false;
        }

        // move
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointToMove, speed * Time.deltaTime);
        }
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

    public void StopMoving()
    {
        isMoving = false;
    }
}
