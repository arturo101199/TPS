using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    Vector3 playerVelocity;

    CharacterController controller;
    HorizontalMovement horizontalMovement;
    VerticalMovement verticalMovement;

    void Awake()
    {
        getDependencies();
        lockMouse();
    }

    void getDependencies()
    {
        controller = GetComponent<CharacterController>();
        horizontalMovement = GetComponent<HorizontalMovement>();
        verticalMovement = GetComponent<VerticalMovement>();
    }

    void lockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        playerVelocity = horizontalMovement.GetHorizontalMovement();
        playerVelocity.y = verticalMovement.GetVerticalVelocity();
        controller.Move(playerVelocity);
    }
}
