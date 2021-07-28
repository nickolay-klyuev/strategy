using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private FriendlyMoveController friendlyMoveController;
    private MoveController moveController;
    private AttackRangeRadiusController attackRangeRadiusController;
    private UnitProperties currentUnitProperties;
    private MoveAttackLineDrawer attackLineDrawer;
    private MissileSpawnerController[] missileSpawnerControllers;
    private GameObject targetGameobject;

    private float attackSpeed;
    private bool isAttacking = false;
    private bool isDoAttackCoroutine = false;

    // Start is called before the first frame update
    private void Start()
    {
        friendlyMoveController = GetComponent<FriendlyMoveController>();
        moveController = GetComponent<MoveController>();
        attackRangeRadiusController = GetComponentInChildren<AttackRangeRadiusController>();
        currentUnitProperties = GetComponent<UnitProperties>();
        attackLineDrawer = GetComponent<MoveAttackLineDrawer>();
        missileSpawnerControllers = GetComponentsInChildren<MissileSpawnerController>();

        attackSpeed = currentUnitProperties.attackSpeed;
    }

    private void Update()
    {
        // raycast click 
        // mark target to attack
        if (friendlyMoveController != null && friendlyMoveController.GetIsSelected())
        {
            if (Input.GetMouseButtonDown(1)) {
                GameObject hitGameobject = StaticMethods.GetGameObjectByRaycast();
                UnitProperties raycastHitunitProperties = hitGameobject.GetComponent<UnitProperties>();
                if (raycastHitunitProperties != null) {
                    if (raycastHitunitProperties.unitType == "enemy" && friendlyMoveController != null)
                    {
                        StopAttack();
                        moveController.StartChasing(hitGameobject);
                    }
                }
            }
        }
    }

    public void StartAttack(GameObject target)
    {
        StopAttack();
        isAttacking = true;
        targetGameobject = target;

        if (!isDoAttackCoroutine)
        {
            StartCoroutine(DoAttack());
        }
    }

    IEnumerator DoAttack()
    {
        // all these need to avoid delay after click and attack start
        isDoAttackCoroutine = true;
        while (isAttacking)
        {
            yield return new WaitForSeconds(attackSpeed);
            if (isAttacking)
            {
                missileSpawnerControllers[Random.Range(0, missileSpawnerControllers.Length - 1)].SpawnMissile(targetGameobject);
            }
        }
        isDoAttackCoroutine = false;
        StopCoroutine(DoAttack());
    }

    public void StopAttack()
    {
        isAttacking = false;
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
