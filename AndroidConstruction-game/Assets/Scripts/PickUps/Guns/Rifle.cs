using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Guns
{
    [SerializeField]
    private bool semi;
    [SerializeField]
    private float rateOfFire;
    [SerializeField]
    private float ROF;

    [Header("Bullet Effects")]
    [SerializeField]
    protected GameObject proyectilePrefab;
    [SerializeField]
    protected Color bulletColor;
    [SerializeField]
    protected Color bulletTrailColor;

    protected override void Awake() {
        // Funcion base del Awake
        base.Awake();

        // Pregunto si es semi automatica
        if(semi)
            // Seteo que puede realizar el siguiente disparo
            canFire = true;
        else
            // Si no seteo su tiempo de disparo
            ROF = rateOfFire;
    }

    public override void IsLoaded()
    {
        // Pregunta si tiene municion o si tiene municion infinita activado
        if(currentAmmo > 0 || infiniteAmmo)
            // Pregunta si es semi-automatica y si puede disparar
            if(semi && canFire)
                FireGun();
            // Pregunta si la funcion del calculo del tiempo entre disparo devuelva positivo
            else if(CalculateRateOfFire())
                FireGun();
        else
            Debug.Log("No tiene municion");
    }

    protected override void FireGun()
    {
        // Reproduzco los effectos del arma
        gunAudioSource.PlayOneShot(fireClip);
        fireEffect.Play();

        // Genero el proyectil del arma
        GameObject bullet = Instantiate(proyectilePrefab, cannon.position , cannon.rotation);
        bullet.GetComponent<Bullet>().SetStatsEffects(damage ,bulletLifeTime ,bulletTrailColor ,bulletColor);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(cannon.right * velocityProyectile, ForceMode2D.Impulse);

        // Le indico que no puede disparar
        canFire = false;
        // Continua la funcion base
        base.FireGun();
    }

    public override void OffGun()
    {
        // Vuelve a cargar el tiempo de disparo
        ROF = rateOfFire;
        // Le indico que puede disparar
        canFire = true;
    }

    private bool CalculateRateOfFire()
    {
        // Pregunto si la variable que maneja el tiempo de disparo es mayor a 0
        if(ROF > 0)
        {
            // Si lo es le va restando con el paso del tiempo
            ROF -= Time.deltaTime;
            return false;
        }
        else
        {
            // Recarga la variable que maneja el tiempo de disparo
            ROF = rateOfFire;
            return true;
        }
    }

    public override void AmmoCheck()
    {
        // Pregunta si no tiene municion infinita
        if(!infiniteAmmo)
        {
            // Pregunto si tiene ese tipo de municion
            if(inventoryManager.greyAmmo > 0)
            {
                // Calculo la cantidad de municion a recargar
                ammoPlus = maxAmmo - currentAmmo;
                Debug.Log("Municion a recargar: " + ammoPlus);

                // Chequeo para que no quede negativo el inventario
                if(inventoryManager.greyAmmo < 0)
                {
                    ammoPlus += inventoryManager.greyAmmo;
                    inventoryManager.greyAmmo = 0;
                }

                // Arranco la corutina de recarga
                StartCoroutine(ReloadGun(ammoPlus));
            }
            else
                gunAudioSource.PlayOneShot(noAmmoClip);
        }
    }
}
