using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float speed = 1f;

    private bool isSelected = false;
    private bool isMoving = false;
    private Vector3 pointToMove;
    private AttackController attackController;
    private UnitProperties unitProperties;
    private string unitType;

    // Start is called before the first frame update
    void Start()
    {
        unitProperties = GetComponent<UnitProperties>();
        attackController = GetComponent<AttackController>();
        unitType = unitProperties.unitType;
    }

    // Update is called once per frame
    void Update()
    {
        // stop moving after reach the point
        if (transform.position == pointToMove)
        {
            isMoving = false;
        }

        //move
        if (isMoving && unitType == "friendly")
        {
            transform.position = Vector3.MoveTowards(transform.position, pointToMove, speed * Time.deltaTime);
        }

        //rotate
        if (attackController.GetTargetGameobject() != null && attackController.GetIsAttacking())
        {
            RotateToPoint(attackController.GetTargetGameobject().transform.position);
        }
        if (isMoving && !attackController.GetIsAttacking())
        {
            RotateToPoint(pointToMove);
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
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10f);
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

    public void MoveToPoint(Vector3 point)
    {
        pointToMove = new Vector3(point.x, point.y, transform.position.z);
        isMoving = true;
    }
}
