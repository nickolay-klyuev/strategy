using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed = 1f;

    private bool isSelected = false;
    private bool isMoving = false;
    private bool isAttacking = false;
    private Vector3 pointToMove;
    private AttackController attackController;

    // Start is called before the first frame update
    void Start()
    {
        attackController = GetComponent<AttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        // to change later and remove from this controller
        if (isSelected)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 1);
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }

        //move
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointToMove, speed * Time.deltaTime);
        }
        //rotate
        if (isMoving && !isAttacking)
        {
            RotateToPoint(pointToMove);
        }
        else
        {
            RotateToPoint(attackController.GetTargetGameobject().transform.position);
        }
    }

    void OnMouseDown()
    {
        isSelected = true;
    }

    private void RotateToPoint(Vector3 point)
    {
        //rotate to point direction
        Vector3 vectorToTarget = point - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void SetIsSelected(bool selected)
    {
        isSelected = selected;
    }

    public bool GetIsSelected()
    {
        return isSelected;
    }

    public void SetIsAttacking(bool attacking)
    {
        isAttacking = attacking;
    }

    public void MoveToPoint(Vector3 point)
    {
        pointToMove = new Vector3(point.x, point.y, transform.position.z);
        isMoving = true;
    }
}
