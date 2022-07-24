using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Repuestos : MonoBehaviour, ICollectible
{
    public static event Action OnRepuestosCollected;
    Rigidbody2D rb;

    bool hasTarget;
    Vector3 targetPosition;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Collect()
    {
        Debug.Log("Collected repuestos");
        OnRepuestosCollected?.Invoke();
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        if(hasTarget)
        {
            Vector2 targetDirecction =(targetPosition - transform.position).normalized;
            rb.velocity = new Vector2(targetDirecction.x, targetDirecction.y) * 5f;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}
