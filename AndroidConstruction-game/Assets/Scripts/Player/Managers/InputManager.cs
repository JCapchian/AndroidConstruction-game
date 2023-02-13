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
    private bool reloadGunButton;
    private bool pauseButton;

    private bool gun1Button;
    private bool gun2Button;
    private bool gun3Button;

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

            _playerAction.PlayerInteractions.Interact.performed += i => interactButton = true;
            _playerAction.PlayerInteractions.Interact.canceled += i => interactButton = false;

            _playerAction.PlayerInteractions.ReloadGun.performed += i => reloadGunButton = true;
            _playerAction.PlayerInteractions.ReloadGun.canceled += i => reloadGunButton = false;

            _playerAction.PlayerInteractions.PauseGame.performed += i => pauseButton = true;
            _playerAction.PlayerInteractions.PauseGame.canceled += i => pauseButton = false;

            _playerAction.PlayerInteractions.Gun1.performed += i => gun1Button = true;
            _playerAction.PlayerInteractions.Gun1.canceled += i => gun1Button = false;

            _playerAction.PlayerInteractions.Gun2.performed += i => gun2Button = true;
            _playerAction.PlayerInteractions.Gun2.canceled += i => gun2Button = false;

            _playerAction.PlayerInteractions.Gun3.performed += i => gun3Button = true;
            _playerAction.PlayerInteractions.Gun3.canceled += i => gun3Button = false;
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

        // Botones de Cambio de armas
        // BotonArma 1
        if(gun1Button)
        {
            if(inventoryManager.gunsEquiped[0])
                inventoryManager.ChangeGun(0);
            else
                Debug.Log("No Arma 1");
        }

        // BotonArma 2
        if(gun2Button)
        {
            if(inventoryManager.gunsEquiped[1])
                inventoryManager.ChangeGun(1);
            else
                Debug.Log("No Arma 2");
        }

        // BotonArma 3
        if(gun3Button)
        {
            if(inventoryManager.gunsEquiped[2])
                inventoryManager.ChangeGun(2);
            else
                Debug.Log("No Arma 3");
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
