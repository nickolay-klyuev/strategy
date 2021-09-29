using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKeyPointController : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnObjects;

    private int enemyResourses = 0;
    private int enemyResoursesIncome = 500;
    private int wavesCount = 0;

    private bool isInvadingStarted = false;

    private Transform spawnPoint;
    private MiniMapController miniMapController;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.Find("Spawn Point");
        miniMapController = GameObject.Find("Mini Map").GetComponent<MiniMapController>();
        InvokeRepeating("StartInvade", 30f, 30f);
    }

    private bool isSpawnCoroutineWorking = false;

    private void FixedUpdate()
    {
        if (!isSpawnCoroutineWorking && isInvadingStarted)
        {
            StartCoroutine(Spawn());
        }

        if (wavesCount >= 10)
        {
            LevelsController.LoadNextScene();
        }
    }

    private void StartInvade()
    {
        wavesCount++;
        isInvadingStarted = true;
        enemyResourses += enemyResoursesIncome;
        enemyResoursesIncome += 100;
    }

    IEnumerator Spawn()
    {
        isSpawnCoroutineWorking = true;
        //if (GameObject.FindGameObjectsWithTag(spawnObject.name).Length < spawnObject.GetComponent<UnitProperties>().limit)
        //{
            while (isInvadingStarted)
            {
                yield return new WaitForSeconds(1f);

                GameObject unit = spawnObjects[Random.Range(0, spawnObjects.Length)];
                if (SpendResource(unit.GetComponentInChildren<UnitProperties>().GetCost()))
                {
                    GameObject newUnit = Instantiate(unit, transform.position, Quaternion.identity);
                    miniMapController.AddIndicator(newUnit);
                    UnitsOnScene.AddUnit(newUnit);
                }
                else
                {
                    isInvadingStarted = false;
                }
            }
        //}
        isSpawnCoroutineWorking = false;
    }

    private bool SpendResource(int amount)
    {
        if (amount > enemyResourses)
        {
            Debug.Log("Not enough resources");
            return false;
        }
        else
        {
            enemyResourses -= amount;
            return true;
        }
    }
}
