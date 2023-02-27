using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Guns
{
    //private AudioManager sound;
    [SerializeField]
    private int amountOfPellets;
    public float spread;

    [Header("Bullet Effects")]
    [SerializeField]
    protected GameObject proyectilePrefab;
    [SerializeField]
    protected Color bulletColor;
    [SerializeField]
    protected Color bulletTrailColor;

    public override void IsLoaded()
    {
        if(currentAmmo > 0 || infiniteAmmo)
            if(canFire)
                FireGun();
        else
            Debug.Log("No tiene municion");
    }

    protected override void FireGun()
    {
        // Genero un proyectil por la cantidad de pellets
        gunAudioSource.PlayOneShot(fireClip);
        fireEffect.Play();
        for (var i = 0; i < amountOfPellets; i++)
        {
            // Genero el proyectil del arma
            GameObject bullet = Instantiate(proyectilePrefab, cannon.position , cannon.rotation);
            bullet.GetComponent<Bullet>().SetStatsEffects(damage ,bulletLifeTime ,bulletTrailColor ,bulletColor);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            // Le declaro una direction
            Vector2 dir = transform.rotation * Vector2.right;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-spread, spread);
            rb.velocity = (dir + pdir) * velocityProyectile;
        }

        canFire = false;
        base.FireGun();
    }

    public override void OffGun()
    {
        canFire = true;
    }

    public override void AmmoCheck()
    {
        //Pregunto si tiene ese tipo de municion
        if(inventoryManager.greenAmmo > 0)
        {
            //Calculo la cantidad de municion a recargar
            ammoPlus = maxAmmo - currentAmmo;
            Debug.Log("Municion a recargar: " + ammoPlus);

            //Chequeo para que no quede negativo el inventario
            if(inventoryManager.greenAmmo < 0)
            {
                ammoPlus += inventoryManager.greenAmmo;
                inventoryManager.greenAmmo = 0;
            }

            //Arranco la corutina de recarga
            StartCoroutine(ReloadGun(ammoPlus));
        }
        else
        {
            Debug.Log("No hay suficiente municion para: " + gameObject.name);
            gunAudioSource.PlayOneShot(noAmmoClip);
        }
    }
}