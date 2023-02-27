using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Managers
    [SerializeField]
    GUIManager gUIManager;

    [SerializeField]
    private GameObject gunContainer;
    public Interactable closeItem;

    [Header ("Inventario Jugador")]
    public int greyAmmo;
    public int greenAmmo;
    public int purpleAmmo;

    [SerializeField]
    private int lastGreyAmmo;
    [SerializeField]
    private int lastGreenAmmo;
    [SerializeField]
    private int lastPurpleAmmo;

    [Header ("Armas invetario")]
    public int gunsAmount;
    [SerializeField]
    public List<Guns> gunsEquiped;
    public Guns activeGun;
    [SerializeField]
    int  activeGunPosition;
    [SerializeField]
    Transform dropPoint;
    [SerializeField]
    float changeGunCooldown;
    [SerializeField]
    bool canChangeGun;

    [Header ("Recolectables")]
    // LLaves
    [SerializeField]
    bool greenKey = false;
    [SerializeField]
    bool redKey = false;
    [SerializeField]
    bool blueKey = false;
    [SerializeReference]
    public bool hasAllKeys;

    public int circuitsAmount;
    public int scrapAmount;

    public GameObject activeItem;
    public float haveActiveItem;
    public GameObject[] pasiveItem;

    [Header("Audio Source & Audio Clips")]
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip completeObjetiveClip;
    [SerializeField]
    AudioClip pickUpClip;

    private void Awake()
    {
        gUIManager = FindObjectOfType<GUIManager>();

        audioSource = GetComponent<AudioSource>();

        gunContainer = transform.GetChild(0).GetChild(0).gameObject;

        for (var i = 0; i < 3; i++)
            gunsEquiped.Add(null);

        dropPoint = transform.GetChild(2).transform;
        Debug.Log(gunsEquiped.Count);
    }

    #region GunsRegions

    /// <summary>Equipa el arma</summary>
    public void EquipGun()
    {
        if(closeItem.GetComponent<Guns>())
        {
            var gunPicked = closeItem.GetComponent<Guns>();
            TurnGun(false);
            // La declaro en la lista
            gunsEquiped[gunsAmount] = gunPicked;

            // Cambio su orientacion
            gunsEquiped[gunsAmount].transform.rotation = gunContainer.transform.rotation;
            gunsEquiped[gunsAmount].transform.position = gunContainer.transform.position;

            // La asigno como padre
            gunsEquiped[gunsAmount].transform.parent = gunContainer.transform;
            gunsEquiped[gunsAmount].c2D.enabled = false;

            // La guardo como arma activa
            activeGun = gunsEquiped[gunsAmount];

            // Activo el objeto
            TurnGun(true);
            //activeGunPosition = gunsAmount;

            gunsAmount ++;
        }
        else
            Debug.Log("Eso no es un arma");
    }

    /// <summary>Prende y apaga las armas</summary>
    void TurnGun(bool state)
    {
        if(activeGun)
           activeGun.gameObject.SetActive(state);
    }

    /// <summary>Cambia entre las armas de jugador</summary>
    public void ChangeGun(int gunPosition)
    {
        if(canChangeGun)
        {
            canChangeGun = false;

            TurnGun(false);

            activeGun = gunsEquiped[gunPosition];
            activeGunPosition = gunPosition;

            TurnGun(true);

            gUIManager.GunUpdateGUI();

            StartCoroutine(ChangeGunTimer());
        }
        else
            Debug.Log("Cant Change guns");
    }

    IEnumerator ChangeGunTimer()
    {
        yield return new WaitForSeconds(changeGunCooldown);
        canChangeGun = true;
    }

    /// <summary>Se llama para restar municion del inventario</summary>
    public void LeesAmmo(string ammoType, int ammoQuantity)
    {
        // Pregunta que tipo de municion va a restar
        switch (ammoType)
        {
            case "greyAmmo":
                greyAmmo -= ammoQuantity;
                // Actualizo interfaz
                gUIManager.InvAmmoUpdateGUI(greyAmmo);
                break;
            case "greenAmmo":
                greenAmmo -= ammoQuantity;
                // Actualizo interfaz
                gUIManager.InvAmmoUpdateGUI(greenAmmo);
                break;
            case "purpleAmmo":
                purpleAmmo -= ammoQuantity;
                // Actualizo interfaz
                gUIManager.InvAmmoUpdateGUI(purpleAmmo);
                break;
        }
    }

    #endregion

    #region PickUp Region

    public void PickUpAmmo(string ammoType, int ammoQuantity)
    {
        switch (ammoType)
        {
            case "greyAmmo":
                greyAmmo += ammoQuantity;
                // Actualizo interfaz
                gUIManager.InvAmmoUpdateGUI(greyAmmo);
                Debug.Log("Obtuvo: " + ammoQuantity + " de municion gris");
                break;
            case "greenAmmo":
                greenAmmo += ammoQuantity;
                // Actualizo interfaz
                gUIManager.InvAmmoUpdateGUI(greenAmmo);
                Debug.Log("Obtuvo: " + ammoQuantity + " de municion verde");
                break;
            case "purpleAmmo":
                purpleAmmo += ammoQuantity;
                // Actualizo interfaz
                gUIManager.InvAmmoUpdateGUI(purpleAmmo);
                Debug.Log("Obtuvo: " + ammoQuantity + " de municion purpura");
                break;
        }

    }

    public void PickUpKey(string keyName)
    {
        // Identifico cual llave es
        switch (keyName)
        {
            case "greenKey":
                greenKey = true;
                gUIManager.TurnKey("greenKey");
                // Funcion del hud
                break;
            case "blueKey":
                blueKey = true;
                gUIManager.TurnKey("blueKey");
                // Funcion del hud
                break;
            case "redKey":
                redKey = true;
                gUIManager.TurnKey("redKey");
                // Funcion del hud
                break;
        }

        if(greenKey && blueKey && redKey)
        {
            hasAllKeys = true;
            audioSource.PlayOneShot(completeObjetiveClip);
        }
    }

    public void ResetKeys()
    {
        greenKey = false;
        blueKey = false;
        redKey = false;

        hasAllKeys = false;
    }
    #endregion

    #region Interaction region
    private void OnTriggerEnter2D(Collider2D other) {
        // Pregunta si es un arma
        if(other.GetComponent<Interactable>())
            closeItem = other.GetComponent<Interactable>();
    }

    private void OnTriggerExit2D(Collider2D other) {
        // Pregunta si es un arma
        if(other.GetComponent<Interactable>())
            closeItem = null;
    }
    #endregion

    #region Actions region

    public void ClearKeys()
    {
        greenKey = false;
        redKey = false;
        blueKey = false;

        hasAllKeys = false;
    }

    public void ClearCircuitsScrap()
    {
        circuitsAmount = 0;
        scrapAmount = 0;
    }

    public void SaveInventory()
    {
        lastGreenAmmo = greenAmmo;
        lastGreyAmmo = greyAmmo;
        lastPurpleAmmo = purpleAmmo;
    }

    public void LoadInventory()
    {
        greenAmmo = lastGreenAmmo;
        greyAmmo = lastGreyAmmo;
        purpleAmmo = lastPurpleAmmo;
    }
    #endregion
}
