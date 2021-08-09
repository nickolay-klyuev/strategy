using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseMessageScript : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }

    public void CloseMessage()
    {
        transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
