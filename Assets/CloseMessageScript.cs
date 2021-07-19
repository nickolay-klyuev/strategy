using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMessageScript : MonoBehaviour
{
    public void CloseMessage()
    {
        Destroy(transform.parent.gameObject);
    }
}
