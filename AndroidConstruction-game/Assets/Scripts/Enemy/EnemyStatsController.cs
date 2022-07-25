using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsController : MonoBehaviour
{
    public RunInfo contadorRun;

    public int vidaMaxima = 100;
    [SerializeField]
    private int vida;
    public GameObject[] lootPrefab;
    private int damageDone;

    // Start is called before the first frame update
    void Start()
    {
        vida = vidaMaxima;
        contadorRun = GameObject.Find("GameManager").GetComponent<RunInfo>();
    }

    // On Spawn
    private void Awake() 
    {
        vida = vidaMaxima;
        contadorRun = GameObject.Find("GameManager").GetComponent<RunInfo>();    
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("Toco");
        if(other.gameObject.tag == "Proyectile")
        {
            damageDone = other.gameObject.GetComponent<Bullet>().damageBullet;
            TakeDamage(damageDone);

            //Destroy Proyectile
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        vida -= damage;
        Debug.Log("Le has quitado " + damage + " puntos de vida");
        Debug.Log(vida + " vida restante");
    }

    private void Update() {
        if(vida <= 0)
        {
            Debug.Log("El enemigo a muerto");
            //Spawns droploot
            DropLoot();
            //Saves the death of the enemy in the run
            contadorRun.EnemigoEliminado();

            Destroy(gameObject);
        }
    }

    private void DropLoot()
    {
        var chances = Random.Range(0,lootPrefab.Length);
        Debug.Log(chances);
        switch (chances)
        {
            case 1:
                Instantiate(lootPrefab[0], gameObject.transform.position, gameObject.transform.rotation);
                break;
            case 2:
                Instantiate(lootPrefab[1], gameObject.transform.position, gameObject.transform.rotation);
                break;
            case 3:
                Instantiate(lootPrefab[2], gameObject.transform.position, gameObject.transform.rotation);
                break;
        }
    }
}
