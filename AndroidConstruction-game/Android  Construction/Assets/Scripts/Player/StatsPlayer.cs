using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class StatsPlayer : MonoBehaviour
{   
    public int vidaMaxima = 100;
    public int vida = 90;
    public int energia;
    public bool muerto;

    public HealthBar hB;
    public TMP_Text uiCurrent;
    public TMP_Text uiMax;

    public Animator transition;

    public float transitionTime = 1f;

    private void Start() 
    {
        hB.SetMaxHealth(vidaMaxima);
        hB.SetHealth(vida);
        
        muerto = false;

        uiCurrent.text = vida.ToString();
        uiMax.text = vidaMaxima.ToString();      
    }
    public void Update()
    {
        if(muerto == true)
        {
            ReinicioEscena();
        }
    }

    public void ReinicioEscena()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
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

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other);
        if(other.gameObject.tag == "EnemyProyectile")
        {
            Debug.Log("Entro");
            PerderVida(10);
        }
            
    }

    public void SumarVida(int vidaASumar)
    {
        Debug.Log(vida);
        vida += vidaASumar;
        hB.SetHealth(vida);
        uiCurrent.text = vida.ToString();
    }

    public void PerderVida(int vidaARestar)
    {
        if(vida <= 0)
        {
            muerto = true;
        }
        else
        {
            vida = vida - vidaARestar;
            hB.SetHealth(vida);
            uiCurrent.text = vida.ToString();
            Debug.Log(vida);
        }
        
    }

}   
