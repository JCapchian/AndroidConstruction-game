using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActive : MonoBehaviour
{
    public GameObject player;
    public PlayerScript playerScript;
    public InventoryContoller IC;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            playerScript = other.gameObject.GetComponent<PlayerScript>();
            Debug.Log(player);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetButtonDown("Submit") && other.gameObject.tag == "Player")
        {
            Debug.Log(player);
            IC = player.gameObject.GetComponent<InventoryContoller>();
            PickItem();
        }
    }



    public void PickItem()
    {
        Debug.Log("Item Activo Agarrado");

        gameObject.transform.SetParent(player.transform);
        IC.ShowActive();
        gameObject.SetActive(false);
    }
}
