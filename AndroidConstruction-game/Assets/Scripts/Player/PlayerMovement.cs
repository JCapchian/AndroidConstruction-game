using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    public Animator MyAnimator;

    // Update is called once per frame
    void FixedUpdate() {
        //Horizontal:<- ->, A y D
        //Vertical: Arriba y abajo
        
        float InputHorizontal = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Movimiento del objeto
        transform.position += (Vector3.right * InputHorizontal + Vector3.up * verticalInput) * speed;

        MyAnimator.SetFloat("Movimiento", Mathf.Abs(InputHorizontal) + Mathf.Abs(verticalInput));
    }
}