using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    public List<GameObject> enemiesList = new List<GameObject>();
    public Transform[] spawnsPoints;
    public GameObject[] doorsPoints;
    public GameObject loot;
    public Transform lootDrop;
    public GameObject zoneEnemy;
    public GameObject turretEnemy;
    public BoxCollider2D triggerZone;
    public Vector2 maxCollider;
    public bool active = false;
    public bool first = false;
    public int quantitySpawns;
    public float timeSpawn;
    public float maxTimeSpawn;

    
    // Start is called before the first frame update
    void Start()
    {
        //timeSpawn = maxTimeSpawn;
    }

    private void OnTriggerStay2D(Collider2D other) 
    {

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(active == false && other.gameObject.tag == "Player")
        {
            active = true;
            triggerZone.size = new Vector2(20.25f,20.73f);
            Debug.Log("Entro");
        } 

        if(other.gameObject.tag == "Enemy")
        {
            enemiesList.Add(other.gameObject);
            Debug.Log("NuevoEnemigo");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy")
        {
            enemiesList.Remove(other.gameObject);
            Debug.Log("FueraEnemigo");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(active == true)
            ActivateSpawner();
        
        if(quantitySpawns == 0 && enemiesList.Count == 0)
            CloseOpenDoors(false);
    }

    private void ActivateSpawner()
    {
        // Active the spawner the inmediatly the first time
        if(first == false)
        {
            timeSpawn = maxTimeSpawn;
            // Spawn Doors
            CloseOpenDoors(true);

            first = true;
        }

        if(timeSpawn > 0)
        {
            timeSpawn -= Time.deltaTime;
            Debug.Log("Cargando...");   
        }
        else
        {
            SpawnEnemies();
            
            timeSpawn = maxTimeSpawn;
        }
    }

    private void SpawnLoot()
    {
        Instantiate(loot, lootDrop.transform.position, lootDrop.transform.rotation);
    }

    private void SpawnEnemies()
    {
        for (var i = 0; i < spawnsPoints.Length; i++)
        {
            if(quantitySpawns == 1)
            {
                Instantiate(turretEnemy, spawnsPoints[i].transform.position, spawnsPoints[i].transform.rotation);
                Debug.Log("Ultimo Enemigo");
                spawnsPoints[i] = null;
                CloseOpenDoors(false);
                SpawnLoot();
                this.gameObject.GetComponent<Spawner>().enabled = false;
            }
            else
            {
                Instantiate(zoneEnemy, spawnsPoints[i].transform.position, spawnsPoints[i].transform.rotation);
                Debug.Log("Enemigo numero: " + quantitySpawns);
            }
        }
        quantitySpawns -= 1;
    }

    private void CloseOpenDoors(bool onOff)
    {
        for (var i = 0; i < doorsPoints.Length; i++)
        {
            if(onOff == true)
                doorsPoints[i].active = true;
            else
                doorsPoints[i].active = false;
        }
    }
}
