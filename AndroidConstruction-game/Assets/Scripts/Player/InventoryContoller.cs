using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContoller : MonoBehaviour
{
    #region Weapon Attributes
    public GameObject weaponContainer;
    public GameObject newWeapon;
    public Weapon_Script WeaponCode;
    //public PickUp_Weapon PKPW;
    public int weaponCount;
    private int activeWeapon = 0;
    //public int maxAmmo;
    public int ammo;
    #endregion

    #region ItemActive Attributes
    public bool slot;
    private bool coolDown;
    public ItemActive IA;
    public GameObject habilidadesSlot;
    public GameObject shieldObject;
    #endregion
    
    private void Start() {
        habilidadesSlot = gameObject.transform.GetChild(1).gameObject;
        slot = false;
        coolDown = false;
    }

    public void ShowWeapons()
    {
        Debug.Log(weaponContainer.transform.childCount);
        weaponCount = weaponContainer.transform.childCount;
        //PKPW = weaponContainer.transform.GetChild(weaponCount-1).GetComponent<PickUp_Weapon>();
        if(activeWeapon == 0)
            activeWeapon = 1;

        else if(activeWeapon == 1)
            activeWeapon = 2;
        
        //activeWeapon = weaponCount;
        
    }

    public void ShowActive()
    {
        Debug.Log(gameObject.transform.childCount);
        IA = gameObject.transform.GetChild(2).GetComponent<ItemActive>();
        shieldObject = habilidadesSlot.transform.GetChild(0).gameObject;
        slot = true;
    }

    public void EquipNewWeapon()
    {
        for (var i = 0; i < weaponCount; i++)
            {
                weaponContainer.transform.GetChild(i).gameObject.SetActive(false);
                Debug.Log(weaponContainer.transform.GetChild(i).gameObject.name);
                Debug.Log(weaponCount + "Cantidad");
            }

            weaponContainer.transform.GetChild(weaponCount-1).gameObject.SetActive(true);
            Debug.Log(weaponContainer.transform.GetChild(weaponCount-1).gameObject.name);

        /*
        if(weaponCount > 2)
        {
            if(activeWeapon == 1)
            {
                weaponContainer.transform.GetChild(2).gameObject.transform.parent = null;
                PKPW.UnequipWeapon();
            }
            if(activeWeapon == 2)
            {
                weaponContainer.transform.GetChild(1).gameObject.transform.parent = null;
                PKPW.UnequipWeapon();
            }
            Debug.Log(weaponContainer.transform.GetChild(activeWeapon).gameObject.name);
            //weaponContainer.transform.GetChild(ActiveWeapon).gameObject.transform.parent = null;
            //PKPW.UnequipWeapon();
            //cont -= 1;
        }
        else
        {
            
        }
        */
    }

    public void DropWeapon()
    {
        if(activeWeapon == 1)
        {
            Debug.Log("Entre 1");
            Debug.Log(weaponContainer.transform.GetChild(2));
            weaponContainer.transform.GetChild(2).gameObject.transform.parent = null;
            //PKPW.UnequipWeapon();
        }
        if(activeWeapon == 2)
        {
            Debug.Log("Entre 2");
            Debug.Log(weaponContainer.transform.GetChild(1));
            weaponContainer.transform.GetChild(1).gameObject.transform.parent = null;
            //PKPW.UnequipWeapon();
        }
    }

    public void Update() 
    {   
        //Debug.Log(weaponCount);
        if(weaponCount == 2 && Input.GetButtonDown("Reload"))
        {
            Debug.Log(weaponContainer.transform.childCount + " op");

                if(activeWeapon == 1)
                {
                    weaponContainer.transform.GetChild(0).gameObject.SetActive(true);
                    weaponContainer.transform.GetChild(1).gameObject.SetActive(false);
                    //PKPW = weaponContainer.transform.GetChild(0).GetComponent<PickUp_Weapon>();
                    activeWeapon = 2;
                }
                else
                {
                    weaponContainer.transform.GetChild(1).gameObject.SetActive(true);
                    weaponContainer.transform.GetChild(0).gameObject.SetActive(false);
                    //PKPW = weaponContainer.transform.GetChild(1).GetComponent<PickUp_Weapon>();
                    activeWeapon = 1;
                }   
        }
            
        
        if(Input.GetButtonDown("UseAbility"))
        {
            if(slot == true && coolDown == false)
                StartCoroutine(UseItem());
            else
                Debug.Log("No tenes equipado un activo o se esta cargando el que tenes equipado");
        }
        

    }
    public IEnumerator UseItem()
    {
        Debug.Log("Usaste el item");

        coolDown = true;
        shieldObject.SetActive(true);

        yield return new WaitForSeconds(3f);
        
        shieldObject.SetActive(false);
        coolDown = false;
    }
}
