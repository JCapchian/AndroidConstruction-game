using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class Interactable : MonoBehaviour
{
    [Header ("Componentes de los interactuables")]
    [SerializeField]
    private GameObject iconKey;


    [Header ("Scripts de los interactuables")]
    [SerializeField]
    protected InventoryManager inventoryManager;
    [SerializeField]
    protected PlayerManager playerManager;
    [SerializeField]
    protected GUIManager gUIManager;

    protected virtual void Awake()
    {
        iconKey = transform.GetChild(0).gameObject;
    }

    /// <summary>Funcion al interactuar con el objeto</summary>
    public virtual void Interact(GameObject playerReference)
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D other){
        if(other.GetComponent<InventoryManager>())
        {
            Debug.Log(this.name + ": Entro en su zona");
            inventoryManager = other.GetComponent<InventoryManager>();
            playerManager = other.GetComponent<PlayerManager>();
            gUIManager = playerManager.gUIManager;

            // Activo el icono del interactuable
            iconKey.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other){
        if(other.GetComponent<InventoryManager>())
        {
            Debug.Log(this.name + ": Salio de su zona");
            //inventoryManager = null;

            // Desactivo el icono del interactuable
            iconKey.SetActive(false);
        }
    }

}
