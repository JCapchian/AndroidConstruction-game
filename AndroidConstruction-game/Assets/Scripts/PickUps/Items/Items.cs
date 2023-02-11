using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : Interactable
{

    public override void Interact(GameObject playerReference)
    {
        Debug.Log("Recogio: " + name);

        Destroy(this.gameObject);
    }

    
    
}
