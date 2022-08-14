using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    //public Inventory inv;
    public string ammoType;
    public int maxAmmo;
    public int minAmmo;
    private int ammoQuantity;

    // Start is called before the first frame update
    void Start()
    {
        //inv = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void Awake() 
    {
        //inv = GameObject.Find("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            //Almacenar
            ammoQuantity = Random.Range(minAmmo,maxAmmo);
            //inv.CollectAmmo(ammoQuantity,ammoType);
            FindObjectOfType<AudioManager>().Play("PickUpAmmo");

            //Informacion a mostrar en consola
            Debug.Log("+" + ammoQuantity + " Bala");
            Destroy(gameObject);
        }
    }
}
