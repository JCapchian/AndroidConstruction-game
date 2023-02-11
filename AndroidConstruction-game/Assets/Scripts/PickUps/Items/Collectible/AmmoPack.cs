using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : PickUp
{
    enum AmmoTypes
    {
        greyAmmo,
        greenAmmo,
        purpleAmmo
    }
    [SerializeField]
    AmmoTypes ammoTypes;
    
    string ammoType;
    [SerializeField]
    int maxAmmo;
    [SerializeField]
    int minAmmo;
    private int ammoQuantity;

    protected override void Awake()
    {
        // Defino el tipo de municion
        switch (ammoTypes)
        {
            case AmmoTypes.greyAmmo: 
                ammoType = "greyAmmo";
                break;
            case AmmoTypes.greenAmmo: 
                ammoType = "greenAmmo";
                break;
            case AmmoTypes.purpleAmmo:
                ammoType = "purpleAmmo";
                break;
        }

        // Genero un valor aleatorio entre dos valores
        ammoQuantity = Random.Range(minAmmo,maxAmmo);
    }

    protected override void PickUpFunction() 
    {
        // Guardo la municion en el inventario
        inventoryManager.PickUpAmmo(ammoType,ammoQuantity);
        
        base.PickUpFunction();
    }
}
