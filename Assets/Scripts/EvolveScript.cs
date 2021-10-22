using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolveScript : MonoBehaviour
{
    public float evolveSpeed = 5f;
    public GameObject evolveObject;

    private MiniMapController miniMapController;

    // Start is called before the first frame update
    void Start()
    {
        miniMapController = GameObject.Find("Mini Map").GetComponent<MiniMapController>();

        StartCoroutine(Evolve());
    }

    IEnumerator Evolve()
    {
        while (true)
        {
            yield return new WaitForSeconds(evolveSpeed);
            List<GameObject> allEnemyUnits = UnitsOnScene.GetUnits("enemy;unit");
            if (allEnemyUnits.Count == 0)
            {
                continue;
            }
            GameObject randomUnit = allEnemyUnits[(int)Random.Range(0, allEnemyUnits.Count - 1)];
            Vector3 unitPosition = randomUnit.transform.position;
            randomUnit.GetComponent<DestroyScript>().DestroyThisGameObject();
            GameObject evolvedUnit = Instantiate(evolveObject, unitPosition, Quaternion.identity);
            miniMapController.AddIndicator(evolvedUnit);
            UnitsOnScene.AddUnit(evolvedUnit);
        }
    }
}
