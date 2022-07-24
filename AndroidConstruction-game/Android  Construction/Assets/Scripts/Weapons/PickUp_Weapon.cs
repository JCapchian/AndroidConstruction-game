using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUp_Weapon : MonoBehaviour
{
    //Referencial al sistema de inventario
    public Inventory WeaponInventory;
    //Referencia al container del arma del jugador
    public GameObject weaponContainer;

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Entro");
        if(other.gameObject.tag == "Player")
        {
            WeaponInventory = other.gameObject.GetComponent<Inventory>();
            weaponContainer = other.gameObject.transform.GetChild(0).gameObject;
        }   
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(Input.GetButtonDown("Submit") && other.gameObject.tag == "Player")
            EquipWeapon();
    }

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

    /*
    public void EquipWeapon()
    {
        Debug.Log(WeaponInventory.weaponCount);
        if(WeaponInventory.weaponCount < 2 && WeaponInventory.weaponCount >= 0)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;

            Debug.Log(this.gameObject);
            //IC.EquipWeapon(this.gameObject);
            gameObject.transform.SetParent(weaponContainer.transform);
            gameObject.transform.position = weaponContainer.transform.position;
            gameObject.transform.rotation = weaponContainer.transform.rotation;

            // Enable the script of the weapon
            this.GetComponent<Weapon_Script>().enabled = true;

            WeaponInventory.ShowWeapons();
            WeaponInventory.EquipNewWeapon();
        }
        else if (WeaponInventory.weaponCount == 2)
        {
            Debug.Log("Toy lleno");
            WeaponInventory.weaponCount -= 1;
            WeaponInventory.DropWeapon();
        }
            

        
    }
    */
    public void UnequipWeapon()
    {
        gameObject.transform.rotation = Quaternion.identity;
        //Se vuelve a activar su colision
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        //Se deseactiva su script para que no pueda dispararse sola
        this.GetComponent<Weapon_Script>().enabled = false;
    }
    
    


}
