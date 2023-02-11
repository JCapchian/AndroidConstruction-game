using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    [SerializeField]
    MeleeMovement meleeMovement;
    public StatsManager playerStats;
    public bool seePlayer = false;

    private void Awake()
    {
        meleeMovement = GetComponentInParent<MeleeMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<StatsManager>())
        {
            playerStats = other.GetComponent<StatsManager>();
            seePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<StatsManager>())
        {
            playerStats = null;
            seePlayer = false;
        }
    }
}
