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
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = mainMenuMusic;
        audioSource.Play();
    }
}
