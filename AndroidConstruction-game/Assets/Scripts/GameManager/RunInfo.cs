using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunInfo : MonoBehaviour
{
    public int enemigosElimindos;
    public int chatarraRecoletada;
    public int componentesRecoletados;

    public TMP_Text textChatarra;
    public TMP_Text textComponente;


    // Start is called before the first frame update
    void Start()
    {
        enemigosElimindos = 0;
        chatarraRecoletada = 0;
        componentesRecoletados = 0;
    }

    public void EnemigoEliminado()
    {
        enemigosElimindos += 1;
        Debug.Log("Enemigos eliminados= " + enemigosElimindos);
    }

    public void ComponenteAgarrado(string Type)
    {
        switch (Type)
        {
            case "Chatarra":
                chatarraRecoletada += 1;
                Debug.Log("+1 Chatarra");
                textChatarra.text = ("X" + chatarraRecoletada.ToString());
                Debug.Log("Chatarra Obtenida= " + chatarraRecoletada);
                break;
            case "Componente":
                componentesRecoletados += 1;
                Debug.Log("+1 Componentes");
                textComponente.text = ("X" + componentesRecoletados.ToString());
                Debug.Log("Componente Obtenida= " + chatarraRecoletada);
                break;
        }
    }
}
