using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Mission_1_TriggerScript : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private int targetQuantity;
    [SerializeField] private GameObject goalDisplay;

    [SerializeField] private bool doDestroy = true;
    [SerializeField] private string goalText = "Worm holes left: ";

    private bool isComplete = false;
    private Text goalTextDisplay;
    private PostEffectsScript postEffects;

    private void Start()
    {
        postEffects = GameObject.Find("Post processing volume").GetComponent<PostEffectsScript>();
        goalTextDisplay = goalDisplay.GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (!isComplete)
        {
            int generatorsCount = GameObject.FindGameObjectsWithTag("Resources Gatherer Friendly").Length;

            goalTextDisplay.text = $"{goalText}{generatorsCount}/{targetQuantity}";

            if (generatorsCount >= targetQuantity)
            {
                isComplete = true;
                postEffects.StartWinAnimation();
                // next level load from PostEffectsScript after animation
            }
        }
    }
}
