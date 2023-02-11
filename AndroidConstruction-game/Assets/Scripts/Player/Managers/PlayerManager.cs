using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Managers
    [SerializeField]
    InputManager inputManager;
    [SerializeField]
    MovementeManager movementeManager;
    [SerializeField]
    public GUIManager gUIManager;
    [SerializeField]
    AnimationManager animationManager;
    StatsManager statsManager;

    // Controllers
    [SerializeField]
    AimController aimController;

    private void Awake() {
        // Declaro los managers
        inputManager = GetComponent<InputManager>();
        movementeManager = GetComponent<MovementeManager>();
        gUIManager = FindObjectOfType<GUIManager>();
        animationManager = GetComponent<AnimationManager>();
        statsManager = GetComponent<StatsManager>();

        // Declaro los controllers
        aimController = GetComponentInChildren<AimController>();
    }

    private void Update() {
        inputManager.HandleAllInput();
    }

    private void FixedUpdate() {
        movementeManager.HandleMovement();
    }

    private void LateUpdate() {
        animationManager.HandleAllAnimations();
        gUIManager.HandleEffects();
    }

    public void TurnStateManagersPlayer(bool state)
    {
        inputManager.enabled = state;
        movementeManager.enabled = state;
        animationManager.enabled = state;

        aimController.enabled = state;
    }
}
