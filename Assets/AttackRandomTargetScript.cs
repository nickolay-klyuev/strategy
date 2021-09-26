using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRandomTargetScript : MonoBehaviour
{
    private Transform target;
    private MoveController moveController;

    // Start is called before the first frame update
    void Start()
    {
        moveController = GetComponent<MoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveController.GetIsMoving() && !moveController.GetIsChasing())
        {
            if (target != null)
            {
                moveController.MoveToPoint(target.position);
            }
            else
            {
                List<GameObject> friendlyUnits = UnitsOnScene.GetUnits("friendly;unit");
                friendlyUnits.AddRange(UnitsOnScene.GetUnits("friendly;building"));
                target = friendlyUnits[Random.Range(0, friendlyUnits.Count)].GetComponentInChildren<UnitProperties>().transform;
            }
        }
    }
}
