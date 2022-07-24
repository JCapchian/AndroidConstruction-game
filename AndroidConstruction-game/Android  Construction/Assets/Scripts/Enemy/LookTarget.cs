using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTarget : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        transform.up = target.position - transform.position;
    }
}
