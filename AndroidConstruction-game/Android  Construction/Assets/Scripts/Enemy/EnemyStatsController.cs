using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsController : MonoBehaviour
{
    public int vidaMaxima = 100;
    private int vida;
    public GameObject dropPrefab;
    private int damageDone;

    // Start is called before the first frame update
    void Start()
    {
        vida = vidaMaxima;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Proyectile")
        {
            damageDone = other.gameObject.GetComponent<Bullet>().damageBullet;
            TakeDamage(damageDone);

            Destroy(other.gameObject);
        }
    }

    private void TakeDamage(int damage)
    {
        vida -= damage;
        Debug.Log("Le has quitado " + damage + " puntos de vida");
        Debug.Log(vida + " vida restante");
    }

    private void Update() {
        if(vida <= 0)
        {
            Destroy(gameObject);
            Debug.Log("El enemigo a muerto");
            Instantiate(dropPrefab, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}
