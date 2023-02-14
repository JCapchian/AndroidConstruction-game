using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float damping;

    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        target = FindObjectOfType<PlayerManager>().gameObject.transform;

        DontDestroyOnLoad(this.gameObject);
    }

    public void HandleCamera()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        Vector3 movePosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
    }
}
