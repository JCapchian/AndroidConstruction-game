using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimations : EnemyAnimations
{
    MeleeMovement meleeMovement;

    //Animator meleeAnimator;


    protected override void Awake()
    {
        base.Awake();

        meleeMovement = GetComponentInParent<MeleeMovement>();
    }

    public override void HandleAllAnimations()
    {
        base.HandleAllAnimations();

        MoveAnimation();
    }

    protected override void IdleAnimation()
    {
        if(meleeMovement.isIdle)
            enemyAnimator.SetBool("isIdle" ,true);
        else
            enemyAnimator.SetBool("isIdle" ,false);
    }

    void MoveAnimation()
    {
        if(meleeMovement.isMoving)
            enemyAnimator.SetBool("isMoving" ,true);
        else
            enemyAnimator.SetBool("isMoving" ,false);
    }

    public override void DeathAnimation()
    {
        base.DeathAnimation();

        meleeMovement.StopMovement();

        Destroy(enemyStats.gameObject, 0.5f);
    }
}
