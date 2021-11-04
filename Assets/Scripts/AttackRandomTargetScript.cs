using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRandomTargetScript : MonoBehaviour
{
    private MoveController move;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<MoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!move.GetIsChasing() && !move.GetIsMoving())
        {
            //List<GameObject> friendlyUnits = UnitsOnScene.GetUnits("friendly;unit");
            List<GameObject> friendlyUnits = UnitsOnScene.GetUnits("friendly;building");
            move.MoveToPoint(friendlyUnits[Random.Range(0, friendlyUnits.Count)].GetComponentInChildren<UnitProperties>().transform.position);
        }
    }
}
