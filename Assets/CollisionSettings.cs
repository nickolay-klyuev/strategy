using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSettings : MonoBehaviour
{
    [SerializeField] private bool reuseCollisionCallbacks;

    void Awake()
    {
        Physics.reuseCollisionCallbacks = reuseCollisionCallbacks;
    }
}
