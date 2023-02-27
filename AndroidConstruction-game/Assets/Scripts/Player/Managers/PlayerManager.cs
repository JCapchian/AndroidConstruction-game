using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameObject playerPackage;

    // Managers
    [SerializeField]
    InputManager inputManager;
    [SerializeField]
    MovementeManager movementeManager;
    [SerializeField]
    InventoryManager inventoryManager;
    [SerializeField]
    public GUIManager gUIManager;
    [SerializeField]
    AnimationManager animationManager;
    [SerializeField]
    StatsManager statsManager;
    [SerializeField]
    CameraManager cameraManager;

    // Controllers
    [SerializeField]
    AimController aimController;

    private void Awake() {
        playerPackage = transform.parent.gameObject;

        // Declaro los managers
        inputManager = GetComponent<InputManager>();
        movementeManager = GetComponent<MovementeManager>();
        animationManager = GetComponent<AnimationManager>();
        inventoryManager = GetComponent<InventoryManager>();
        statsManager = GetComponent<StatsManager>();

        gUIManager = FindObjectOfType<GUIManager>();
        cameraManager = FindObjectOfType<CameraManager>();

        // Declaro los controllers
        aimController = GetComponentInChildren<AimController>();

        this.transform.parent = null;

        if(SceneManager.GetActiveScene().buildIndex != 2)
            DontDestroyOnLoad(this.gameObject);
    }
    #region Private Functions

    private void Update() {
        inputManager.HandleAllInput();
    }

    private void FixedUpdate() {
        movementeManager.HandleMovement();
        aimController.HandleAllFunctions();
        cameraManager.HandleCamera();
    }

    private void LateUpdate() {
        animationManager.HandleAllAnimations();
        gUIManager.HandleEffects();
    }

    #endregion

    #region Public Functions

    public void TurnStateManagersPlayer(bool state)
    {
        inputManager.enabled = state;
        movementeManager.enabled = state;
        animationManager.enabled = state;

        aimController.enabled = state;
    }

    public void ResetPlayer()
    {
        statsManager.ResetStats();
        inventoryManager.LoadInventory();
        movementeManager.TurnMovement(true);
        animationManager.ResetDeath();
    }

    public void DestroyPlayer()
    {
        Destroy(this.gameObject);
        Destroy(gUIManager.gameObject);
        Destroy(cameraManager.gameObject);
    }

    public void TurnDontDestroyOnLoad(bool state)
    {
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(gUIManager.gameObject);
        DontDestroyOnLoad(cameraManager.gameObject);
    }

    #endregion
}
