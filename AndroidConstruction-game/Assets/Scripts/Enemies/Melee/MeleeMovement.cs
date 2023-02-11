using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMovement : MonoBehaviour
{
    DetectionZone detectionZone;
    [SerializeField]
    MeleeAnimations meleeAnimations;

    [SerializeField]
    Rigidbody2D rb2D;
    [SerializeField]
    float speed;
    [SerializeField]
    public bool isIdle;
    [SerializeField]
    public bool isMoving;

    //Transform playerPosition;

    private void Awake()
    {
        meleeAnimations = GetComponent<MeleeAnimations>();
        detectionZone = GetComponentInChildren<DetectionZone>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void HandleMovement()
    {
        NormalMovement();
    }

    private void NormalMovement()
    {
        if(detectionZone.seePlayer)
        {
            var targetPosition = detectionZone.playerStats.transform.position;
            if(Vector2.Distance(transform.position , targetPosition) > 3)
            {
                isIdle = false;
                isMoving = true;

                Vector3 temp = Vector3.MoveTowards(transform.position, detectionZone.playerStats.transform.position, speed * Time.deltaTime);
                rb2D.MovePosition(temp);
            }
        }
        else
        {
            isIdle = true;
            isMoving = false;
        }
    }
}
