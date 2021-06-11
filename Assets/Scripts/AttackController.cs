using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private UnitProperties currentUnitProperties;
    private MissileSpawnerController[] missileSpawnerControllers;
    private GameObject targetGameobject;

    private float attackSpeed;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        currentUnitProperties = GetComponent<UnitProperties>();
        missileSpawnerControllers = GetComponentsInChildren<MissileSpawnerController>();

        attackSpeed = currentUnitProperties.attackSpeed;
    }

    public void StartAttack(GameObject target)
    {
        isAttacking = true;
        targetGameobject = target;
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
