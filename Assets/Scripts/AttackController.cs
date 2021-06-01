using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private UnitProperties currentUnitProperties;
    private MoveController moveController;
    private GameObject targetGameobject;
    private float attackSpeed;
    private float attackPower;
    //private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        currentUnitProperties = GetComponent<UnitProperties>();
        moveController = GetComponent<MoveController>();
        attackSpeed = currentUnitProperties.attackSpeed;
        attackPower = currentUnitProperties.attackPower;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (isAttacking)
        {

        }*/
    }

    private void AttackingTarget()
    {
        UnitProperties targetProperties = targetGameobject.GetComponent<UnitProperties>();
        targetProperties.health -= attackPower;
    }

    public void StartAttack(GameObject target)
    {
        targetGameobject = target;
        moveController.SetIsAttacking(true);
        
        InvokeRepeating("AttackingTarget", attackSpeed, attackSpeed);
        //isAttacking = true;
    }

    public void StopAttack()
    {
        CancelInvoke("AttackingTarget");
        moveController.SetIsAttacking(false);
    }

    public GameObject GetTargetGameobject()
    {
        return targetGameobject;
    }
}
