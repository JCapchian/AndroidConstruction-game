using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : PickUp
{
    [SerializeField]
    private Doors door;
    enum KeyType
    {
        greenKey,
        redKey,
        blueKey
    }
    [SerializeField]
    KeyType keyType;
    public string keyName;

    protected override void Awake()
    {
        // Defino el tipo de llave
        switch (keyType)
        {
            case KeyType.blueKey:
                keyName = "blueKey";
                break;
            case KeyType.greenKey:
                keyName = "greenKey";
                break;
            case KeyType.redKey:
                keyName = "redKey";
                break;
        }
    }

    protected override void PickUpFunction()
    {
        // Guardo la llave en el inventario
        inventoryManager.PickUpKey(keyName);

        door.AbrirPuerta();

        // Continua la funcion base
        base.PickUpFunction();
    }
}
