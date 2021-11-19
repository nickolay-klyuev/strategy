using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{
    static public float givingResourcesBack = 0f; // skill OldMaterialGathering
    static public float gatherFromDeadEnemies = 0f; // skill Vultures
    static public bool isDieHard = false; // skill DieHard

    private UnitProperties unitProperties;
    private MoveController moveController;
    private AttackController attackController;
    private AudioSource audioSource;

    private bool destroyTriggered = false;

    public void DestroyThisGameObject()
    {
        destroyTriggered = true;

        if (isDieHard && unitProperties.unitType == "friendly" && unitProperties.isBuilding == false)
        {
            StartCoroutine(DieHard());
        }
        else
        {
            ThisDestroy();
        }

        if (givingResourcesBack > 0 && unitProperties.unitType == "friendly" && unitProperties.isBuilding == false)
        {
            ResourceSystem.GatherResource((int)((float)unitProperties.cost * givingResourcesBack));
        }

        if (gatherFromDeadEnemies > 0 && unitProperties.unitType == "enemy" && unitProperties.isBuilding == false)
        {
            ResourceSystem.GatherResource((int)((float)unitProperties.cost * gatherFromDeadEnemies));
        }
    }

    private void ThisDestroy()
    {
        Destroy(gameObject);
        UnitsOnScene.RemoveUnit(gameObject);
    }

    IEnumerator DieHard()
    {
        yield return new WaitForSeconds(3f);
        ThisDestroy();
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
        audioSource = GetComponent<AudioSource>();
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

    private bool deathSoundPlayed = false;
    // Update is called once per frame
    void Update()
    {
        if (!destroyTriggered)
        {
            if (unitProperties.health <= 0 && unitProperties.hasDeathAnimation) // trigger death animation and stop moving during it
            {   
                if (audioSource != null && !deathSoundPlayed)
                {
                    deathSoundPlayed = true;
                    audioSource.Play();
                }

                if (!unitProperties.isBuilding)
                {
                    moveController.StopChasing();
                    moveController.StopMoving();
                    attackController.StopAttack();
                }

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
}
