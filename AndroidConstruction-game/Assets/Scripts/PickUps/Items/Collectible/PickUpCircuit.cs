using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCircuit : PickUp
{
    protected override void PickUpFunction()
    {
        inventoryManager.circuitsAmount++;

        base.PickUpFunction();
    }
}
