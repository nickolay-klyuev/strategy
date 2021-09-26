using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private FriendlyUnitsSelectionController friendlyMoveController;
    private MoveController moveController;
    private AttackRangeRadiusController attackRangeRadiusController;
    private UnitProperties currentUnitProperties;
    private MoveAttackLineDrawer attackLineDrawer;
    private MissileSpawnerController[] missileSpawnerControllers;

    private GameObject targetGameobject;
    public GameObject GetTargetGameobject()
    {
        return targetGameobject;
    }

    private Animator animator;

    private float attackSpeed;
    private bool isAttacking = false;
    private bool isDoAttackCoroutine = false;

    // Start is called before the first frame update
    private void Start()
    {
        friendlyMoveController = GetComponent<FriendlyUnitsSelectionController>();
        if (friendlyMoveController == null)
        {
            friendlyMoveController = GetComponentInParent<FriendlyUnitsSelectionController>();
        }

        moveController = GetComponent<MoveController>();
        if (moveController == null)
        {
            moveController = GetComponentInParent<MoveController>();
        }

        attackRangeRadiusController = GetComponentInChildren<AttackRangeRadiusController>();
        currentUnitProperties = GetComponent<UnitProperties>();
        if (currentUnitProperties == null) // for units with separate cannon
        {
            currentUnitProperties = GetComponentInParent<UnitProperties>();
        }

        attackLineDrawer = GetComponent<MoveAttackLineDrawer>();
        missileSpawnerControllers = GetComponentsInChildren<MissileSpawnerController>();
        animator = GetComponent<Animator>();

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

        //rotate
        if (GetTargetGameobject() != null && GetIsAttacking())
        {
            UnitRotateController.RotateToPoint(GetTargetGameobject().transform.position, transform);
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
            float randomIndex = attackSpeed * 0.1f; // just 10% random for attack speed
            float attackSpeedWithRandom = attackSpeed + Random.Range(-randomIndex, randomIndex);

            yield return new WaitForSeconds(attackSpeedWithRandom);

            if (isAttacking)
            {
                // SpawnMissileInCoroutine() run this funtion in fire animation (TriggerFire)
                /*if (animator != null) // fire animation
                {
                    animator.SetTrigger("TriggerFire");
                }*/
                SpawnMissileInCoroutine();
            }
        }
        isDoAttackCoroutine = false;
        StopCoroutine(DoAttack());
    }

    public void SpawnMissileInCoroutine()
    {
        // spawn all missile at once
        /*foreach (MissileSpawnerController missileController in missileSpawnerControllers)
        {
            missileController.SpawnMissile(targetGameobject);
        }*/
        
        // spawn from random missile spawner (if a lot of missile spawners, but damage is the same as one missile spawner)
        missileSpawnerControllers[Random.Range(0, missileSpawnerControllers.Length - 1)].SpawnMissile(targetGameobject);
    }

    public void StopAttack()
    {
        isAttacking = false;
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }
}
