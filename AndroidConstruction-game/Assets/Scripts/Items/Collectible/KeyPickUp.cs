using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    private KeyCont contadorKeys;

    public Doors puerta;

    // Start is called before the first frame update
    void Start()
    {
        contadorKeys = GameObject.Find("GameManager").GetComponent<KeyCont>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Agarraste " + this.gameObject.name);

            contadorKeys.KeyPickUp(this.gameObject.name);
            puerta.AbrirPuerta(this.gameObject.name);
            FindObjectOfType<AudioManager>().Play("KeyPick");

            Destroy(gameObject);

        }
        
    }
}
