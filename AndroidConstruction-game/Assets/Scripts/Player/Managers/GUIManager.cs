using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    GameObject playerPackage;
    public bool PrimerNivel;

    // Managers
    GameObject playerRef;
    StatsManager statsManager;
    InventoryManager inventoryManager;
    PlayerManager playerManager;
    [SerializeField]
    MusicPlayer musicPlayer;

    [Header ("Stats Jugador")]
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private TMP_Text maxHealthText;
    [SerializeField]
    private TMP_Text currentHealthText;

    [Header ("Stats Armas")]
    #region statsArmas
    [SerializeField]
    private TMP_Text currentAmmo;
    [SerializeField]
    private TMP_Text ammoSeparator;
    [SerializeField]
    private TMP_Text invAmmo;
    [SerializeField]
    private TMP_Text infiniteAmmo;
    public Image weaponIcon;
    #endregion

    [Header ("Objetos")]
    [SerializeField]
    private float showKeyTime;
    [SerializeField]
    private GameObject KeysGUI;
    [SerializeField]
    private Image redKeyImage;
    [SerializeField]
    private Image greenKeyImage;
    [SerializeField]
    private Image blueKeyImage;

    [Header ("Contadores")]
    public int enemigosElimindos = 0;

    [Header ("Textos")]
    private TMP_Text textAviso;

    [Header ("Efectos")]
    [SerializeField]
    private float transitionTime = 1f;
    [SerializeField]
    private Animator transition;
    [SerializeField]
    private GameObject crosshairObject;

    [Header ("Pantallas")]

    #region pantallas
    [SerializeField]
    public bool isPaused;
    [SerializeField]
    private GameObject pauseScreen;
    [SerializeField]
    private GameObject deathScreen;
    [SerializeField]
    private GameObject winScreen;

    #endregion


    private void Awake()
    {
        playerPackage = transform.parent.gameObject;

        var actualScene = SceneManager.GetActiveScene();

        // Busco y guardo al jugador como referencia
        playerRef = FindObjectOfType<PlayerManager>().gameObject;

        // Declaro los managers dentro del jugador
        playerManager = playerRef.GetComponent<PlayerManager>();
        statsManager = playerRef.GetComponent<StatsManager>();
        inventoryManager = playerRef.GetComponent<InventoryManager>();
        musicPlayer = FindObjectOfType<MusicPlayer>();

        // Pantallas
        pauseScreen = transform.GetChild(4).gameObject;
        deathScreen = transform.GetChild(2).gameObject;
        winScreen = transform.GetChild(3).gameObject;

        deathScreen.SetActive(false);
        winScreen.SetActive(false);

        // Vida
        healthSlider = transform.GetChild(0).GetComponent<Slider>();
        currentHealthText = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Text>();
        maxHealthText = transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TMP_Text>();

        SetMaxHealth(statsManager.maxHealth);

        // Armas
        invAmmo = transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        ammoSeparator = transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>();
        currentAmmo = transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>();
        weaponIcon = transform.GetChild(1).GetChild(3).GetComponent<Image>();
        infiniteAmmo = transform.GetChild(1).GetChild(4).GetComponent<TMP_Text>();

        var tempColor = weaponIcon.color;
        tempColor.a = 0f;
        weaponIcon.color = tempColor;

        KeysGUI = transform.GetChild(5).gameObject;
        redKeyImage = KeysGUI.transform.GetChild(0).GetComponent<Image>();
        greenKeyImage = KeysGUI.transform.GetChild(1).GetComponent<Image>();
        blueKeyImage = KeysGUI.transform.GetChild(2).GetComponent<Image>();

        redKeyImage.rectTransform.anchoredPosition = new Vector2(-400, -50);
        greenKeyImage.rectTransform.anchoredPosition = new Vector2(-250, -50);
        blueKeyImage.rectTransform.anchoredPosition = new Vector2(-100, -50);

        // Effects & Detail
        crosshairObject = transform.GetChild(6).gameObject;
        Cursor.visible = false;

        // Text
        textAviso = transform.GetChild(7).GetComponent<TMP_Text>();

        this.transform.SetParent(null);

        if(SceneManager.GetActiveScene().buildIndex != 2)
            DontDestroyOnLoad(this.gameObject);
    }

    #region Effects & Detail Region

    public void HandleEffects()
    {
        CrosshairMovement();
        //CheckBoundries();
    }

    void CrosshairMovement()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        crosshairObject.transform.position = mousePosition;
    }

    #endregion

    #region PlayerStats Region

    /// <summary>Rellena la barra de vida al maximo</summary>
    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
        currentHealthText.text = "" + health;
        maxHealthText.text = "" + health;
    }

    /// <summary>Cambia el valor de la barra de vida</summary>
    public void SetHealth(int health)
    {
        healthSlider.value = health;
        currentHealthText.text = "" + health;
    }

    #endregion

    #region Scenes Region

    IEnumerator ReinicioEscena()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        yield return new WaitForSeconds(1.5f);

        SHowDeathScreen();
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

    #region Menues Region

    public void SHowDeathScreen()
    {
        deathScreen.gameObject.SetActive(true);

        Time.timeScale = 0;
    }

    public void ShowWinScreen()
    {
        winScreen.gameObject.SetActive(true);

        Time.timeScale = 0;
    }

    private void TurnScreens(bool state)
    {
        winScreen.gameObject.SetActive(state);
        deathScreen.gameObject.SetActive(state);
    }

    #endregion

    #region TimeActions Region


    public void TurnResumeOrPause(bool state)
    {
        // Resume = true
        // Pause = false

        pauseScreen.gameObject.SetActive(state);
        if(state)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        playerManager.TurnStateManagersPlayer(!state);
        isPaused = !state;
    }
    public void Resume()
    {
        pauseScreen.gameObject.SetActive(false);
        Time.timeScale = 1;
        playerManager.TurnStateManagersPlayer(true);
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseScreen.gameObject.SetActive(true);
        Time.timeScale = 0;
        playerManager.TurnStateManagersPlayer(false);
        isPaused = true;
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        TurnScreens(false);
        
        playerManager.DestroyPlayer();
        Destroy(musicPlayer.gameObject);
        Cursor.visible = true;

        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        TurnScreens(false);

        Time.timeScale = 1;

        playerManager.ResetPlayer();
    }

    #endregion

    #region PickUps && Recolectables

    public void TurnKey(string keyName)
    {
        var targetPosition = new Vector2(0, 100);

        switch(keyName)
        {
            case "greenKey":
                greenKeyImage.rectTransform.anchoredPosition = Vector2.Lerp(greenKeyImage.rectTransform.anchoredPosition, new Vector2(-250,100), showKeyTime);
                break;
            case "blueKey":
                blueKeyImage.rectTransform.anchoredPosition = Vector2.Lerp(blueKeyImage.rectTransform.position, new Vector2(-100,100), showKeyTime);
                break;
            case "redKey":
                redKeyImage.rectTransform.anchoredPosition = Vector2.Lerp( redKeyImage.rectTransform.anchoredPosition, new Vector2(-400,100), showKeyTime);
                break;
        }
    }

    public void ResetKeys()
    {
        var targetPosition = new Vector2(0, -100);

        greenKeyImage.rectTransform.anchoredPosition = targetPosition;
        blueKeyImage.rectTransform.anchoredPosition = targetPosition;
        redKeyImage.rectTransform.anchoredPosition = targetPosition;
    }

    #endregion

    #region Guns Region

    /// <summary>Se llama cada vez que cambio de arma</summary>
    public void GunUpdateGUI()
    {
        // Cambio la imagen del arma
        weaponIcon.sprite = inventoryManager.activeGun.gunImage;

        // Le subo la opacidad a 1
        var tempColor = weaponIcon.color;
        tempColor.a = 1f;
        weaponIcon.color = tempColor;

        if(inventoryManager.activeGun.infiniteAmmo)
        {
            Debug.Log("infinite Ammo");

            currentAmmo.gameObject.SetActive(false);
            invAmmo.gameObject.SetActive(false);
            ammoSeparator.gameObject.SetActive(false);
            infiniteAmmo.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("No infinite Ammo");

            currentAmmo.gameObject.SetActive(true);
            invAmmo.gameObject.SetActive(true);
            ammoSeparator.gameObject.SetActive(true);
            infiniteAmmo.gameObject.SetActive(false);

            // Cambio el valor de la municion
            currentAmmo.text = "" + inventoryManager.activeGun.currentAmmo;
            AmmoUpdateColorInventory(inventoryManager.activeGun.ammoType);
        }
    }

    private void AmmoUpdateColorInventory (string ammoType)
    {
        switch (ammoType)
        {
            case "greyAmmo":
                invAmmo.text = "" + inventoryManager.greyAmmo;
                invAmmo.color = Color.gray;
                break;
            case "greenAmmo":
                invAmmo.text = "" + inventoryManager.greenAmmo;
                invAmmo.color = Color.green;
                break;
            case "purpleAmmo":
                invAmmo.text = "" + inventoryManager.purpleAmmo;
                invAmmo.color = Color.magenta;
                break;
        }
    }

    public void CurrentAmmoUpdateGUI(int ammoQuantity)
    {
        currentAmmo.text = "" + ammoQuantity;
    }

    public void InvAmmoUpdateGUI(int ammoQuantity)
    {
        if(inventoryManager.activeGun)
        {
            switch (inventoryManager.activeGun.ammoType)
            {
                case "greyAmmo":
                    invAmmo.text = "" + inventoryManager.greyAmmo;
                    break;
                case "greenAmmo":
                    invAmmo.text = "" + inventoryManager.greenAmmo;
                    break;
                case "purpleAmmo":
                    invAmmo.text = "" + inventoryManager.purpleAmmo;
                    break;
            }
        }
        //invAmmo.text = "" + ammoQuantity;
    }

    #endregion

    #region Text Region

    public void InteractuablesTextPopUp(string textoMostrar, bool state)
    {
        if(state)
        {
            textAviso.text = textoMostrar;
            textAviso.enabled = state;
        }
        else
            textAviso.enabled = state;
    }

    #endregion
}
