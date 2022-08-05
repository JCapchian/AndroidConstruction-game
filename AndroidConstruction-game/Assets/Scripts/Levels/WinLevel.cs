using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinLevel : MonoBehaviour
{

    public GameObject keyImage;
    public Pausa m_levelLoader;
    public bool entro = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && entro == true)
        {
            Debug.Log("Le doy a la E");
            m_levelLoader.ShowWinScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            keyImage.active = true;
            entro = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            keyImage.active = false;
            
        }
    }


}
