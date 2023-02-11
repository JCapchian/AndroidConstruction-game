using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected EnemyAnimations enemyAnimations;
    [SerializeField]
    protected EnemyAbilities enemyAbilities;

    protected virtual void Awake()
    {
        enemyAnimations = GetComponentInChildren<EnemyAnimations>();
        enemyAbilities = GetComponentInChildren<EnemyAbilities>();
    }

    protected virtual void FixedUpdate()
    {

    }

    private void Update()
    {
        enemyAbilities.HandleAttack();
    }

    private void LateUpdate() {
        enemyAnimations.HandleAllAnimations();
    }
}
