using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    EnemyAnimations enemyAnimations;
    EnemyAbilities enemyAbilities;
    [SerializeField]
    Spawner spawnerRef;
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    Collider2D c2D;
    [SerializeField]
    protected GameObject[] dropsPrefabs;
    [SerializeField]
    int health;

    private void Awake()
    {
        enemyAnimations = GetComponentInChildren<EnemyAnimations>();
        enemyAbilities = GetComponentInParent<EnemyAbilities>();

        sprite = GetComponentInChildren<SpriteRenderer>();
        c2D = GetComponent<Collider2D>();
    }

    public void GetSpawner(Spawner spawner)
    {
        spawnerRef = spawner;
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Daño realizado " + damage);

        StartCoroutine(DamageVisualEffect());

        if(health < 0)
        {
            Debug.Log("Muerto");

            Destroy(c2D);
            enemyAnimations.DeathAnimation();
            if(spawnerRef)
            {
                Debug.Log("Muerto Spawner");
                spawnerRef.EnemyDeath();
            }

            Instantiate(dropsPrefabs[Random.Range(0, dropsPrefabs.Length)], this.transform.position, this.transform.rotation);

        }

    }

    private IEnumerator DamageVisualEffect()
    {
        sprite.color = Color.white;

        yield return new WaitForSeconds(0.2f);

        sprite.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        sprite.color = Color.white;

        yield return new WaitForSeconds(0.2f);

        sprite.color = Color.red;

        yield return new WaitForSeconds(0.2f);

        sprite.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Bullet>())
            TakeDamage(other.gameObject.GetComponent<Bullet>().GetDamage());
    }
}
