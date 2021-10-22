using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRotateController : MonoBehaviour
{
    static public void RotateToPoint(Vector3 point, Transform transform)
    {
        //rotate to point direction
        Vector3 vectorToTarget = point - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10f);
    }
}
