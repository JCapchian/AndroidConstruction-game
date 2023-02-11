using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{   
    // Input Systems
    PlayerAction _playerAction;

    // Managers
    InventoryManager inventoryManager;
    GUIManager gUIManager;

    // Movement Input
    [SerializeField]
    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;

    // Action Buttons
    public bool interactButton;
    private bool fireWeaponButton;
    private bool switchGunButton;
    private bool reloadGunButton;
    private bool pauseButton;

    #region Enable & Disable
    private void OnEnable() {
        if(_playerAction == null)
        {
            _playerAction = new PlayerAction();

            Debug.Log(_playerAction);

            // Guarda un Vector2 de los controles de movimiento
            _playerAction.PlayerControls.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            // Ejecucion de controles
            _playerAction.PlayerInteractions.FireWeapon.performed += i => fireWeaponButton = true;
            _playerAction.PlayerInteractions.FireWeapon.canceled += i => fireWeaponButton = false;

            _playerAction.PlayerInteractions.SwitchGun.performed += i => switchGunButton = true;
            _playerAction.PlayerInteractions.SwitchGun.canceled += i => switchGunButton = false;

            _playerAction.PlayerInteractions.Interact.performed += i => interactButton = true;
            _playerAction.PlayerInteractions.Interact.canceled += i => interactButton = false;

            _playerAction.PlayerInteractions.ReloadGun.performed += i => reloadGunButton = true;
            _playerAction.PlayerInteractions.ReloadGun.canceled += i => reloadGunButton = false;

            _playerAction.PlayerInteractions.PauseGame.performed += i => pauseButton = true;
            _playerAction.PlayerInteractions.PauseGame.canceled += i => pauseButton = false;

        }
        _playerAction.Enable();
    }

    private void OnDisable() {
        _playerAction.Disable();
    }
    #endregion

    private void Awake() {
        inventoryManager = GetComponent<InventoryManager>();
        gUIManager = FindObjectOfType<GUIManager>();
    }
    
    public void HandleAllInput()
    {
        HandleMovementInput();
        HandleActionsInput();
    }

    private void HandleMovementInput()
    {
        // Declaro los valores de los inputs
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }

    private void HandleActionsInput()
    {
        // Acciones de los botones
        
        // Boton de disparo
        if(fireWeaponButton)
        {
            Debug.Log("Disparo el arma");

            // Accion
            // Pregunto si tiene un arma
            if(inventoryManager.activeGun)
                inventoryManager.activeGun.IsLoaded();
            else
                Debug.Log("No tiene arma equipada");
        }
        else
        {
            if(inventoryManager.activeGun)
                inventoryManager.activeGun.OffGun();
        }
        

        // Boton de Interaccion
        if(interactButton)
        {
            Debug.Log("Interactuo");

            //Accion
            if(inventoryManager.closeItem)
                inventoryManager.closeItem.Interact(transform.gameObject);
            else
                Debug.Log("No hay nada en el piso");

            interactButton = false;
        }

        // Boton de Cambio de armas
        if(switchGunButton)
        {
            Debug.Log("Cambio de armas");

            //Accion
            if(inventoryManager.gunsEquiped.Count > 1)
                inventoryManager.ChangeGun();
            else
                Debug.Log("Tiene una sola arma");

            switchGunButton = false;
        }

        // Boton de Recarga de armas
        if(reloadGunButton)
        {
            // Accion
            if(inventoryManager.activeGun)
                inventoryManager.activeGun.AmmoCheck();
            else
                Debug.Log("No tiene arma equipada");
            
            reloadGunButton = false;
        }

        // Boton de Pausa
        if(pauseButton)
        {
            //Accion
            if(gUIManager.isPaused)
                gUIManager.TurnResumeOrPause(true);
                //gUIManager.Resume();
            else
                gUIManager.TurnResumeOrPause(false);
                //gUIManager.PauseGame();

            pauseButton = false;
        }
    }
}
