using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // Managers
    MovementeManager movementeManager;

    // Components
    [SerializeField]
    private Animator playerAnimator;

    private void Awake() {
        playerAnimator = GetComponent<Animator>();
        movementeManager = GetComponent<MovementeManager>();
    }

    public void HandleAllAnimations()
    {
        MoveAnimation();
    }

    private void MoveAnimation()
    {
        playerAnimator.SetBool("isMoving", movementeManager.isMoving);
    }

    public void DeathAnimation()
    {
        playerAnimator.SetBool("Death", true);
    }

    public void ResetDeath()
    {
        playerAnimator.SetBool("Death", false);
    }
}
