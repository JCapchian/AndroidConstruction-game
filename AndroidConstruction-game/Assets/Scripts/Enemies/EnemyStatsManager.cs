using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : MonoBehaviour
{
    [SerializeField]
    EnemyAnimations enemyAnimations;
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    int health;

    private void Awake()
    {
        enemyAnimations = GetComponentInChildren<EnemyAnimations>();

        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Daño realizado " + damage);

        StartCoroutine(DamageVisualEffect());

        if(health < 0)
            enemyAnimations.DeathAnimation();
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
