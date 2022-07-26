using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Script : MonoBehaviour
{
    [Header("Propiedades")]
    //Type of proyectile
    public GameObject proyectilePrefab;
    public Transform cannon;
    [SerializeField]
    private Inventory inv;
    [SerializeField]
    private AudioManager sound;
    [SerializeField]
    private WeaponHUB hubRef;

    [Header("Estadisticas Generales")]
    public string weaponType;
    public string ammoType;
    public bool semi;
    public float BPM;
    private float totalBPM;
    public ParticleSystem fireEffect;
    public float speed = 20f;
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime;
    public bool reloading = false;
    private int reloadAmmo;
    public int damage;

    [Header("Estadisticas Laser")]
    public float distanceRay = 100;
    public float maxChargeTime;
    [SerializeField]
    private float chargeTime;
    public float maxAmmoPerTime;
    [SerializeField]
    private float ammoPerTime;

    public float maxDamagePerTime;
    [SerializeField]
    private float damagePerTime;


    [Header ("Propiedades Laser")]
    public LineRenderer ref_LineRenderer;
    Transform ref_TransformGun;
    [SerializeField]
    private bool charged = false;
    public EnemyStatsController targetedEnemy;
    
    [Header("Estadisticas Shotgun")]
    public int amountOfBullets;
    public float spread;

    private void Awake() {
        //Scripts Things
        sound = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        inv = GameObject.Find("Player").GetComponent<Inventory>();
        hubRef = GameObject.Find("UIArma").GetComponent<WeaponHUB>();
        ref_TransformGun = GetComponent<Transform>();
        
        //General Things
        totalBPM = BPM;
        BPM = 0;
        currentAmmo = maxAmmo;

        // Laser Things
        chargeTime = maxChargeTime;
        ammoPerTime = maxAmmoPerTime;
        damagePerTime = maxDamagePerTime;
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player")
            inv = other.gameObject.GetComponent<Inventory>();

    }

    // Update is called once per frame
    void Update()
    {   
        // Checks the weapontype
        if(currentAmmo > 0)
        {
            switch (weaponType)
            {
                case "Laser":
                    LaserFunction();
                    break;
                case "Shotgun":
                    ShotgunFunction();
                    break;
                case "Rifle":
                    RifleFunction();
                    break;
                default:
                    break;
            }
        }

        if(Input.GetButtonDown("Reload"))
        {
            if(currentAmmo < maxAmmo)
            {
                reloadAmmo = maxAmmo - currentAmmo;

                if(inv.GetAmmo(ammoType) >= reloadAmmo)
                    StartCoroutine(ReloadWeapon());
                else
                    Debug.Log("No municion en el inventario");
            }
            else
                Debug.Log("Aun hay municion en el cargador");
        }
        
    }

    #region Laser_Weapon
    private void LaserFunction()
    {
        if(Input.GetKey(KeyCode.Mouse0) && reloading == false)
            ShootLaser();
        else
        {
            targetedEnemy = null;
            ref_LineRenderer.enabled = false;
            charged = false;

            // Charge the chargeTime
            if(charged == false && chargeTime < maxChargeTime)
                chargeTime = maxChargeTime;   
        }
    }

    private void ShootLaser()
    {
        //Starts the timer to charge it
        if(chargeTime > 0)
            chargeTime -= Time.deltaTime;
        else
            charged = true;

        if(charged == true)
        {
            ref_LineRenderer.enabled = true;
            //"Physics2D.Raycast" cheks if the rate hash got somthing
            if (Physics2D.Raycast(ref_TransformGun.position, transform.right))
            {
                //Take the position of the canon and draw a laser
                RaycastHit2D _hit = Physics2D.Raycast(cannon.position, transform.right);
                Draw2DRay(cannon.position, _hit.point);
                ConsumeAmmoLaser();

                //When the laser target a enemy
                Debug.Log(_hit.collider.gameObject.name);
                if(_hit.collider.gameObject.tag == "Enemy")
                {
                    targetedEnemy = _hit.collider.gameObject.GetComponent<EnemyStatsController>();
                    Debug.Log("Impactando a: " + targetedEnemy);
                    
                    if(damagePerTime > 0)
                        damagePerTime -= Time.deltaTime;
                    else
                    {
                        targetedEnemy.TakeDamage(damage);
                        Debug.Log(targetedEnemy.name + "sufrio= " + damage + " de daño");
                        damagePerTime = maxDamagePerTime;
                    }
                }
            }
            else
            {
                //Use the default distance variable to draw the laser using the 'distanceRay'
                Draw2DRay(cannon.position, cannon.transform.right * distanceRay); 
                targetedEnemy = null;
                Debug.Log("El laser no esta impactando con nada");
            }
        }
    }

    //This function does is setting the position of the two point in the line renderer
    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        ref_LineRenderer.SetPosition(0, startPos);
        ref_LineRenderer.SetPosition(1, endPos);
    }

    void ConsumeAmmoLaser()
    {
        if(ammoPerTime > 0)
            ammoPerTime -= Time.deltaTime;   
        else
        {
            currentAmmo -= 1;
            hubRef.AmmoUpdate();
            ammoPerTime = maxAmmoPerTime;
        }
            
    }
    #endregion

    #region  Shotgun_Weapon
    private void ShotgunFunction()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireShotGun();
        }
    }

    void FireShotGun()
    {
        for (var i = 0; i < amountOfBullets; i++)
        {
            GameObject bullet = Instantiate(proyectilePrefab, cannon.position , cannon.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            
            Vector2 dir = transform.rotation * Vector2.right;
            Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-spread, spread);
            rb.velocity = (dir + pdir) * speed;
    
            sound.Play("Disparo1");
            fireEffect.Play();
            
        }

        currentAmmo -= 1;
        hubRef.AmmoUpdate();
    }
    #endregion

    #region Rifles&Pistols_Weapons

    private void RifleFunction()
    {
        if(semi)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0) && reloading == false)
                FireWeapon();

            currentAmmo -= 1;
            hubRef.AmmoUpdate();
        }
        else
        {
            if(BPM > 0)
                BPM -= Time.deltaTime;    
            else
            {
                if(Input.GetKey(KeyCode.Mouse0) && reloading == false)
                {
                    FireWeapon();
                    currentAmmo -= 1;

                    hubRef.AmmoUpdate();
                    BPM = totalBPM;
                }
            }   
        }
    }

    private void FireWeapon()
    {
        GameObject bullet = Instantiate(proyectilePrefab, cannon.position , cannon.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(cannon.right * speed, ForceMode2D.Impulse);
        bullet.GetComponent<Bullet>().damageBullet = damage;
        
        sound.Play("Disparo1");
        fireEffect.Play();
    }

    #endregion

    public IEnumerator ReloadWeapon()
    {
        Debug.Log("Recargando...");
        sound.Play("Reloading");
        
        reloading = true;

        yield return new WaitForSeconds(reloadTime);   

        reloading = false;

        currentAmmo += reloadAmmo;
        inv.UsedAmmo(reloadAmmo,ammoType);
        
        hubRef.AmmoUpdate();
        
        sound.Play("Reload");
        Debug.Log("Recargado");
    }
}
