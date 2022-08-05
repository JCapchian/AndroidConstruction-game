using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
    public int damageBullet;
    public GameObject destroyParticleEffect;

    public void OnCollisionEnter2D (Collision2D other)
    {
        Destroy(gameObject);
    }
    
    private void Awake() 
    {
        Destroy(gameObject, 2f);
    }

    public void GetDamage(int weaponDamage)
    {
        damageBullet = weaponDamage;
    }
}
