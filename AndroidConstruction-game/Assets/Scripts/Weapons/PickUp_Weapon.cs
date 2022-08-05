using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUp_Weapon : MonoBehaviour
{
    //Referencial al sistema de inventario
    public Inventory WeaponInventory;
    //Referencia al container del arma del jugador
    public GameObject weaponContainer;
    public GameObject imageKey;

    public bool entro = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            //Debug.Log("Entro");

            imageKey.SetActive(true);

            entro = true;
            Debug.Log(entro);
            
            WeaponInventory = other.gameObject.transform.GetComponent<Inventory>();
            weaponContainer = other.gameObject.transform.GetChild(0).gameObject;
        }   
    }

    private void Update() 
    {
        if(Input.GetButtonDown("Submit") && entro == true)
            EquipWeapon();
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        entro = false;
        imageKey.SetActive(false);
        //Debug.Log(entro);
    }
    /*
    private void OnTriggerStay2D(Collider2D other) 
    {
        Debug.Log("Agarrar");
        if(other.gameObject.tag == "Player" && Input.GetButtonDown("Submit"))
            EquipWeapon();
    }
    */
    public void EquipWeapon()
    {
        
        gameObject.GetComponent<CircleCollider2D>().enabled = false;

        gameObject.transform.SetParent(weaponContainer.transform);
        gameObject.transform.position = weaponContainer.transform.position;
        gameObject.transform.rotation = weaponContainer.transform.rotation;

        // Enable the script of the weapon
        this.GetComponent<Weapon_Script>().enabled = true;
        
        WeaponInventory.WeaponCall();
    }
    public void UnequipWeapon()
    {
        gameObject.transform.rotation = Quaternion.identity;
        //Se vuelve a activar su colision
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        // Disable the script of the weapon
        this.GetComponent<Weapon_Script>().enabled = false;
    }
    
    


}
