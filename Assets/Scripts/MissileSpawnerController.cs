using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawnerController : MonoBehaviour
{
    public GameObject missile;
    public AudioClip launchSound;
    public float soundVolume = 0.3f;

    private GameObject targetGameObject;
    private Vector3 targetPosition;
    private bool doSpawn = false;
    private List<GameObject> createdMissiles = new List<GameObject>();
    private string parentUnitType;
    private float parentAccuracy;
    private float parentAccuracyWhileMoving;
    private float parentAttackRange;
    private bool parentAutoAim;
    private MoveController moveController;
    
    // Start is called before the first frame update
    void Start()
    {
        moveController = GetComponentInParent<MoveController>();
        parentUnitType = GetComponentInParent<UnitProperties>().unitType;
        parentAccuracy = GetComponentInParent<UnitProperties>().accuracy;
        parentAccuracyWhileMoving = GetComponentInParent<UnitProperties>().accuracyWhileMoving;
        parentAttackRange = GetComponentInParent<UnitProperties>().attackRange;
        parentAutoAim = GetComponentInParent<UnitProperties>().autoAim;

        // create 2 missiles, just in case
        for (int i = 0; i < 2; i++)
        {
            GameObject createdMissile = Instantiate(missile, transform.position, transform.rotation);
            createdMissile.GetComponent<MissileController>().SetParentUnitType(parentUnitType);
            createdMissile.SetActive(false);
            createdMissiles.Add(createdMissile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doSpawn)
        {
            for (int i = 0; i < createdMissiles.Count; i++)
            {
                if (createdMissiles[i].activeSelf)
                {
                    continue;
                }
                createdMissiles[i].transform.position = transform.position;
                createdMissiles[i].transform.rotation = transform.rotation;
                createdMissiles[i].SetActive(true);
                MissileController missileController = createdMissiles[i].GetComponent<MissileController>();
                if (!missileController.GetIsFlying())
                {
                    if (parentAutoAim) // for units with auto aim
                    {
                        missileController.LunchMissile(targetGameObject);
                    }
                    else
                    {
                        // change target possition by accuracy 
                        if (moveController.GetIsMoving())
                        {
                            targetPosition += new Vector3(Random.Range(-parentAccuracyWhileMoving, parentAccuracyWhileMoving), 
                                                            Random.Range(-parentAccuracyWhileMoving, parentAccuracyWhileMoving), 0);
                        }
                        else
                        {
                            targetPosition += new Vector3(Random.Range(-parentAccuracy, parentAccuracy), Random.Range(-parentAccuracy, parentAccuracy), 0);
                        }

                        if (GetComponent<AudioSource>() != null)
                        {
                            GetComponent<AudioSource>().PlayOneShot(launchSound, soundVolume);
                        }

                        // missiles are flying always to attack radius
                        missileController.LunchMissile(StaticMethods.GetMaxAttackRangePosition(transform.parent.position, targetPosition, parentAttackRange));
                    }
                }
                doSpawn = false;
                break;
            }
        }
    }

    void OnDestroy()
    {
        foreach (GameObject missile in createdMissiles)
        {
            Destroy(missile);
        }
    }

    public void SpawnMissile(GameObject target)
    {
        if (target != null)
        {
            targetGameObject = target;
            targetPosition = target.transform.position;
            doSpawn = true;
        }
    }
}
