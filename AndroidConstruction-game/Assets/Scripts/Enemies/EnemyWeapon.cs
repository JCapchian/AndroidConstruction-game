using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    //Type of proyectile
    public GameObject proyectilePrefab;
    public Transform cannon;

    //Propieties
    public AudioManager audioRef;
    private bool coolDown;
    private LookTarget LT;
    private float speed = 20f;
    private int damage;
    private float range;

    // Start is called before the first frame update
    void Start()
    {
        audioRef = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        coolDown = false;
        LT = gameObject.GetComponent<LookTarget>();
        LT.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
            LT.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
            LT.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && coolDown == false)
            StartCoroutine(AtaqueEnemigo());
    }

    IEnumerator AtaqueEnemigo()
    {
        audioRef.Play("DisparoEnemigo1");
        Instantiate(proyectilePrefab, cannon.position, cannon.rotation);


        coolDown = true;
        yield return new WaitForSeconds(5f);
        coolDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
