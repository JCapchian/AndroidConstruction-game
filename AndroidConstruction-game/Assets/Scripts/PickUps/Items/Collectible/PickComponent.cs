using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickComponent : MonoBehaviour
{

    public RunInfo contadorRun;
    private string[] nombrePartes;
    private string nombreEnviar;

    // Start is called before the first frame update
    void Start()
    {
        contadorRun = GameObject.Find("GameManager").GetComponent<RunInfo>();
    }

    void Awake()
    {
        contadorRun = GameObject.Find("GameManager").GetComponent<RunInfo>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            nombreEnviar = gameObject.name;
            Debug.Log(nombreEnviar);

            if(nombreEnviar.Contains("(Clone)"))
                nombrePartes = gameObject.name.Split('(');
            else
                nombrePartes = gameObject.name.Split(' ');

            nombreEnviar = nombrePartes[0];
            Debug.Log(nombreEnviar);

            contadorRun.ComponenteAgarrado(nombreEnviar);
            FindObjectOfType<AudioManager>().Play("PickUpItem");

            Destroy(gameObject);
        }
    }
}
