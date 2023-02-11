using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : Enemy
{
    [SerializeField]
    TurretAimDirection turretAim;

    protected override void Awake()
    {
        base.Awake();

        turretAim = GetComponentInChildren<TurretAimDirection>();
    }

    protected override void FixedUpdate()
    {
        turretAim.HandleDirectionCannon();
    }
}
