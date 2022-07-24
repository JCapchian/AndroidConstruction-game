using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Script : MonoBehaviour
{
    //Type of proyectile
    public GameObject proyectilePrefab;
    public Transform cannon;
    public Inventory inventoryReference;

    [Header("Estadisticas")]
    public bool semi;
    public string ammoType;
    public float BPM;
    private float totalBPM;
    public ParticleSystem fireEffect;
    public float speed = 20f;
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime;
    public int damage;

    private void Awake() {
        totalBPM = BPM;
        BPM = 0;
        currentAmmo = maxAmmo;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            inventoryReference = other.gameObject.GetComponent<Inventory>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(currentAmmo > 0)
        {
            if(semi)
            {
                if(Input.GetKeyDown(KeyCode.Mouse0))
                    FireWeapon();
                    currentAmmo -= 1;
            }
            else
            {
                if(BPM > 0)
                    BPM -= Time.deltaTime;    
                else
                {
                    if(Input.GetKey(KeyCode.Mouse0))
                    {
                        FireWeapon();
                        currentAmmo -= 1;
                        BPM = totalBPM;
                    }
                }   
            }
        }

        if(Input.GetButtonDown("Reload"))
        {
            if(currentAmmo == 0)
                StartCoroutine(ReloadWeapon());
            else
                Debug.Log("Aun hay municion en el cargador");
        }
    }

    private void FireWeapon()
    {
        GameObject bullet = Instantiate(proyectilePrefab, cannon.position , cannon.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(cannon.right * speed, ForceMode2D.Impulse);
        bullet.GetComponent<Bullet>().damageBullet = damage;

        fireEffect.Play();
    }

    public IEnumerator ReloadWeapon()
    {
        var reloadAmmo = 0;

        Debug.Log("Recargando...");
        yield return new WaitForSeconds(reloadTime);
        reloadAmmo = maxAmmo - currentAmmo;
        currentAmmo += reloadAmmo;
        switch (ammoType)
        {
            case ("Grey"):
                inventoryReference.greyAmmo -= reloadAmmo;
                break;
            case ("Green"):
                inventoryReference.greenAmmo -= reloadAmmo;
                break;
            case ("Purple"):
                inventoryReference.purpleAmmo -= reloadAmmo;
                break;
        }
        Debug.Log("Tu mama");
    }

}
