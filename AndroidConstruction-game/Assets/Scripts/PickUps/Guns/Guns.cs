using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Guns : Interactable
{
    public SpriteRenderer spriteGun;

    [Header("Componentes")]
    [SerializeField]
    public Sprite gunImage;
    [SerializeField]
    protected Transform cannon;

    [Header("Propiedades")]
    [SerializeField]
    protected bool isActive;
    [SerializeField]
    protected bool canFire = true;
    enum AmmoTypes
    {
        greyAmmo,
        greenAmmo,
        purpleAmmo
    }

    [Header("Clips de Audio")]
    [SerializeField]
    protected AudioSource gunAudioSource;
    [SerializeField]
    protected AudioClip interactClip;
    [SerializeField]
    protected AudioClip fireClip;
    [SerializeField]
    protected AudioClip noAmmoClip;
    [SerializeField]
    protected AudioClip reloadClip;
    [SerializeField]
    protected AudioClip finishReloadClip;

    [Header("Estadisticas Generales")]
    [SerializeField]
    AmmoTypes ammo;
    public string ammoType;
    protected int ammoPlus;
    public bool infiniteAmmo;
    public ParticleSystem fireEffect;
    public float velocityProyectile = 20f;
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime;
    public bool reloading = false;
    public int damage;

    protected override void Awake()
    {
        base.Awake();

        gunAudioSource = GetComponent<AudioSource>();
        cannon = transform.GetChild(1).transform;
        //hubRef = GameObject.Find("UIArma").GetComponent<WeaponHUB>();
        fireEffect = GetComponent<ParticleSystem>();
        spriteGun = GetComponent<SpriteRenderer>();
        gunImage = spriteGun.sprite;

        currentAmmo = maxAmmo;

        ammoType = ammo.ToString();
    }

    public override void Interact(GameObject playerRef)
    {
        // Declaro una referencia del inventario
        inventoryManager.EquipGun();

        gUIManager.GunUpdateGUI();

        isActive = true;

        gunAudioSource.PlayOneShot(interactClip);
    }

    /// <summary>Funcion cuando preciona el boton de disparo</summary>
    public virtual void IsLoaded()
    {

    }

    /// <summary>Funcion cuando suelto el boton de disparo</summary>
    public virtual void OffGun()
    {

    }

    /// <summary>Funcion de disparo en general</summary>
    protected virtual void FireGun()
    {
        // Pregunto si no tiene municion infinita
        if(!infiniteAmmo)
            currentAmmo--;

        // Actualizo GUI
        gUIManager.CurrentAmmoUpdateGUI(currentAmmo);
    }

    /// <summary>Funcion de recarga cuando le da al boton de recarga</summary>
    public virtual void AmmoCheck()
    {

    }

    /// <summary>Corutina que arranca el tiempo de la recarga</summary>
    protected virtual IEnumerator ReloadGun(int reloadAmmo)
    {
        Debug.Log("Reacargado...");
        gunAudioSource.PlayOneShot(reloadClip);

        reloading = true;

        yield return new WaitForSeconds(reloadTime);

        reloading = false;

        currentAmmo += reloadAmmo;
        gUIManager.CurrentAmmoUpdateGUI(currentAmmo);
        inventoryManager.LeesAmmo(ammoType,ammoPlus);

        gunAudioSource.PlayOneShot(finishReloadClip);
        Debug.Log("Recargado");
    }

    protected virtual void SetBulletEffect()
    {

    }
}
