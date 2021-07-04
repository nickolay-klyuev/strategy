using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private FriendlyMoveController friendlyMoveController;
    private MoveController moveController;
    private AttackRangeRadiusController attackRangeRadiusController;
    private UnitProperties currentUnitProperties;
    private MissileSpawnerController[] missileSpawnerControllers;
    private GameObject targetGameobject;
    private LineRenderer attackLine;

    private float attackSpeed;
    private bool isAttacking = false;

    // Start is called before the first frame update
    private void Start()
    {
        friendlyMoveController = GetComponent<FriendlyMoveController>();
        moveController = GetComponent<MoveController>();
        attackRangeRadiusController = GetComponentInChildren<AttackRangeRadiusController>();
        currentUnitProperties = GetComponent<UnitProperties>();
        missileSpawnerControllers = GetComponentsInChildren<MissileSpawnerController>();
        
        attackLine = GetComponent<LineRenderer>();
        attackLine.positionCount = 2;
        attackLine.useWorldSpace = true;
        attackLine.loop = false;
        attackLine.enabled = false;

        attackSpeed = currentUnitProperties.attackSpeed;
    }

    private void Update()
    {
        // raycast click 
        // mark target to attack
        if (friendlyMoveController != null && friendlyMoveController.GetIsSelected())
        {
            if (Input.GetMouseButtonDown(1)) {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                UnitProperties raycastHitunitProperties = hit.collider.gameObject.GetComponent<UnitProperties>();
                if (raycastHitunitProperties != null) {
                    if (raycastHitunitProperties.unitType == "enemy" && friendlyMoveController != null)
                    {
                        StopAttack();
                        moveController.StartChasing(hit.collider.gameObject);
                    }
                }
            }
        }

        // update line to see which enemy is under attack
        if (isAttacking && friendlyMoveController.GetIsSelected())
        {
            attackLine.enabled = true;
            attackLine.SetPosition(0, transform.position);
            attackLine.SetPosition(1, targetGameobject.transform.position);
        }
        else
        {
            attackLine.enabled = false;
        }
    }

    public void StartAttack(GameObject target)
    {
        StopAttack();
        isAttacking = true;
        targetGameobject = target;

        // draw line to see which enemy under attack
        attackLine.enabled = true;

        InvokeRepeating("DoAttack", attackSpeed, attackSpeed);
    }

    private void DoAttack()
    {
        foreach(MissileSpawnerController missileSpawnerController in missileSpawnerControllers)
        {
            missileSpawnerController.SpawnMissile(targetGameobject);
        }
    }

    public void StopAttack()
    {
        isAttacking = false;
        attackLine.enabled = false;
        CancelInvoke("DoAttack");
    }

    public GameObject GetTargetGameobject()
    {
        return targetGameobject;
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }
}
