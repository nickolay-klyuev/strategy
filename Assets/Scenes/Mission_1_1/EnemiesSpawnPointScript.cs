using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnPointScript : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesToSpawn;
    [SerializeField] float initialSpawnTime;
    [SerializeField] float spawnInterval;

    private MiniMapController miniMap;
    private int maxEnemies = 3;

    // Start is called before the first frame update
    void Start()
    {
        miniMap = GameObject.Find("Mini Map").GetComponent<MiniMapController>();

        StartCoroutine(SpawnEnemies());
        StartCoroutine(DoMoreEnemies());
    }

    IEnumerator DoMoreEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            maxEnemies++;
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(initialSpawnTime);

        while (true)
        {
            if (UnitsOnScene.GetUnits("enemy;unit").Count <= maxEnemies)
            {
                foreach (GameObject enemy in enemiesToSpawn)
                {
                    GameObject enemyCreated = Instantiate(enemy, transform.position, Quaternion.identity);
                    UnitsOnScene.AddUnit(enemyCreated);
                    miniMap.AddIndicator(enemyCreated);
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
