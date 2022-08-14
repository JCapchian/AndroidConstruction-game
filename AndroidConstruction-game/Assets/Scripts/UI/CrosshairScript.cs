using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake() 
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = mousePosition;
    }
}
