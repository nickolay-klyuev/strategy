using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mission_1_TriggerScript : MonoBehaviour
{
    public GameObject[] targets = new GameObject[4];
    public GameObject goalDisplay;
    private bool isComplete = false;
    private int targetsLeft;
    private string goalText = "Worm holes left: ";
    private PostEffectsScript postEffects;

    private void Start()
    {
        postEffects = GameObject.Find("Post processing volume").GetComponent<PostEffectsScript>();
        goalDisplay.GetComponent<Text>().text = $"{goalText}{targets.Length};";
    }

    private void FixedUpdate()
    {
        if (!isComplete)
        {
            targetsLeft = 0;
            bool areAllDestroyed = true;
            foreach(GameObject target in targets)
            {
                if (target != null)
                {
                    targetsLeft++;
                    areAllDestroyed = false;
                }
            }
            if (areAllDestroyed)
            {
                isComplete = true;
                postEffects.StartWinAnimation();
                // next level load from PostEffectsScript after animation
            }
        }
        goalDisplay.GetComponent<Text>().text = $"{goalText}{targetsLeft};";
    }
}
