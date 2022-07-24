using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCont : MonoBehaviour
{
    static public bool redKey;
    static public bool blueKey;
    static public bool greenKey;

    static public int cantidadLLaves;

    

    public void KeyPickUp(string colorKey)
    {
        switch (colorKey)
        {
            case "GreenKey":
                greenKey = true;
                cantidadLLaves += 1;
                break;

            case "BlueKey":
                blueKey = true;
                cantidadLLaves += 1;
                break;

            case "RedKey":
                redKey = true;
                cantidadLLaves += 1;
                break;
        }
    }
}
