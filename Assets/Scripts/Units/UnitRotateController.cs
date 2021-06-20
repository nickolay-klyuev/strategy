using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRotateController : MonoBehaviour
{
    private AttackController attackController;
    private MoveController moveController;

    // Start is called before the first frame update
    void Start()
    {
        attackController = GetComponent<AttackController>();
        moveController = GetComponent<MoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        //rotate
        if (attackController.GetTargetGameobject() != null && attackController.GetIsAttacking())
        {
            RotateToPoint(attackController.GetTargetGameobject().transform.position);
        }

        if (moveController.GetIsMoving() && !attackController.GetIsAttacking())
        {
            RotateToPoint(moveController.GetPointToMove());
        }
    }

    private void RotateToPoint(Vector3 point)
    {
        //rotate to point direction
        Vector3 vectorToTarget = point - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10f);
    }
}
