using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public GameObject activeSlot;

    static public GameObject weaponEquiped;
    static public GameObject activeEquiped;
    public GameObject weaponContainer;

    //Cheks if the player has equiped a weapon
    public bool equiped;
    //Cheks if the player has a active item active
    public bool slotEmpty;

    //Keeps the item the player pick
    private GameObject ItemPickUp;

    //Reference the items class
    public ItemPasive itemP;
    public ItemActive itemA;

    private void Update() {
        /*
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(!slotEmpty)
                Debug.Log("No hay item equipado");
            else
                StartCoroutine(itemA.UseItem());
                
        }
        */
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        //Debug.Log(other.gameObject.tag);
        ItemPickUp = other.gameObject;

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!equiped && other.gameObject.tag == "Weapon")
            {
                EquipWeapon(ItemPickUp);
            }
            if(other.gameObject.tag == "Pasive")
            {
                EquipPasive(ItemPickUp);
            }
            if(!slotEmpty && other.gameObject.tag == "Active")
            {
                EquipActive(ItemPickUp);
            }
        }
    }

    private void EquipWeapon(GameObject Weapon)
    {
        equiped = true;
        
        weaponEquiped = Weapon;

        // Moves the weapon in the position of the weapon container and set as a Parent
        weaponEquiped.transform.SetParent(weaponContainer.transform);
        weaponEquiped.transform.position = weaponContainer.transform.position;
        weaponEquiped.transform.rotation = weaponContainer.transform.rotation;

        // Enable the script of the weapon
        weaponEquiped.GetComponent<Weapon_Script>().enabled = true;
    }

    private void EquipPasive(GameObject Pasive)
    {
        itemP.PickItem();

        Destroy(Pasive);
    }

    private void EquipActive(GameObject Active)
    {
        //Equip the active
        itemA.PickItem();

        //Active the scritp
        slotEmpty = true;
        activeEquiped = Active;
        activeEquiped.GetComponent<ItemActive>().enabled = true;

        //Assign the sprite to the UI
        activeSlot.GetComponentInChildren<Image>().color = activeEquiped.GetComponent<SpriteRenderer>().color;

        //Destroy the object on the ground
    }

    private void Drop()
    {
        equiped = false;

    }

}
