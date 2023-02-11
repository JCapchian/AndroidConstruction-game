using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField]
    EnemyStats enemyStats;
    EnemyAbilities enemyAbilities;

    protected Animator enemyAnimator;

    protected virtual void Awake() {
        enemyStats = GetComponentInParent<EnemyStats>();
        enemyAbilities = GetComponentInParent<MeleeAbilities>();

        enemyAnimator = GetComponent<Animator>();
    }

    public virtual void HandleAllAnimations()
    {
        IdleAnimation();
    }

    protected virtual void IdleAnimation()
    {

    }

    public virtual void AttackAnimation()
    {
        enemyAnimator.SetTrigger("isAttacking");
    }

    public void DeathAnimation()
    {
        enemyAnimator.SetBool("isDeath" ,true);
        Destroy(enemyStats.gameObject, 0.5f);
    }
}
