using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(CharacterController))]

public class MainController : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float runMultiplier = 1.25f;
    public float jumpSpeed = 7.0f;
    public float gravity = 23.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    public Camera playerCamera;
    CharacterController characterController;
    public Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public TextMeshProUGUI velocity;
    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        if (velocity != null)
        {
            velocity.SetText("Player velocity : " + CalculVelocity());
        }
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? (walkSpeed * runMultiplier) : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? (walkSpeed * runMultiplier) : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButtonDown("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    private float CalculVelocity()
    {
        Vector3 horizontalVelocity = characterController.velocity;
        horizontalVelocity = new Vector3(characterController.velocity.x, 0, characterController.velocity.z);

        // The speed on the x-z plane ignoring any speed
        float horizontalSpeed = horizontalVelocity.magnitude;
        // The speed from gravity or jumping
        float verticalSpeed = characterController.velocity.y;
        // The overall speed
        float overallSpeed = characterController.velocity.magnitude;

        return ((float)Math.Round(overallSpeed * 100f) / 100f);
    }
}
