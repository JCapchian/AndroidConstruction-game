using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip menuMusic;
    [SerializeField]
    AudioClip gameMusic;
    [SerializeField]
    bool isInMenu;
    [SerializeField]
    bool isInGame;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.Stop();

        DontDestroyOnLoad(this.gameObject);

        Debug.Log("Desperto");
        CheckScecne();

    }
    public void CheckScecne()
    {
        var actualScene = SceneManager.GetActiveScene();

        if(actualScene.buildIndex < 1)
            isInMenu = true;
        else
            isInMenu = false;

        if(actualScene.buildIndex > 1)
            isInGame = true;
        else
            isInGame = false;

        LoadClip();
    }

    private void LoadClip()
    {
        if(isInMenu)
        {
            audioSource.clip = menuMusic;
            audioSource.Play();
        }
        if(isInGame && audioSource.clip != gameMusic)
        {
            audioSource.clip = gameMusic;
            audioSource.Play();
        }
    }
}
