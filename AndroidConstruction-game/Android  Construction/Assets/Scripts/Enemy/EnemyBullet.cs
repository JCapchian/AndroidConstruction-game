using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Rigidbody2D RB;
    public StatsPlayer SP;
    public float speed;
    public int damage;


    private void FixedUpdate() {
        RB.MovePosition(transform.position + transform.up * speed * Time.fixedDeltaTime);
    }

    private void Awake() {
        Debug.Log("Aparecio");
        //Destroy(gameObject, 3f);
    }
    

    // Update is called once per frame
    private void Update()
    {
        //Change the direction of the bullet
        
        //transform.position += shootDir * speed * Time.deltaTime;

        //transform.rotation = PickUpController.weaponEquiped.transform.rotation;
    }
}
