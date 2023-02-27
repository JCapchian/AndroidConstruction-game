using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    SpawnerManagers spawnerManagers;
    [Header ("Componentes")]
    private Collider2D triggerZone;

    [Header ("Enemigos")]
    [SerializeField]
    EnemyStats enemyStatsRef;
    [SerializeField]
    GameObject meleeEnemyObject;
    [SerializeField]
    GameObject turretEnemyObject;

    [Header ("Propiedades")]
    [SerializeField]
    bool canSpawn;
    [SerializeField]
    public bool isEnemyDeath;
    [SerializeField]
    public bool isActive;
    public bool lastWave;

    public Transform spawnPoint;

    private void Awake()
    {
        triggerZone = GetComponent<Collider2D>();
        spawnerManagers = GetComponentInParent<SpawnerManagers>();
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(isActive == false && other.GetComponent<PlayerManager>())
    //     {
    //         isActive = true;
    //         Debug.Log("Entro");
    //         ActivateSpawner();
    //         //CloseOpenDoors(true);
    //         Destroy(triggerZone);
    //     }
    // }

    private void ActivateSpawner()
    {
        // Spawn waves
        // if(!lastWave && canSpawn)
        // {
        //     StartCoroutine(StartWave());
        //     timeBetweenWaves = timeBetweenSpawn;
        //     // Spawn Doors
        //     CloseOpenDoors(true);
        // }
        // else
        // {

        // }
        /*
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
        */
    }

    public void EnemyDeath()
    {
        isEnemyDeath = true;
        isActive = false;

        spawnerManagers.CheckSpawners();
    }

    public IEnumerator StartWave(float time)
    {
        // Activo el tiempo de espera
        yield return new WaitForSeconds(time);

        //Guardo una referencia del enemigo;
        var newEnemies = meleeEnemyObject;

        if(spawnerManagers.lastWave)
            // Spawneo una torreta
            newEnemies = Instantiate(turretEnemyObject, transform.position, transform.rotation);
        else
            // Spawneo un melee
            newEnemies = Instantiate(meleeEnemyObject, transform.position, transform.rotation);

        // Guardo la referencia del enemigo
        enemyStatsRef = newEnemies.GetComponent<EnemyStats>();
        // Le guardo una referencia del spawner
        enemyStatsRef.GetSpawner(this);
    }

}
