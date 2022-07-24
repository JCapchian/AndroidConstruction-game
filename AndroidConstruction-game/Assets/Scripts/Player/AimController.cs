using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{

    [SerializeField]
    Transform target;
    [SerializeField]
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        if(mainCamera == null)
            mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //Obtain the position of the mouse in the "world"
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;
        
        //Save the position where to look
        Vector3 lookAtDirection = mouseWorldPosition - target.position;
        lookAtDirection.z = 0;
        target.right = lookAtDirection;
    }

}
