using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPasive : MonoBehaviour
{
    // Attribute to add
    public int vidaSumar = 10;
    public StatsPlayer sP;

    //Show the stats

    private void Start() {
        sP = GameObject.Find("Player").GetComponent<StatsPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            sP = GameObject.Find("Player").GetComponent<StatsPlayer>();
            PickItem();
        }
    }


    public void PickItem()
    {
        if(sP.vida < sP.vidaMaxima)
        {
            sP.SumarVida(vidaSumar);
            FindObjectOfType<AudioManager>().Play("Curar");
            Destroy(gameObject);
        }
        else
            Debug.Log("VidaLLena");
        
    }
}
