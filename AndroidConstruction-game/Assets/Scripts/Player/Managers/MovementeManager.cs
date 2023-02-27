using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementeManager : MonoBehaviour
{
    //Managers
    InputManager inputManager;
    AnimationManager animationmanager;

    // Componets
    Rigidbody2D rb2D;
    [SerializeField]
    private float movementSpeed;
    public bool isMoving;

    private void Awake() {
        // Managers
        inputManager = GetComponent<InputManager>();
        animationmanager = GetComponent<AnimationManager>();

        // Componets
        rb2D = GetComponent<Rigidbody2D>();
    }

    public void HandleMovement()
    {
        BasicMovement();
    }

    private void BasicMovement()
    {
        var movementOffset = inputManager.movementInput * movementSpeed * Time.fixedDeltaTime;
        var newPosition = rb2D.position + movementOffset;

        rb2D.MovePosition(newPosition);

        if(inputManager.movementInput.magnitude != 0)
            isMoving = true;
        else
            isMoving = false;

        //animationmanager.MoveAnimation(movementOffset);
    }

    public void TurnMovement(bool state)
    {
        if(state)
            rb2D.bodyType = RigidbodyType2D.Dynamic;
        else
            rb2D.bodyType = RigidbodyType2D.Static;
    }
}
