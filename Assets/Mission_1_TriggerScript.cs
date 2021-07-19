using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission_1_TriggerScript : MonoBehaviour
{
    public GameObject[] targets = new GameObject[4];
    private bool isComplete = false;

    private void FixedUpdate()
    {
        if (!isComplete)
        {
            bool areAllDestroyed = true;
            foreach(GameObject target in targets)
            {
                if (target != null)
                {
                    areAllDestroyed = false;
                    break;
                }
            }
            if (areAllDestroyed)
            {
                isComplete = true;
            }
        }
    }
}
