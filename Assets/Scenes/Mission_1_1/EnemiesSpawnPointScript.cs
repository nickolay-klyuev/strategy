using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnPointScript : MonoBehaviour
{
    [SerializeField] List<GameObject> enemiesToSpawn;
    [SerializeField] float initialSpawnTime;
    [SerializeField] float spawnInterval;

    private MiniMapController miniMap;

    // Start is called before the first frame update
    void Start()
    {
        miniMap = GameObject.Find("Mini Map").GetComponent<MiniMapController>();

        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(initialSpawnTime);

        while (UnitsOnScene.GetUnits("enemy;unit").Count <= 5)
        {
            foreach (GameObject enemy in enemiesToSpawn)
            {
                GameObject enemyCreated = Instantiate(enemy, transform.position, Quaternion.identity);
                UnitsOnScene.AddUnit(enemyCreated);
                miniMap.AddIndicator(enemyCreated);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
