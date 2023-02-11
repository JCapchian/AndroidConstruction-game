using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    EnemyAnimations enemyAnimations;
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    Collider2D c2D;
    [SerializeField]
    protected GameObject dropPrefab;
    [SerializeField]
    int health;

    private void Awake()
    {
        enemyAnimations = GetComponentInChildren<EnemyAnimations>();

        sprite = GetComponentInChildren<SpriteRenderer>();
        c2D = GetComponent<Collider2D>();
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Daño realizado " + damage);

        StartCoroutine(DamageVisualEffect());

        if(health < 0)
        {
            enemyAnimations.DeathAnimation();
            Destroy(c2D);
            Instantiate(dropPrefab, this.transform.position, this.transform.rotation);
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
