using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    public GameObject mainTarget;

    private Vector3 mainTargetPosition;

    private MoveController moveController;
    private AttackController attackController;

    // Start is called before the first frame update
    void Start()
    {
        mainTargetPosition = mainTarget.transform.position;
        moveController = GetComponent<MoveController>();
        attackController = GetComponent<AttackController>();

        moveController.MoveToPoint(mainTargetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackController.GetIsAttacking())
        {
            moveController.StopMoving();
        }
        else
        {
            moveController.MoveToPoint(mainTargetPosition);
        }
    }
}
