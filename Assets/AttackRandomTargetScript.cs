using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRandomTargetScript : MonoBehaviour
{
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<MoveController>().MoveToPoint(new Vector2(44.38f, 1.44f));
        /*if (target != null)
        {
            GetComponent<MoveController>().MoveToPoint(target.position);
        }
        else
        {
            List<GameObject> friendlyUnits = UnitsOnScene.GetUnits("friendly;unit");
            friendlyUnits.AddRange(UnitsOnScene.GetUnits("friendly;building"));
            target = friendlyUnits[Random.Range(0, friendlyUnits.Count)].GetComponentInChildren<UnitProperties>().transform;
        }*/
    }
}
