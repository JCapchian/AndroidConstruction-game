using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // Managers
    [SerializeField]
    PlayerManager playerManager;
    GUIManager gUIManager;
    MovementeManager movementeManager;

    SpriteRenderer sprite;
    AnimationManager animationManager;

    [Header ("Componentes del jugador")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip damageClip;
    [SerializeField]
    Collider2D hitCollider;

    [Header("Estadisticas del jugador")]
    [SerializeField]
    public int maxHealth;
    [SerializeField]
    public int health;

    private void Awake() {
        // Declaro los managers
        playerManager = GetComponent<PlayerManager>();
        animationManager = GetComponent<AnimationManager>();
        movementeManager = GetComponent<MovementeManager>();

        // Declaro los componentes
        gUIManager = FindObjectOfType<GUIManager>();
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        hitCollider = GetComponent<Collider2D>();

        // Declaro las estadicas
        health = maxHealth;

        //gUIManager.SetMaxHealth(health);
    }

    public void TakeDamage(int damage)
    {
        // Resto el daño a la vida
        health -= damage;

        // Atualizo la interfaz
        gUIManager.SetHealth(health);

        // Efecto de daño
        StartCoroutine(DamageVisualEffect());

        if(health <= 0)
        {
            StartCoroutine(DeathOfThePlayer());
            hitCollider.enabled = false;
        }
    }

    private IEnumerator DamageVisualEffect()
    {
        audioSource.PlayOneShot(damageClip);

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

    private void OnTriggerEnter2D(Collider2D other) {
        // Pregunto si es una bala
        if(other.GetComponent<EnemyBullet>())
        {
            var bullet = other.GetComponent<EnemyBullet>();
            TakeDamage(bullet.BulletDamage);
        }
    }

    private IEnumerator DeathOfThePlayer()
    {
        // Apago los managers del jugador
        playerManager.TurnStateManagersPlayer(false);

        movementeManager.StopMovement();

        // Ejecuto la animacion de muerte
        animationManager.DeathAnimation();

        yield return new WaitForSeconds(1.5f);

        gUIManager.SHowDeathScreen();

    }
}
