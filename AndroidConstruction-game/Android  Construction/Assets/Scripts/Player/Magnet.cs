using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.TryGetComponent<Repuestos>(out Repuestos repuestos))
        {
            repuestos.SetTarget(transform.parent.position);
        }
    }
}
