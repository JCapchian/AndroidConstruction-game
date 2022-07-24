using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Weapon Position
    private GameObject weaponContainer;
    public GameObject weaponActive;
    public SpriteRenderer weaponSprite;
    private WeaponHUB hubRef;
    private int activeWeapon;
    [Header("Municion")]
    private int maxGreyAmmo;
    private int maxGreenAmmo;
    private int maxPurpleAmmo;
    public int greyAmmo;
    public int greenAmmo;
    public int purpleAmmo;

    public void WeaponCall()
    {
        Debug.Log(weaponContainer.transform.childCount);
        for (var i = 0; i < weaponContainer.transform.childCount; i++)
            Debug.Log(weaponContainer.transform.GetChild(i).gameObject.name);

        if (weaponContainer.transform.childCount == 2)
        {
            //Disable the first weapon
            weaponContainer.transform.GetChild(0).gameObject.SetActive(false);
            weaponContainer.transform.GetChild(0).gameObject.GetComponent<Weapon_Script>().enabled = false;

            //Set the second weapon = 0
            activeWeapon = 1;
            weaponActive = weaponContainer.transform.GetChild(1).gameObject;
            weaponSprite =  weaponActive.gameObject.GetComponent<SpriteRenderer>();
            Debug.Log("Arma activa " + activeWeapon);

            hubRef.UpdateIcon(weaponActive.GetComponent<SpriteRenderer>().sprite);
            hubRef.ColorSet();
            hubRef.AmmoUpdate();
        }
        else
        {
            //Set the first weapon = 0
            activeWeapon = 0;
            weaponActive = weaponContainer.transform.GetChild(0).gameObject;
            weaponSprite =  weaponActive.gameObject.GetComponent<SpriteRenderer>();

            Debug.Log("Arma activa " + activeWeapon);
            Debug.Log(weaponActive.name + " Arma");

            hubRef.UpdateIcon(weaponSprite.sprite);
            hubRef.ColorSet();
            hubRef.AmmoUpdate();
        }
        if (weaponContainer.transform.childCount == 3)
        {
            //Endable the first weapon
            weaponContainer.transform.GetChild(0).gameObject.SetActive(true);
            //Disinherit the first weapon -- childCount = 2
            weaponContainer.transform.GetChild(0).gameObject.transform.parent = null;
            //Call the funtion to Unequip the weapon
            weaponContainer.transform.GetChild(0).gameObject.GetComponent<PickUp_Weapon>().UnequipWeapon();

            //Disable the second weapon
            weaponContainer.transform.GetChild(0).gameObject.SetActive(false);

            //Set the second weapon = 0
            activeWeapon = 1;

            weaponActive = weaponContainer.transform.GetChild(1).gameObject;
            weaponSprite =  weaponActive.gameObject.GetComponent<SpriteRenderer>();

            hubRef.UpdateIcon(weaponActive.GetComponent<SpriteRenderer>().sprite);
            hubRef.ColorSet();
            hubRef.AmmoUpdate();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Save the weapon position
        weaponContainer = this.gameObject.transform.GetChild(0).gameObject;

        greyAmmo = maxGreyAmmo;
        greenAmmo = maxGreenAmmo;
        purpleAmmo = maxPurpleAmmo;

        hubRef = GameObject.Find("UIArma").GetComponent<WeaponHUB>();
    }

    // Update is called once per frame
    void Update()
    {
        //Change weapon
        if(Input.GetKeyDown(KeyCode.Q) && weaponActive.GetComponent<Weapon_Script>().reloading == false)
        {
            if(weaponContainer.transform.childCount > 1)
            {
                ChangeWeapon();
            }
            else
            {
                Debug.Log("No tenes equipada ningun arma o no tenes secundaria");
            }
        }
    }

    private void ChangeWeapon()
    {
        if(activeWeapon == 0)
        {
            //Active the second weapon
            weaponContainer.transform.GetChild(0).gameObject.SetActive(false);
            weaponContainer.transform.GetChild(1).gameObject.SetActive(true);
        
            weaponContainer.transform.GetChild(0).gameObject.GetComponent<Weapon_Script>().enabled = false;
            weaponContainer.transform.GetChild(1).gameObject.GetComponent<Weapon_Script>().enabled = true;
            
            weaponActive = weaponContainer.transform.GetChild(1).gameObject;
            weaponSprite =  weaponActive.gameObject.GetComponent<SpriteRenderer>();

            hubRef.UpdateIcon(weaponActive.GetComponent<SpriteRenderer>().sprite);
            hubRef.ColorSet();
            hubRef.AmmoUpdate();

            activeWeapon = 1;
        }
        else if(activeWeapon == 1)
        {
            //Active the first weapon
            weaponContainer.transform.GetChild(1).gameObject.SetActive(false);
            weaponContainer.transform.GetChild(0).gameObject.SetActive(true);

            weaponContainer.transform.GetChild(1).gameObject.GetComponent<Weapon_Script>().enabled = false;
            weaponContainer.transform.GetChild(0).gameObject.GetComponent<Weapon_Script>().enabled = true;

            weaponActive = weaponContainer.transform.GetChild(0).gameObject;
            weaponSprite =  weaponActive.gameObject.GetComponent<SpriteRenderer>();
            
            hubRef.UpdateIcon(weaponActive.GetComponent<SpriteRenderer>().sprite);
            hubRef.ColorSet();
            hubRef.AmmoUpdate();

            activeWeapon = 0;
        }
    }

    public int GetAmmo(string ammoType)
    {
        switch (ammoType)
        {
            case "Grey":
                return greyAmmo;
            case "Green":
                return greenAmmo;
            case "Purple":
                return purpleAmmo;
        }
        return 0;        
    }

    public void UsedAmmo(int ammoQuantity, string ammoType)
    {
        switch (ammoType)
        {
            case "Grey":
                greyAmmo -= ammoQuantity;
                break;
            case "Green":
                greenAmmo -= ammoQuantity;
                break;
            case "Purple":
                purpleAmmo -= ammoQuantity;
                break;
        }

        hubRef.AmmoUpdate();
    }

    public void CollectAmmo(int ammoQuantity, string ammoType)
    {
        switch (ammoType)
        {
            case "Grey":
                greyAmmo += ammoQuantity;
                break;
            case "Green":
                greenAmmo += ammoQuantity;
                break;
            case "Purple":
                purpleAmmo += ammoQuantity;
                break;
        }
        
        hubRef.AmmoUpdate();
    }

    public void DisableWeapon()
    {
        weaponActive.SetActive(false);
    }
}
