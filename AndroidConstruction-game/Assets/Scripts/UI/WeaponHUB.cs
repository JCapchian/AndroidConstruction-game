using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponHUB : MonoBehaviour
{
    public TMP_Text curretAmmo;
    public TMP_Text invAmmo;
    public Image weaponIcon;

    public Inventory invPlayer;
    
    public void UpdateIcon(Sprite Weapon)
    {
        weaponIcon.sprite = Weapon;
    }

    public void ColorSet()
    {
        switch (invPlayer.weaponActive.GetComponent<Weapon_Script>().ammoType.ToString())
        {
            case "Grey":
                invAmmo.color = new Color32(128,128,128,255);
                break;
            case "Green":
                invAmmo.color = new Color32(47, 255, 0,255);
                break;
            case "Purple":
                invAmmo.color = new Color32(149, 0, 255,255);
                break;
        }
    }
    
    public void AmmoUpdate()
    {
        curretAmmo.text = invPlayer.weaponActive.GetComponent<Weapon_Script>().currentAmmo.ToString();
        switch (invPlayer.weaponActive.GetComponent<Weapon_Script>().ammoType.ToString())
        {
            case "Grey":
                invAmmo.text = invPlayer.greyAmmo.ToString();
                break;
            case "Green":
                invAmmo.text = invPlayer.greenAmmo.ToString();
                break;
            case "Purple":
               invAmmo.text = invPlayer.purpleAmmo.ToString();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
