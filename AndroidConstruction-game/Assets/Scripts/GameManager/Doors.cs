using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public void AbrirPuerta()
    {
        Destroy(this.gameObject);
    }
}
