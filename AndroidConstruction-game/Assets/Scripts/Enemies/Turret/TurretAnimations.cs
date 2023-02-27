using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAnimations : EnemyAnimations
{
    [SerializeField]
    TurretAimDirection turretAimDirection;

    [SerializeField]
    SpriteRenderer sprite;


    protected override void Awake()
    {
        base.Awake();

        sprite = GetComponent<SpriteRenderer>();
    }

    public override void HandleAllAnimations()
    {
        base.HandleAllAnimations();
        FlipSprite();
    }

    private void FlipSprite()
    {
        if(turretAimDirection.gameObject.transform.rotation.z > 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;

    }

    public override void DeathAnimation()
    {
        base.DeathAnimation();

        Destroy(enemyStats.gameObject, 0.5f);
    }
}