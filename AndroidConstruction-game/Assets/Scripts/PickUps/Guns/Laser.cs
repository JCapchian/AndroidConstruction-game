using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Guns
{

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
    public LineRenderer lineRenderer;
    Transform ref_TransformGun;
    [SerializeField]
    private bool charged = false;
    public EnemyStatsController targetedEnemy;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
        ref_TransformGun = GetComponent<Transform>();
    }

    public override void IsLoaded()
    {
        // Pregunta si esta recargando
        if(!reloading && currentAmmo > 0)
            // Si no Dispara el laser;
            ShootLaser();
    }

    protected override void FireGun()
    {

    }

    public override void OffGun()
    {
        // Apago el laser
        TurnOffLaser();
        // Charge the chargeTime
        if(charged == false && chargeTime < maxChargeTime)
            chargeTime = maxChargeTime;
    }

    private void ShootLaser()
    {
        if(ChargeLaser())
        {
            lineRenderer.enabled = true;
            //"Physics2D.Raycast" cheks if the rate hash got somthing
            if (Physics2D.Raycast(ref_TransformGun.position, transform.right, distanceRay, 6))
            {
                //Take the position of the canon and draw a laser
                RaycastHit2D _hit = Physics2D.Raycast(cannon.position, transform.right);
                Draw2DRay(cannon.position, _hit.point);
                ConsumeAmmoLaser();

                //When the laser target a enemy
                //Debug.Log(_hit.collider.gameObject.name);
                if(_hit.collider.gameObject.tag == "Enemy")
                {
                    targetedEnemy = _hit.collider.gameObject.GetComponent<EnemyStatsController>();
                    //Debug.Log("Impactando a: " + targetedEnemy);

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
        else
            TurnOffLaser();
    }

    /// <summary>Dibuja la linea del laser entre dos posiciones dadas</summary>
    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    /// <summary>Consume muncion con el tiempor</summary>
    void ConsumeAmmoLaser()
    {
        if(infiniteAmmo == false)
        {
            if(ammoPerTime > 0)
                ammoPerTime -= Time.deltaTime;   
            else
            {
                currentAmmo -= 1;
                //hubRef.AmmoUpdate();
                ammoPerTime = maxAmmoPerTime;
            }
        }   
    }

    /// <summary>Apaga el laser</summary>
    void TurnOffLaser()
    {
        targetedEnemy = null;
        lineRenderer.enabled = false;
        charged = false;
    }

    /// <summary>Carga el laser</summary>
    bool ChargeLaser()
    {
        //Arranca el timer de la carga
        if(chargeTime > 0)
        {
            chargeTime -= Time.deltaTime;
            TurnOffLaser();
            return false;
        }
        else
        {
            return true;
        }
    }

    public override void AmmoCheck()
    {
        //Pregunto si tiene ese tipo de municion
        if(inventoryManager.purpleAmmo > 0)
        {
            //Calculo la cantidad de municion a recargar
            ammoPlus = maxAmmo - currentAmmo;
            Debug.Log("Municion a recargar: " + ammoPlus);
            
            //Chequeo para que no quede negativo el inventario
            if(inventoryManager.purpleAmmo < 0)
            {
                ammoPlus += inventoryManager.purpleAmmo;
                inventoryManager.purpleAmmo = 0;
            }

            //Arranco la corutina de recarga
            StartCoroutine(ReloadGun(ammoPlus));
        }
        else
        {
            Debug.Log("No hay suficiente municion para: " + gameObject.name);
            //FeedBack para el jugador de que no puede cambiar de armas
        }
    }
}
