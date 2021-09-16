using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIdentityScript : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
