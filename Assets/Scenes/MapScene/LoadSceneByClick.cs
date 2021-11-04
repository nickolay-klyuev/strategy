using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneByClick : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnMouseDown()
    {
        LevelsController.LoadSceneByName(sceneName);
    }
}
