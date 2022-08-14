using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PickUp_Gun : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject gunContainer;
    [SerializeField]
    private GameObject imageKey;
    [SerializeField]
    private PlayerScript playerScript;
    [SerializeField]
    private Collider2D pickArea;

    private void Awake(){
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        imageKey = this.gameObject.transform.GetChild(0).gameObject;
        pickArea = this.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player" )
        {
            Debug.Log(this.name + ": Entro en su zona");

            imageKey.active = true;
            gunContainer = other.gameObject.transform.GetChild(0).GetChild(0).gameObject;
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
        }
    }

    private void Pick(){
        Debug.Log("Agarro el Arma: " + gameObject.name);

        imageKey.active = false;
        playerScript.AssignGuns(this.gameObject);
        pickArea.enabled = false;

        // Attach the item into the player inventory
        this.transform.parent = gunContainer.transform;
        this.transform.position = gunContainer.transform.position;
    }

    public void Drop(){
        Debug.Log("Solto el Item: " + gameObject.name);

        imageKey.active = false;
        pickArea.enabled = true;
        this.gameObject.active = true;
        //playerScript.PickGun(this.gameObject);
         
    }
}
