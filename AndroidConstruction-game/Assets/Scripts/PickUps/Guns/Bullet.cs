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
    LayerMask layerMask;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        sprite = GetComponent<SpriteRenderer>();

        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D (Collision2D other)
    {
        Destroy(gameObject);
    }

    public void SetStatsEffects(int weaponDamage, Color trailColor, Color bulletColor)
    {
        damageBullet = weaponDamage;

        trail.startColor = trailColor;
        trail.endColor = trailColor;

        sprite.color = bulletColor;
    }

    public int GetDamage()
    {
        return damageBullet;
    }
}
