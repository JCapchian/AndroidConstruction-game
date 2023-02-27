using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerPackage;
    [SerializeField]
    GameObject playerPrefab;
    [SerializeField]
    InventoryManager inventoryManager;
    [SerializeField]
    StatsManager statsManager;

    [SerializeField]
    Transform entranceLevel;

    [SerializeField]
    bool PrimerNivel;

    private void Awake()
    {
        //var actualScene = SceneManager.GetActiveScene();
        //statsManager = playerPackage.GetComponentInChildren<StatsManager>();

        // if(!statsManager.MurioPrimerNivel)
        // {
        //     Debug.Log("Crear al jugador");
        //     //SpawnPlayer();
        // }



        playerPrefab = FindObjectOfType<PlayerManager>().gameObject;

        inventoryManager = playerPrefab.GetComponent<InventoryManager>();

        TeleportPlayer();
    }



    void TeleportPlayer()
    {
        playerPrefab.transform.position = entranceLevel.position;
    }

    void SpawnPlayer()
    {
        Instantiate(playerPackage, Vector3.zero, this.transform.rotation);
        Debug.Log("Paquete spawneado");
    }
}
