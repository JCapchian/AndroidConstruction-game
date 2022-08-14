using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PickUp_ItemActive : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private GameObject activeSpace;
    private GameObject imageKey;
    private Collider2D pickArea;
    private PlayerScript playerScript;

    private void Awake(){
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        imageKey = this.transform.GetChild(0).gameObject;
        pickArea = this.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player" )
        {
            Debug.Log(this.name + ": Entro en su zona");

            imageKey.active = true;
            activeSpace = other.gameObject.transform.GetChild(1).gameObject;
            playerScript = other.gameObject.GetComponent<PlayerScript>();
        }
    }

    private void OnTriggerStay2D(Collider2D other){
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player" && Keyboard.current.eKey.isPressed)
            Pick();

    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player")
        {
            Debug.Log(this.name + ": Salio de su zona");

            imageKey.active = false;
            activeSpace = null;
            playerScript = null;
        }
    }

    void Pick(){
        Debug.Log("Agarro el Item: " + gameObject.name);

        spriteRenderer.enabled = false;
        imageKey.active = false;
        pickArea.enabled = false;
        playerScript.AssigItemActive(this.gameObject);

        // Attach the item into the player inventory
        this.transform.parent = activeSpace.transform;
        this.transform.position = activeSpace.transform.position;
    }

    public void Drop(){
        Debug.Log("Solto el Item: " + gameObject.name);

        spriteRenderer.enabled = true;
        imageKey.active = false;
        //playerScript.AssigItemActive(this.gameObject);
         
    }
}
