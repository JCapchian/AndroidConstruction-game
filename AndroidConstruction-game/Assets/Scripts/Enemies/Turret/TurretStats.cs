using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStats : EnemyStats
{
    [SerializeField]
    private float bulletSpeed;

    public float GetBulletSpeed()
    {
        Debug.Log(bulletSpeed);
        return bulletSpeed;
    }
}
