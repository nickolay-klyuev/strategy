using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private UnitProperties currentUnitProperties;
    private GameObject targetGameobject;

    // Start is called before the first frame update
    void Start()
    {
        currentUnitProperties = GetComponent<UnitProperties>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAttack(GameObject target)
    {
        Debug.Log($"attack this target: {target.name}");
        targetGameobject = target;
    }
}
