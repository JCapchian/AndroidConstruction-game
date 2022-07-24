using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemCollector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log(gameObject);
        if(this.gameObject.tag == "Player")
        {
            Debug.Log("+1 Repuesto");
            Debug.Log(this.gameObject);
            //Almacenar
            Destroy(this);
        }
    }

    
}
