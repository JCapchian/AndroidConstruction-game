using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    int bulletDamage;
    [SerializeField]
    float lifeTime = 3f;

    public int BulletDamage
    {
        set
        {
            bulletDamage = value;
        }
        get
        {
            return bulletDamage;
        }
    }

    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.GetComponent<StatsManager>())
        {
            var player = other.gameObject.GetComponent<StatsManager>();
            player.TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }
}
