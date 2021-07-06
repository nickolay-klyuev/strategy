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
}
