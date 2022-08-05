using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToPoint : MonoBehaviour
{

    public GameObject player;
    public bool used = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() 
    {
        player = FindObjectOfType<StatsPlayer>().gameObject;  
        Teleport();
    }
    
    private void Teleport()
    {
        player.transform.position = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
