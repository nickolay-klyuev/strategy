using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    private UnitProperties unitProperties;
    private MoveController moveController;
    private AttackController attackController;

    public void DestroyThisGameObject()
    {
        Destroy(gameObject);
        UnitsOnScene.RemoveUnit(gameObject);
    }

    public void LeaveOnlySprite()
    {
        foreach (Component comp in GetComponents<Component>())
        {
            if (!(comp is Transform || comp is SpriteRenderer))
            {
                Destroy(comp);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        moveController = GetComponent<MoveController>();
        attackController = GetComponent<AttackController>();
        if (moveController == null || attackController == null)
        {
            moveController = GetComponentInChildren<MoveController>();
            attackController = GetComponentInChildren<AttackController>();
        }

        if (GetComponentInChildren<UnitProperties>() != null)
        {
            unitProperties = GetComponentInChildren<UnitProperties>();
        }
        else
        {
            unitProperties = GetComponent<UnitProperties>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (unitProperties.health <= 0 && unitProperties.hasDeathAnimation) // trigger death animation and stop moving during it
        {
            moveController.StopChasing();
            moveController.StopMoving();
            attackController.StopAttack();

            GetComponent<Animator>().SetTrigger("DeathTrigger");
        }
        else if (unitProperties.health <= 0 && GetComponent<BuildingsSelectTools>() != null)
        {
            Destroy(GetComponent<BuildingsSelectTools>().GetMenuUI());
            DestroyThisGameObject();
        }
        else if (unitProperties.health <= 0)
        {
            DestroyThisGameObject();
        }
    }
}
