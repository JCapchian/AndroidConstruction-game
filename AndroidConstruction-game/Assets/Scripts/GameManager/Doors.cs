using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{

    public string key;
    public GameObject llave;

    public void AbrirPuerta(string tipo)
    {
        if(key == llave.gameObject.name)
            Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
