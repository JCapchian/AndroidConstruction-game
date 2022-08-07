using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;

public class Pausa : MonoBehaviour
{
    public RunInfo cont;

    public GameObject pauseScreen;

    public GameObject deathScreen;
    public TMP_Text contEnemiesDEATH;
    public TMP_Text contChatarraDEATH;
    public TMP_Text contComponentesDEATH;
    

    public GameObject winScreen;
    public TMP_Text contEnemiesWIN;
    public TMP_Text contChatarraWIN;
    public TMP_Text contComponentesWIN;

    private void Awake()
    {
        Resume ();
        deathScreen.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
    }
    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
        if (Time.timeScale > 0)
            PauseGame();
        else
            Resume();
        }
        */
    }
    public void Resume()
    {
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void PauseGame()
    {
        pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0;

    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
    }

    public void SHowDeathScreen()
    {
        deathScreen.gameObject.SetActive(true);

        Time.timeScale = 0;

        contEnemiesDEATH.text = "X" + cont.enemigosElimindos.ToString();
        contChatarraDEATH.text = "X" + cont.chatarraRecoletada.ToString();
        contComponentesDEATH.text = "X" + cont.componentesRecoletados.ToString();
    }
    
    public void ShowWinScreen()
    {
        winScreen.gameObject.SetActive(true);

        Time.timeScale = 0;

        contEnemiesDEATH.text = "X" + cont.enemigosElimindos.ToString();
        contChatarraDEATH.text = "X" + cont.chatarraRecoletada.ToString();
        contComponentesDEATH.text = "X" + cont.componentesRecoletados.ToString();
    }
}
