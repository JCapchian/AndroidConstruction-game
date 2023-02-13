using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    // Managers
    GameObject playerRef;
    StatsManager statsManager;
    InventoryManager inventoryManager;
    PlayerManager playerManager;

    [Header ("Stats Jugador")]
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private TMP_Text maxHealthText;
    [SerializeField]
    private TMP_Text currentHealthText;

    [Header ("Stats Armas")]
    #region statsArmas
    public TMP_Text curretAmmo;
    public TMP_Text invAmmo;
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


    [Header ("Informacion de la partida")]
    #region infoPartida
    public int enemigosElimindos = 0;
    public int chatarraRecoletada = 0;
    public int componentesRecoletados = 0;

    [Header ("Textos")]
    private TMP_Text textAviso;
    public TMP_Text textChatarra;
    public TMP_Text textComponente;
    public TMP_Text contEnemiesDEATH;
    public TMP_Text contChatarraDEATH;
    public TMP_Text contComponentesDEATH;
    public TMP_Text contEnemiesWIN;
    public TMP_Text contChatarraWIN;
    public TMP_Text contComponentesWIN;

    #endregion

    [Header ("Efectos")]
    #region efectos
    [SerializeField]
    private float transitionTime = 1f;
    [SerializeField]
    private Animator transition;
    [SerializeField]
    private GameObject crosshairObject;
    #endregion

    [Header ("Pantallas")]
    #region pantallas
    [SerializeField]
    private Vector2 screenBounds;
    public bool isPaused;
    public GameObject pauseScreen;
    public GameObject deathScreen;
    public GameObject winScreen;
    #endregion


    private void Awake() {
        // Busco y guardo al jugador como referencia
        playerRef = FindObjectOfType<PlayerManager>().gameObject;

        // Declaro los managers dentro del jugador
        playerManager = playerRef.GetComponent<PlayerManager>();
        statsManager = playerRef.GetComponent<StatsManager>();
        inventoryManager = playerRef.GetComponent<InventoryManager>();

        // Asignos los componentes que voy a utilizar
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height , 0));

        // Vida
        healthSlider = transform.GetChild(0).GetComponent<Slider>();
        currentHealthText = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TMP_Text>();
        maxHealthText = transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TMP_Text>();

        SetMaxHealth(statsManager.maxHealth);

        // Armas
        curretAmmo = transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>();
        invAmmo = transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>();
        weaponIcon = transform.GetChild(2).GetChild(3).GetComponent<Image>();

        var tempColor = weaponIcon.color;
        tempColor.a = 0f;
        weaponIcon.color = tempColor;

        // Recoletables
        textChatarra = transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TMP_Text>();
        textComponente = transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<TMP_Text>();

        contEnemiesDEATH = transform.GetChild(3).GetChild(2).GetChild(1).GetComponent<TMP_Text>();
        contChatarraDEATH = transform.GetChild(3).GetChild(4).GetChild(2).GetComponent<TMP_Text>();
        contComponentesDEATH = transform.GetChild(3).GetChild(3).GetChild(2).GetComponent<TMP_Text>();

        contEnemiesWIN = transform.GetChild(4).GetChild(2).GetChild(1).GetComponent<TMP_Text>();
        contChatarraWIN = transform.GetChild(4).GetChild(4).GetChild(2).GetComponent<TMP_Text>();
        contComponentesWIN = transform.GetChild(4).GetChild(3).GetChild(2).GetComponent<TMP_Text>();

        KeysGUI = transform.GetChild(6).gameObject;
        redKeyImage = KeysGUI.transform.GetChild(0).GetComponent<Image>();
        greenKeyImage = KeysGUI.transform.GetChild(1).GetComponent<Image>();
        blueKeyImage = KeysGUI.transform.GetChild(2).GetComponent<Image>();

        redKeyImage.rectTransform.anchoredPosition = new Vector2(-400, -50);
        greenKeyImage.rectTransform.anchoredPosition = new Vector2(-250, -50);
        blueKeyImage.rectTransform.anchoredPosition = new Vector2(-100, -50);

        // Pantallas
        pauseScreen = transform.GetChild(5).gameObject;
        deathScreen = transform.GetChild(3).gameObject;
        winScreen = transform.GetChild(4).gameObject;

        deathScreen.SetActive(false);
        winScreen.SetActive(false);

        // Effects & Detail
        crosshairObject = transform.GetChild(7).gameObject;
        Cursor.visible = false;

        TurnResumeOrPause(false);

        // Text
        textAviso = transform.GetChild(8).GetComponent<TMP_Text>();

        enemigosElimindos = 0;
        chatarraRecoletada = 0;
        componentesRecoletados = 0;


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
/*
    private void CheckBoundries()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height , Camera.main.transform.position.z));

        Vector3 viewPos =  crosshairObject.transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x, screenBounds.x * -1);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y, screenBounds.y * -1);
        
        crosshairObject.transform.position = viewPos;
    }
    */
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

        contEnemiesDEATH.text = "X" + enemigosElimindos.ToString();
        contChatarraDEATH.text = "X" + chatarraRecoletada.ToString();
        contComponentesDEATH.text = "X" + componentesRecoletados.ToString();
    }

    public void ShowWinScreen()
    {
        winScreen.gameObject.SetActive(true);

        Time.timeScale = 0;

        contEnemiesDEATH.text = "X" + enemigosElimindos.ToString();
        contChatarraDEATH.text = "X" + chatarraRecoletada.ToString();
        contComponentesDEATH.text = "X" + componentesRecoletados.ToString();
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
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #endregion

    #region PickUps && Recolectables

    public void TurnKey(string keyName)
    {
        var targetPosition = new Vector2(0, 100);

        switch (keyName)
        {
            case "greenKey":
                Debug.Log("LLave verde GUI");
                greenKeyImage.rectTransform.anchoredPosition = Vector2.Lerp(greenKeyImage.rectTransform.anchoredPosition, new Vector2(-250,100), showKeyTime);
                // Funcion del hud
                break;
            case "blueKey":
                Debug.Log("LLave blue GUI");
                blueKeyImage.rectTransform.anchoredPosition = Vector2.Lerp(blueKeyImage.rectTransform.position, new Vector2(-100,100), showKeyTime);
                // Funcion del hud
                break;
            case "redKey":
                Debug.Log("LLave verde GUI");
                redKeyImage.rectTransform.anchoredPosition = Vector2.Lerp( redKeyImage.rectTransform.anchoredPosition, new Vector2(-400,100), showKeyTime);
                // Funcion del hud
                break;
        }
    }

    /// <summary>Se llama cada vez que se recoge un collecionable</summary>
    public void PickUpRecolectable(string Type)
    {
        switch (Type)
        {
            case "Chatarra":
                chatarraRecoletada += 1;
                Debug.Log("+1 Chatarra");
                textChatarra.text = ("X" + chatarraRecoletada.ToString());
                Debug.Log("Chatarra Obtenida= " + chatarraRecoletada);
                break;
            case "Componente":
                componentesRecoletados += 1;
                Debug.Log("+1 Componentes");
                textComponente.text = ("X" + componentesRecoletados.ToString());
                Debug.Log("Componente Obtenida= " + chatarraRecoletada);
                break;
        }
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
            curretAmmo.enabled = false;
            invAmmo.enabled = false;
        }
        else
        {
            curretAmmo.enabled = true;
            invAmmo.enabled = true;

            // Cambio el valor de la municion
            curretAmmo.text = "" + inventoryManager.activeGun.currentAmmo;
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
        curretAmmo.text = "" + ammoQuantity;
    }

    public void InvAmmoUpdateGUI(int ammoQuantity)
    {
        invAmmo.text = "" + ammoQuantity;
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

    #region Counts Region

    /// <summary>Se llama cada vez que se elemina un enemigo</summary>
    public void EnemeyCountUpdate()
    {
        enemigosElimindos += 1;
        Debug.Log("Enemigos eliminados= " + enemigosElimindos);
    }

    #endregion

}
