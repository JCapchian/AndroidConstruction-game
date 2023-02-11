using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{



    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            //Almacenar
            //other.gameObject.GetComponent<InventoryContoller>().ammo += 1;
            //Informacion a mostrar en consola
            Debug.Log("+1 Bala");
            Destroy(gameObject);
        }
    }
}
