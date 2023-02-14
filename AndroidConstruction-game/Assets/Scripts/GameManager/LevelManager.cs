using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerRef;
    InventoryManager inventoryManager;

    [SerializeField]
    Transform entranceLevel;
    
    private void Awake()
    {
        playerRef = FindObjectOfType<PlayerManager>().gameObject;

        inventoryManager = playerRef.GetComponent<InventoryManager>();

        SpawnPlayerScene();
    }

    private void SpawnPlayerScene()
    {
        playerRef.transform.position = entranceLevel.position;
    }
}
