using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damageBullet;
    public GameObject destroyParticleEffect;
    
    public void OnCollisionEnter2D (Collision2D other)
    {
        Instantiate(destroyParticleEffect, this.gameObject.transform.position , this.gameObject.transform.rotation);
        Destroy(gameObject);
    }

    private void Awake() 
    {
        Destroy(gameObject, 3f);
    }

    public void GetDamage(int weaponDamage)
    {
        damageBullet = weaponDamage;
    }

}
