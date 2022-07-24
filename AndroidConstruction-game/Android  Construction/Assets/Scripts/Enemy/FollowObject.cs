using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    public float movementSpeed;
    Vector3 auxVector;
    // Update is called once per frame
    void Update()
    {
        auxVector = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
        auxVector.z = transform.position.z;
        transform.position = auxVector;
    }
}
