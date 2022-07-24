using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPasive : MonoBehaviour
{
    // Attribute to add
    public int vidaSumar = 10;
    public StatsPlayer sP;
    public Canvas stast;

    //Show the stats
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            sP = other.gameObject.GetComponent<StatsPlayer>();
            stast.enabled = true;
        }
    }

    //
    private void OnTriggerExit2D(Collider2D other) 
    {
        stast.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetButtonDown("Submit") && other.gameObject.tag == "Player")
        {
            PickItem();
        }
    }

    public void PickItem()
    {
        sP.SumarVida(vidaSumar);
        Destroy(gameObject);
    }
}
