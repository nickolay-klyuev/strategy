using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private UnitProperties currentUnitProperties;
    private AttackAnimationController attackAnimationController;
    private GameObject targetGameobject;
    private float attackSpeed;
    private float attackPower;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        currentUnitProperties = GetComponent<UnitProperties>();
        attackAnimationController = GetComponentInChildren<AttackAnimationController>();
        attackSpeed = currentUnitProperties.attackSpeed;
        attackPower = currentUnitProperties.attackPower;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AttackingTarget()
    {
        UnitProperties targetProperties = targetGameobject.GetComponent<UnitProperties>();
        targetProperties.health -= attackPower;
    }

    public void StartAttack(GameObject target)
    {
        isAttacking = true;
        targetGameobject = target;
        InvokeRepeating("AttackingTarget", attackSpeed, attackSpeed);
        InvokeRepeating("AttackAnimation", attackSpeed, attackSpeed);
    }

    public void StopAttack()
    {
        isAttacking = false;
        CancelInvoke("AttackingTarget");
        CancelInvoke("AttackAnimation");
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
