using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilities : MonoBehaviour
{
    [SerializeField]
    protected EnemyAnimations enemyAnimations;
    [SerializeField]
    protected StatsManager player;

    [SerializeField]
    protected bool playerInRange;
    [SerializeField]
    protected bool canAttack;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected float coolDown;

    public void HandleAttack()
    {
        BasicAttack();
    }

    protected virtual void BasicAttack()
    {
        if(playerInRange && canAttack)
        {
            Debug.Log("Intenta atacar");
            StartCoroutine(AttackSequence());
        }
    }

    protected virtual IEnumerator AttackSequence()
    {
        yield return new WaitForSeconds(0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<StatsManager>())
        {
            playerInRange = true;
            player = other.GetComponent<StatsManager>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<StatsManager>())
        {
            playerInRange = false;
            player = null;
        }
    }
}
