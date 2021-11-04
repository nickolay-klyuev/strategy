using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicBackgroundMainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip mainMenuMusic;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = mainMenuMusic;
        audioSource.Play();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("Mission"))
        {
            Destroy(gameObject);
        }
    }
}
