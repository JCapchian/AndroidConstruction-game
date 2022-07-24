using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActive : MonoBehaviour
{
    public GameObject Player;
    public InventoryContoller IC;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            Player = other.gameObject;
            Debug.Log(Player);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(Input.GetButtonDown("Submit") && other.gameObject.tag == "Player")
        {
            Debug.Log(Player);
            IC = Player.gameObject.GetComponent<InventoryContoller>();
            PickItem();
        }
    }

    public void PickItem()
    {
        Debug.Log("Item Activo Agarrado");

        gameObject.transform.SetParent(Player.transform);
        IC.ShowActive();
        gameObject.SetActive(false);
    }
}
