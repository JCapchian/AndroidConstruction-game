using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScrap : PickUp
{
    protected override void PickUpFunction()
    {
        inventoryManager.scrapAmount += 1;

        base.PickUpFunction();
    }
}
