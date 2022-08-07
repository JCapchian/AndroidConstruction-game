using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{   
    [Header ("Propiedades Jugador")]
    [SerializeField]
    private PlayerAction playerAction;
    [SerializeField]
    private Rigidbody2D rb;
    //public Animator animator;
    //public HealthBar healthBar;
    //public TMP_Text uiHealthCurrent;
    //public TMP_Text uiHealthMax;
    
    [Header ("Estadistacas Jugador")]
    public int maxHealth;
    private int health;
    public int energy;
    public bool death;
    [SerializeField]
    private float movementSpeed;
    
    // Awake is called when the gameobject appear
    private void Awake() 
    {
        // Define the Input
        playerAction = new PlayerAction();

        rb = GetComponent<Rigidbody2D>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        PollInput();
    }

    private void OnEnable() {
        playerAction.Enable();
    }
    private void OnDisable() {
        playerAction.Disable();
    }


    #region InputSystem
    
    private void PollInput(){
        var movementInputVector = playerAction.PlayerControls.Movement.ReadValue<Vector2>();
        Move(movementInputVector);
    }

    #endregion

    #region Movement

    private void Move (Vector2 inputVector){
        //
        var movementOffset = inputVector * movementSpeed * Time.fixedDeltaTime;
        var newPosition = rb.position + movementOffset;
        rb.MovePosition(newPosition);
    }

    #endregion

    #region Aim

    #endregion

    #region StatsPlayer

    #endregion

    #region Invetory

    #endregion

}
