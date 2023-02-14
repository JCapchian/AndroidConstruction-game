using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    private Collider2D triggerZone;
    public bool active = false;

    [SerializeField]
    List<GameObject> enemiesList = new List<GameObject>();
    [SerializeField]
    GameObject meleeEnemy;
    [SerializeField]
    GameObject turretEnemy;

    [SerializeField]
    Transform lootDrop;
    [SerializeField]
    GameObject finalDrop;
    [SerializeField]
    int wavesAmount;
    public float timeBetweenWaves;
    public float timeBetweenSpawn;

    public Transform[] spawnsPoints;
    public GameObject[] doorsPoints;

    private void Awake()
    {
        triggerZone = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(active == false && other.GetComponent<PlayerManager>())
        {
            active = true;
            Debug.Log("Entro");
            ActivateSpawner();
            CloseOpenDoors(true);
            Destroy(triggerZone);
        }
    }

    private void ActivateSpawner()
    {
        // Spawn waves
        if(wavesAmount > 0)
        {
            StartCoroutine(StartWave());
            timeBetweenWaves = timeBetweenSpawn;
            // Spawn Doors
            CloseOpenDoors(true);
        }

        if(timeBetweenWaves > 0)
        {
            timeBetweenWaves -= Time.deltaTime;
            Debug.Log("Cargando...");
        }
        else
        {
            StartWave();
            timeBetweenWaves = timeBetweenSpawn;
        }
    }



    private void SpawnLoot()
    {
        Instantiate(finalDrop, lootDrop.transform.position, lootDrop.transform.rotation);
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(0.4f);
        
        for (var i = 0; i < spawnsPoints.Length; i++)
        {
            if(wavesAmount > 1)
            {
                var newEnemies = Instantiate(turretEnemy, spawnsPoints[i].transform.position, spawnsPoints[i].transform.rotation);
                enemiesList.Add(newEnemies);
                Debug.Log("Ultimo Enemigo");
                spawnsPoints[i] = null;
                CloseOpenDoors(false);
                SpawnLoot();
                this.gameObject.GetComponent<Spawner>().enabled = false;
            }
            else
            {
                var newEnemies = Instantiate(meleeEnemy, spawnsPoints[i].transform.position, spawnsPoints[i].transform.rotation);
                enemiesList.Add(newEnemies);
                Debug.Log("Enemigo numero: " + wavesAmount);
            }
        }
        wavesAmount -= 1;
    }

    private void CloseOpenDoors(bool state)
    {
        for (var i = 0; i < doorsPoints.Length; i++)
        {
            doorsPoints[i].SetActive(state);
        }
    }
}
