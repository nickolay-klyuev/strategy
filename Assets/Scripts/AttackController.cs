using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private UnitProperties currentUnitProperties;
    private GameObject targetGameobject;
    private float attackSpeed;
    //private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        currentUnitProperties = GetComponent<UnitProperties>();
        attackSpeed = currentUnitProperties.attackSpeed;
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
        Debug.Log("Fire");
    }

    public void StartAttack(GameObject target)
    {
        targetGameobject = target;
        InvokeRepeating("AttackingTarget", attackSpeed, attackSpeed);
        //isAttacking = true;
    }

    public void StopAttack()
    {
        CancelInvoke("AttackingTarget");
    }
}
