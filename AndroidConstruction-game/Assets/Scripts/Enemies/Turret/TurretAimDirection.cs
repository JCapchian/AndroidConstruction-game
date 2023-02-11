using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAimDirection : MonoBehaviour
{
    [SerializeField]
    PlayerManager player;
    [SerializeField]
    Transform cannon;

    private void Awake()
    {
        cannon = transform.GetChild(0).transform;
    }

    public void HandleDirectionCannon()
    {
        if(player)
        {
            transform.up = player.transform.position - transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<PlayerManager>())
        {
            player = other.GetComponent<PlayerManager>();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.GetComponent<PlayerManager>())
        {
            player = null;
        }
    }
}
