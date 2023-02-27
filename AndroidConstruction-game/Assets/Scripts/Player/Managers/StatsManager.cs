using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField]
    public bool MurioPrimerNivel;
    Scene actualScene;

    // Managers
    [SerializeField]
    PlayerManager playerManager;
    GUIManager gUIManager;
    MovementeManager movementeManager;
    InventoryManager inventoryManager;

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
        actualScene = SceneManager.GetActiveScene();

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

        MurioPrimerNivel = false;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Daño");

        // Resto el daño a la vida
        health -= damage;

        // Atualizo la interfaz
        gUIManager.SetHealth(health);

        // Efecto de daño
        StartCoroutine(DamageVisualEffect());

        if(health <= 0)
        {
            health = 0;
            gUIManager.SetHealth(health);

            if(actualScene.buildIndex == 2)
                MurioPrimerNivel = true;

            StartCoroutine(DeathOfThePlayer());

        }
    }

    public void HealPlayer(int healthOnGround)
    {
        health += healthOnGround;

        if(health > maxHealth)
            health = maxHealth;
            
        gUIManager.SetHealth(health);
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
        hitCollider.enabled = false;

        movementeManager.TurnMovement(false);

        // Ejecuto la animacion de muerte
        animationManager.DeathAnimation();

        yield return new WaitForSeconds(1.5f);

        gUIManager.SHowDeathScreen();

    }
    
    public void ResetStats()
    {
        health = maxHealth;
        hitCollider.enabled = true;
        
        gUIManager.SetHealth(health);

        playerManager.TurnStateManagersPlayer(true);
        
    }
}
