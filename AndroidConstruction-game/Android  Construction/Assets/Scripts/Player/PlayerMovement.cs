using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal:<- ->, A y D
        //Vertical: Arriba y abajo
        
        float InputHorizontal = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Movimiento del objeto
        transform.position += (Vector3.right * InputHorizontal + Vector3.up * verticalInput) * speed;

    }
}