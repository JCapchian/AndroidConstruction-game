using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;


public class StatsPlayer : MonoBehaviour
{   
    public int vidaMaxima = 100;
    public int vida = 90;
    public int energia;
    public bool muerto;

    
    //public Inventory inventory;

    public HealthBar hB;
    public TMP_Text uiCurrent;
    public TMP_Text uiMax;
    public Pausa UI;

    public Animator transition;
    public Animator MyAnimator;

    public float transitionTime = 1f;

    private void Start() 
    {
        hB.SetMaxHealth(vidaMaxima);
        hB.SetHealth(vida);
        
        muerto = false;

        uiCurrent.text = vida.ToString();
        uiMax.text = vidaMaxima.ToString();     

        GameObject.DontDestroyOnLoad(this.gameObject);
    }
    public void Update()
    {
        if(vida == 0)
        {
            //movement.enabled = false;
            //inventory.DisableWeapon();

            MyAnimator.SetTrigger("Death"); 
            StartCoroutine(ReinicioEscena());
        }

    }

    IEnumerator ReinicioEscena()
    {
        //tartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        yield return new WaitForSeconds(1.5f);

        //UI.SHowDeathScreen();
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(2);
        
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
            Destroy(other.gameObject);
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

        if(vida > 0)
        {
            vida = vida - vidaARestar;
            hB.SetHealth(vida);
            uiCurrent.text = vida.ToString();
            Debug.Log(vida);
        }
        
    }

}   
