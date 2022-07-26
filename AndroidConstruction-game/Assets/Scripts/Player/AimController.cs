using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class AimController : MonoBehaviour
{

    [SerializeField]
    Transform targetCamera;
    [SerializeField]
    Camera mainCamera;

    private void Awake() {
        // Aim decalrations
        mainCamera = Camera.main;
        targetCamera = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
            //Obtain the position of the mouse in the "world"
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            
            mouseWorldPosition.z = 0;
            
            //Save the position where to look
            Vector3 lookAtDirection = mouseWorldPosition - targetCamera.position;
            lookAtDirection.z = 0;
            targetCamera.right = lookAtDirection;
    }

}
