using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Managers References
    protected InventoryManager inventoryManager;
    //Component
    [SerializeField]
    protected AudioSource audioSource;
    [SerializeField]
    protected AudioClip pickUpClip;

    protected virtual void Awake()
    {

    }

    protected virtual void PickUpFunction()
    {
        audioSource.PlayOneShot(pickUpClip);

        // Destruyo el objeto
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Obtengo la referencia del jugador
        if(other.GetComponent<InventoryManager>())
        {
            inventoryManager = other.GetComponent<InventoryManager>();
            PickUpFunction();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Vacio la referencia
        if(other.GetComponent<InventoryManager>())
        {
            inventoryManager = null;
        }
    }
}
