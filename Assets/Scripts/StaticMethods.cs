using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticMethods : MonoBehaviour
{
    static public GameObject GetGameObjectByRaycast()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        return hit.collider.gameObject;
    }

    // missiles are flying always to attack radius
    static public Vector2 GetMaxAttackRangePosition(Vector2 initialPos, Vector2 targetPos, float attackRange)
    {
        Vector2 targetDistanceVector = (initialPos - targetPos);
        float targetDistance = Mathf.Sqrt(Mathf.Pow(targetDistanceVector.x, 2) + Mathf.Pow(targetDistanceVector.y, 2));
        float d = attackRange - targetDistance;
        float x1 = initialPos.x;
        float y1 = initialPos.y;
        float x2 = targetPos.x;
        float y2 = targetPos.y;
        float x3 = x2 + d/Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) + Mathf.Pow(y2 - y1, 2)) * (x2 - x1);
        float y3 = y2 + d/Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) + Mathf.Pow(y2 - y1, 2)) * (y2 - y1);

        return new Vector2(x3, y3);
    }
}
