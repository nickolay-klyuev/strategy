using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private FriendlyMoveController friendlyMoveController;
    private UnitProperties currentUnitProperties;
    private MissileSpawnerController[] missileSpawnerControllers;
    private GameObject targetGameobject;

    private float attackSpeed;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        friendlyMoveController = GetComponent<FriendlyMoveController>();
        currentUnitProperties = GetComponent<UnitProperties>();
        missileSpawnerControllers = GetComponentsInChildren<MissileSpawnerController>();

        attackSpeed = currentUnitProperties.attackSpeed;
    }

    void Update()
    {
        // raycast click 
        if (Input.GetMouseButtonDown(1)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            UnitProperties raycastHitunitProperties = hit.collider.gameObject.GetComponent<UnitProperties>();
            if (raycastHitunitProperties != null) {
                if (raycastHitunitProperties.unitType == "enemy" && friendlyMoveController != null)
                {
                    StartAttack(hit.collider.gameObject);
                }
            }
        }
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
