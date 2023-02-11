using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTarget : MonoBehaviour
{
    public Transform target;

    public void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        transform.up = target.position - transform.position;
    }
}
