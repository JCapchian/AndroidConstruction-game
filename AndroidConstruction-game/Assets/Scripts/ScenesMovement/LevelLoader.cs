using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private MusicPlayer musicPlayer;

    [SerializeField]
    private Animator transition;
    [SerializeField]
    private float transitionTime = 1f;

    private void Awake() {
        if(FindObjectOfType<MusicPlayer>())
        {
            musicPlayer = FindObjectOfType<MusicPlayer>();
            musicPlayer.CheckScecne();
        }
    }

    public void LoadNextLevel()
    {
        Debug.Log("Cargar proximo nivel");
        //Load the next scene in the index
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play Animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadDemo()
    {
        StartCoroutine(Demo());
    }

    IEnumerator Demo()
    {
        //Play Animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(5);
    }
}
