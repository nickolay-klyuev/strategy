using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalModeController : MonoBehaviour
{
    [SerializeField] private GameObject resultsBlock;
    private Text timeSurvivedText;
    private Text gotExperienceText;

    static private float timeSurvived = 0f;
    static public float GetTimeSurvived()
    {
        return timeSurvived;
    }

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        timeSurvivedText = resultsBlock.transform.GetChild(1).GetComponent<Text>();
        gotExperienceText = resultsBlock.transform.GetChild(2).GetComponent<Text>();

        resultsBlock.SetActive(false);

        StartCoroutine(TimeSurviveCounter());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (UnitsOnScene.GetUnits("friendly;building").Count == 0 && UnitsOnScene.GetUnits("friendly;unit").Count == 0)
        {
            gameOver = true;
        }

        if (gameOver)
        {
            Time.timeScale = 0;

            timeSurvivedText.text = $"Time survived: {timeSurvived} sec.";
            gotExperienceText.text = $"Got experience: {GlobalExpSystem.GetExp()} exp.";

            resultsBlock.SetActive(true);
        }
    }

    IEnumerator TimeSurviveCounter()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(1);
            timeSurvived++;
        }
    }

    public void FinishGame()
    {
        LevelsController.LoadSceneByName("Main_menu");
    }
}
