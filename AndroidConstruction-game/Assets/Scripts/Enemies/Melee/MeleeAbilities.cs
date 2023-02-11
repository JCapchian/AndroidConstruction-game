using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAbilities : EnemyAbilities
{
    protected override IEnumerator AttackSequence()
    {
        canAttack = false;
        player.TakeDamage(damage);

        enemyAnimations.AttackAnimation();
        Debug.Log("Ataco:" + damage);

        yield return new WaitForSeconds(coolDown);

        canAttack = true;
        Debug.Log("Puede volver a atacar");
    }
}
