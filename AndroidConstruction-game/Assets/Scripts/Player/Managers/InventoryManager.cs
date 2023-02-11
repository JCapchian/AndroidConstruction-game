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
    private int maxGreyAmmo;
    [SerializeField]
    private int maxGreenAmmo;
    [SerializeField]
    private int maxPurpleAmmo;

    public int gunsAmount;
    bool hasTwoGuns = false;
    public List<Guns> gunsEquiped;
    public Guns activeGun;

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

    public int circuitsCont;
    public int scrapCount;

    public PickUp_ItemActive pickUp_ItemActive;
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

        greyAmmo = maxGreyAmmo;
        greenAmmo = maxGreenAmmo;
        purpleAmmo = maxPurpleAmmo;

        for (var i = 0; i < gunsAmount; i++)
            gunsEquiped.Add(null);

        Debug.Log(gunsEquiped.Count);
    }

    #region GunsRegions

    /// <summary>Asigna la pocision del arma</summary>
    public void AssignGuns(Guns gunPicked)
    {
        //Pregunta si tiene primaria
        if(!gunsEquiped[0])
        {
            EquipGun(gunPicked, 0);
            return;
        }
        //Pregunto si tiene secundaria
        if(!gunsEquiped[1])
        {
            EquipGun(gunPicked, 1);
            TurnGun(0, false);
            hasTwoGuns = true;
            return;
        }
    }

    /// <summary>Equipa el arma</summary>
    private void EquipGun(Guns gunPicked, int gunPosition)
    {
        // La declaro en la lista
        gunsEquiped[gunPosition] = gunPicked;

        // Cambio su orientacion
        gunsEquiped[gunPosition].transform.rotation = gunContainer.transform.rotation;
        gunsEquiped[gunPosition].transform.position = gunContainer.transform.position;

        // Activo el objeto
        gunsEquiped[gunPosition].gameObject.SetActive(true);

        // La asigno como padre
        gunsEquiped[gunPosition].transform.parent = gunContainer.transform;

        // La guardo como arma activa
        activeGun = gunsEquiped[gunPosition];
    }

    /// <summary>Cambia entre las armas de jugador</summary>
    public void ChangeGun()
    {
        // Pregunto la cantidad de armas equipadas
        if(hasTwoGuns)
        {
            // Pregunto si tiene el arma equipada
            if(activeGun == gunsEquiped[0])
            {
                activeGun = gunsEquiped[1];
                TurnGun(0, false);
                TurnGun(1, true);

                gUIManager.GunUpdateGUI();
            }
            else
            {
                activeGun = gunsEquiped[0];
                TurnGun(1,false);
                TurnGun(0, true);

                gUIManager.GunUpdateGUI();
            }
        }
        else
            Debug.Log("Solo tienes un arma");
    }

    /// <summary>Prende y apaga las armas</summary>
    void TurnGun(int gunPosition, bool op)
    {
        if(op)
            gunsEquiped[gunPosition].gameObject.SetActive(true);
        else
            gunsEquiped[gunPosition].gameObject.SetActive(false);
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
                Debug.Log("Uso: " + ammoQuantity + " de municion gris");
                break;
            case "greenAmmo":
                greenAmmo -= ammoQuantity;
                // Actualizo interfaz
                gUIManager.InvAmmoUpdateGUI(greenAmmo);
                Debug.Log("Uso: " + ammoQuantity + " de municion verde");
                break;
            case "purpleAmmo":
                purpleAmmo -= ammoQuantity;
                // Actualizo interfaz
                gUIManager.InvAmmoUpdateGUI(purpleAmmo);
                Debug.Log("Uso: " + ammoQuantity + " de municion purpura");
                break;
        }
        // Actualizo interfaz
        //gUIManager.InvAmmoUpdateGUI(ammoQuantity);
        
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
                gUIManager.InvAmmoUpdateGUI(ammoQuantity);
                Debug.Log("Obtuvo: " + ammoQuantity + " de municion gris");
                break;
            case "greenAmmo":
                greenAmmo += ammoQuantity;
                // Actualizo interfaz
                gUIManager.InvAmmoUpdateGUI(ammoQuantity);
                Debug.Log("Obtuvo: " + ammoQuantity + " de municion verde");
                break;
            case "purpleAmmo":
                purpleAmmo += ammoQuantity;
                // Actualizo interfaz
                gUIManager.InvAmmoUpdateGUI(ammoQuantity);
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

    public void ClearCircuitsMetal()
    {
        
    }

    #endregion
}
