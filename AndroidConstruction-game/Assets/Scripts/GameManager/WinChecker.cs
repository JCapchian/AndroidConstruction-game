using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinChecker : MonoBehaviour
{

    public KeyCont contadorLLaves;

    public int cantidadLLaves;    

    public bool entro;

    public Pausa UI;
    // Start is called before the first frame update
    void Start()
    {
        contadorLLaves = GameObject.Find("GameManager").GetComponent<KeyCont>();    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        entro = true;
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        entro = false;
    }
   
    public void Update() {
        if(entro && Input.GetButtonDown("Submit"))
            CheckKeys();
   }

    public void CheckKeys()
    {
        if(KeyCont.redKey == true && KeyCont.greenKey == true && KeyCont.blueKey == true)
        {
            Debug.Log("GANASTE MAQUINA");
            //Destroy(GameObject.Find("Player"));
            UI.ShowWinScreen();
        }
        else
        {
            Debug.Log("Te faltan " + (3 - KeyCont.cantidadLLaves));
        }
    }

}
