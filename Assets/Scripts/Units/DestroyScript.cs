using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    private UnitProperties unitProperties;
    private MoveController moveController;

    public void DestroyThisGameObject()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        moveController = GetComponent<MoveController>();

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
            if (moveController.GetIsChasing())
            {
                moveController.StopChasing();
            }
            else if (moveController.GetIsMoving())
            {
                moveController.StopMoving();
            }
            GetComponent<Animator>().SetTrigger("DeathTrigger");
        }
        else if (unitProperties.health <= 0 && GetComponent<BuildingsSelectTools>() != null)
        {
            Destroy(GetComponent<BuildingsSelectTools>().GetMenuUI());
            Destroy(gameObject);
        }
        else if (unitProperties.health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
