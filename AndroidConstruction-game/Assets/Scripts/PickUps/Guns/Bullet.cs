using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    TrailRenderer trail;
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    int damageBullet;
    [SerializeField]
    float lifeTime;
    [SerializeField]
    LayerMask layerMask;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D (Collision2D other)
    {
        Debug.Log(other.gameObject.layer);
        if(other.gameObject.layer == 7 || other.gameObject.layer == 10)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
    //Destroy(gameObject);
    }

    public void SetStatsEffects(int weaponDamage, float gunLifeTime, Color trailColor, Color bulletColor)
    {
        damageBullet = weaponDamage;
        lifeTime = gunLifeTime;

        trail.startColor = trailColor;
        trail.endColor = trailColor;

        sprite.color = bulletColor;

        Destroy(gameObject, lifeTime);
    }

    public int GetDamage()
    {
        return damageBullet;
    }
}
