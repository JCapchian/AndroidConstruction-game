using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAbilities : EnemyAbilities
{
    [SerializeField]
    TurretAimDirection turretAimDirection;
    [SerializeField]
    TurretStats turretStats;

    [SerializeField]
    Transform cannon;
    [SerializeField]
    GameObject prefabProyectile;

    private void Awake()
    {
        turretAimDirection = GetComponent<TurretAimDirection>();
        turretStats = GetComponentInParent<TurretStats>();

        cannon = transform.GetChild(0).transform;
    }

    protected override IEnumerator AttackSequence()
    {
        canAttack = false;
        //player.TakeDamage(damage);

        enemyAnimations.AttackAnimation();
        var EnemyBullet = Instantiate(prefabProyectile, cannon.position, cannon.rotation);
        EnemyBullet.GetComponent<EnemyBullet>().BulletDamage = damage;
        Rigidbody2D rb = EnemyBullet.GetComponent<Rigidbody2D>();

        rb.AddForce(cannon.up * turretStats.GetBulletSpeed(), ForceMode2D.Impulse);

        Debug.Log("Ataco:" + damage);

        yield return new WaitForSeconds(coolDown);

        canAttack = true;
        Debug.Log("Puede volver a atacar");
    }
}
