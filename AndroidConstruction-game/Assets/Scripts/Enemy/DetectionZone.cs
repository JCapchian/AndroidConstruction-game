using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public FollowObject enemyMovement;

    public bool detected = false;



    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            detected = true;
            enemyMovement.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            detected = false;
            enemyMovement.enabled = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponentInParent<FollowObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
