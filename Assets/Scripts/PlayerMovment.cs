using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float forceMagnitude; // how much force we add to the rigidbody
    [SerializeField] private float maximumVelocity; // the maximum velocity of the rigidbody
    [SerializeField] private float rotationSpeed; // spaceship rotation speed
    
    private Camera mainCamera;
    private Rigidbody rigidBody;

    private Vector3 movementDirection; // how much to move every frame

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        ProcessInput();
        KeepPlayerOnScreen();
        RotateToFaceVelocity();
    }

    private void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue(); // reading the touch position in PIXELS
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition); // converting the touch position into UNITS
            movementDirection = transform.position - worldPosition; // the verctor between the player & wheew we touched
            movementDirection.z = 0f;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (movementDirection == Vector3.zero)
        {
            return;
        }
        else
        {
            rigidBody.AddForce(forceMagnitude * Time.deltaTime * movementDirection, ForceMode.Force);

            rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maximumVelocity);
        }
    }
    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewportPosition =  mainCamera.WorldToViewportPoint(transform.position); // converting the spaceship location to viewport   

        if (viewportPosition.x > 1) // we are out of the right side of the screen
        {
            newPosition.x = -newPosition.x + 0.1f;
        } 
        else if (viewportPosition.x < 0)  // we are out of the left side of the screen
        {
            newPosition.x = -newPosition.x - 0.1f;
        }
        else if (viewportPosition.y > 1) // we are out of the top side of the screen
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        else if (viewportPosition.y < 0) // we are out of the bottom side of the screen
        {
            newPosition.y = -newPosition.y - 0.1f;
        }
        transform.position = newPosition;
    }

    private void RotateToFaceVelocity()
    {
        if (rigidBody.velocity == Vector3.zero)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(rigidBody.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

}

