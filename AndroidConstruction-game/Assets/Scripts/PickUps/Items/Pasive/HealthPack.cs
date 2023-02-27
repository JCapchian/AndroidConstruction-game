using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPack : PickUp
{
    // Attribute to add
    [SerializeField]
    int vidaSumar = 10;

    protected override void PickUpFunction()
    {
        if(statsManager.health < statsManager.maxHealth)
        {
            Debug.Log("Cura");
            statsManager.HealPlayer(vidaSumar);

            base.PickUpFunction();
        }
        else
            Debug.Log("VidaLLena");
    }
}
