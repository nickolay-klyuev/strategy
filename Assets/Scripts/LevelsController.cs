using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsController : MonoBehaviour
{
    public static void LoadNextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadSceneByName(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public static void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void QuitTheGame()
    {
        Application.Quit();
    }
}
