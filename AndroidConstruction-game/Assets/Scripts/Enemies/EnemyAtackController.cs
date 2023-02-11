using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAtackController : MonoBehaviour
{
    public StatsPlayer SP;
    public int Damage;
    public bool coolDown;
    // Start is called before the first frame update
    void Start()
    {
        coolDown = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player" && coolDown == false)
        {
            Debug.Log("Entro en el area de daño");
            SP = other.gameObject.GetComponent<StatsPlayer>();
            StartCoroutine(Ataque());
        }
    }

    public IEnumerator Ataque()
    {
        Debug.Log("Empezo la corutina");
        coolDown = true;


        SP.PerderVida(10);
        FindObjectOfType<AudioManager>().Play("AtaqueArea");
        yield return new WaitForSeconds(3f);

        coolDown = false;
        Debug.Log("Termino la corutina");
    }
}
