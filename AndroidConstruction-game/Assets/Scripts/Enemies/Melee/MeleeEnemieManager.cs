using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemieManager : Enemy
{
    [SerializeField]
    MeleeMovement meleeMovement;

    protected override void Awake()
    {
        base.Awake();

        meleeMovement = GetComponent<MeleeMovement>();
    }

    protected override void FixedUpdate()
    {
        meleeMovement.HandleMovement();
    }
}
