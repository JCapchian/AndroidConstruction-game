using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Script : MonoBehaviour
{
    [Header("Estadisticas")]
    [SerializeField]
    private float distanceRay = 100;
    public float maxChargeTime;
    private float chargeTime;
    public int damage;

    [Header("Propiedades")]
    public Transform laserCañon;
    public LineRenderer ref_LineRenderer;
    Transform ref_TransformGun;
    private bool charged = false;
    private bool firing = false;

    private void Awake() 
    {
        ref_TransformGun = GetComponent<Transform>();
    }

    private void Update() 
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            ref_LineRenderer.enabled = true;
            ShootLaser();
        }
        else
        {
            ref_LineRenderer.enabled = false;
            firing = false;
        }

        // Charge the chargeTime
        if(firing!)
        {
            if (chargeTime < maxChargeTime)
            {
                chargeTime += Time.deltaTime;
            }
        }
    }

    void ShootLaser()
    {
        //Starts the timer to charge it
        if(chargeTime > 0)
            chargeTime -= Time.deltaTime;
        else
        {
            charged = true;
            firing = true;
        }

        if(charged)
        {
            //"Physics2D.Raycast" cheks if the rate hash got somthing
            if (Physics2D.Raycast(ref_TransformGun.position, transform.right))
            {
                //Take the position of the canon and draw a laser
                RaycastHit2D _hit = Physics2D.Raycast(laserCañon.position, transform.right);
                Draw2DRay(laserCañon.position, _hit.point);
            }
            else
            {
                //Use the default distance variable to draw the laser
                Draw2DRay(laserCañon.position, laserCañon.transform.right * distanceRay); 
            }
        }
        
    }

    //This function does is setting the position of the two point in the line renderer
    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        ref_LineRenderer.SetPosition(0, startPos);
        ref_LineRenderer.SetPosition(1, endPos);
    }



}
