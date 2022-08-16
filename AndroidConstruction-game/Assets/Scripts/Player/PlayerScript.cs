using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerScript : MonoBehaviour
{   
    [Header ("Propiedades Jugador")]
    private PlayerAction playerAction;
    private Rigidbody2D rb;
    private Camera cameraPlayer;
    private Transform targetCamera;
    [SerializeField]
    private GameObject gunContainer;

    //public Animator animator;
    //public HealthBar healthBar;
    //public TMP_Text uiHealthCurrent;
    //public TMP_Text uiHealthMax;
    
    [Header ("Estadistacas Jugador")]
    public int maxHealth;
    private int health;
    public bool death;
    public int energy;
    public float movementSpeed;

    #region Ammo & Guns

    [Header ("Inventario Jugador")]
    public int greyAmmo;
    public int greenAmmo;
    public int purpleAmmo;
    private int maxGreyAmmo;
    private int maxGreenAmmo;
    private int maxPurpleAmmo;

    public bool gunActive;
    public PickUp_Gun pickUp_Gun;
    [SerializeField]
    private GameObject primaryGun;
    [SerializeField]
    private GameObject secondaryGun;
    
    #endregion

    #region Objects
    [Header ("Objetos")]
    public PickUp_ItemActive pickUp_ItemActive;
    public GameObject activeItem;
    public float haveActiveItem;
    public GameObject[] pasiveItem;

    #endregion 

    #region UIElements

    [Header ("Elementos UI")]
    public HealthBar hB;
    public TMP_Text uiCurrent;
    public TMP_Text uiMax;
    public Pausa UI;
    public float transitionTime = 1f;
    
    #endregion

    #region Animations
    
    [Header ("Animaciones")]
    public Animator transition;
    public Animator MyAnimator;
    public Vector2 movementInput;
    
    #endregion
    
    // Awake is called when the gameobject appear
    private void Awake() 
    {
        // Define the Input
        playerAction = new PlayerAction();
        Mouse mouse = InputSystem.GetDevice<Mouse>();

        rb = GetComponent<Rigidbody2D>();

        // Define GunSlots
        gunContainer = this.transform.GetChild(0).GetChild(0).gameObject;

        //UI
        health = maxHealth;
        hB.SetMaxHealth(maxHealth);
        hB.SetHealth(health);

        uiCurrent.text = health.ToString();
        uiMax.text = maxHealth.ToString();   

        death = false;

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

        // Gun
        if(playerAction.PlayerControls.SwitchGun.IsPressed())
            Invoke("SwitchGun",10f);
            //SwitchGun();
        
        //Death
        if(health == 0)
        {
            //movement.enabled = false;
            //inventory.DisableWeapon();

            MyAnimator.SetBool("Death", true); 
            StartCoroutine(ReinicioEscena());
        }
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
        movementInput = movementInputVector;
        Debug.Log(movementInput);
        Move(movementInputVector);
    }

    #endregion

    #region Movement

    public void Move (Vector2 inputVector){
        var movementOffset = inputVector * movementSpeed * Time.fixedDeltaTime;
        var newPosition = rb.position + movementOffset;
        rb.MovePosition(newPosition);
        //MyAnimator.SetFloat("Movimiento", movementOffset);
    }

    

    #endregion

    #region StatsPlayer

    #region UI

    public void SumarVida(int healthGain)
    {
        Debug.Log(health);
        health += healthGain;
        hB.SetHealth(health);
        uiCurrent.text = health.ToString();
    }

    public void PerderVida(int healthLess)
    {

        if(health > 0)
        {
            health = health - healthLess;
            hB.SetHealth(health);
            uiCurrent.text = health.ToString();
            Debug.Log(health);
        }
        
    }

    #endregion
    
    #region Scenes

    IEnumerator ReinicioEscena()
    {
        //tartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        yield return new WaitForSeconds(1.5f);

        UI.SHowDeathScreen();
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(2);
        
        //Play Animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(levelIndex);
    }

    #endregion

    #endregion

    #region Invetory

    #region Weapons
    
    public void SwitchGun(){
        Debug.Log("Cambio de arma");

        if(gunActive){
            // Log the weapon change
            Debug.Log("Cambio a primaria");

            // Switch between the guns
            primaryGun.active = true;
            secondaryGun.active = false;

            // Assing the "PickUp_Gun" scrip
            pickUp_Gun = primaryGun.GetComponent<PickUp_Gun>();

            gunActive = false;
        }
        else{
            // Log the weapon change
            Debug.Log("Cambio a secundaria");

            // Switch between the guns
            primaryGun.active = true;
            secondaryGun.active = false;

            // Assing the "PickUp_Gun" scrip
            pickUp_Gun = secondaryGun.GetComponent<PickUp_Gun>();

            gunActive = true;
        }
    }

    public void AssignGuns(GameObject newGun){
        // When the player has no guns
        if(primaryGun == null){
            // Assing the new gun
            primaryGun = newGun;
            primaryGun.transform.parent = gunContainer.transform;
            // Assing the new gun scrip
            pickUp_Gun = primaryGun.GetComponent<PickUp_Gun>();
        }
        // When the player only have one
        else if(secondaryGun == null)
        {
            // Assing the new gun
            secondaryGun = newGun;
            secondaryGun.transform.parent = gunContainer.transform;
            // Assing the new gun scrip
            pickUp_Gun = secondaryGun.GetComponent<PickUp_Gun>();

            // Turn off the primaryGun
            primaryGun.active = false;
        }
        //When the player has two guns
        else
        {
            // Assing the the old script to dorp the gun
            pickUp_Gun = primaryGun.GetComponent<PickUp_Gun>();

            primaryGun.transform.rotation = newGun.transform.rotation;
            primaryGun.transform.position = newGun.transform.position;            
            primaryGun.transform.parent = null;

            pickUp_Gun.Drop();

            // Assing the new gun
            primaryGun = newGun;
            primaryGun.transform.parent = gunContainer.transform;
            pickUp_Gun = primaryGun.GetComponent<PickUp_Gun>();

            // Ensures the new gun is active
            secondaryGun.active = false;
            primaryGun.active = true;
        }
    }

    #endregion

    #region Items

    public void AssigItemActive(GameObject activeItemFloor){
        if(activeItem == null)
            PickActiveItem(activeItemFloor);
        else
            DropActive(activeItemFloor);

    }

    void PickActiveItem(GameObject newActiveItem){
        activeItem = newActiveItem;
        pickUp_ItemActive = activeItem.GetComponent<PickUp_ItemActive>();
        //activeItem = this.transform.GetChild(1).GetChild(0).gameObject;
    }

    void DropActive(GameObject newActiveItem){
        activeItem.transform.position = newActiveItem.transform.position;
        activeItem.transform.parent = null;

        pickUp_ItemActive.Drop();

        activeItem = newActiveItem;
        pickUp_ItemActive = activeItem.GetComponent<PickUp_ItemActive>();
    }

    #endregion

    #endregion

}
