using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float rotationSpeed = 2f;
    float verticalVelocity = 0f;

    CharacterController controller;
    PlayerInput playerInput;
    Vector3 playerVelocity;
    bool groundedPlayer;
    Transform cameraTransform;
    Move move;

    InputAction jumpAction;

    Animator animator;
    int jumpAnimationParameterId;

    void Awake()
    {
        getDependencies();
        getInputActions();
        getHashAnimatorParameters();
        lockMouse();
    }

    void getDependencies()
    {
        controller = GetComponent<CharacterController>();
        move = GetComponent<Move>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
    }

    void getInputActions()
    {
        jumpAction = playerInput.actions["Jump"];
    }

    void getHashAnimatorParameters()
    {
        jumpAnimationParameterId = Animator.StringToHash("Jump");
    }

    void lockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && verticalVelocity < 0) verticalVelocity = 0f;
        playerVelocity = move.MovePlayer(playerVelocity);
        jump();
        applyGravity();
        rotateTowardsCamera();
    }
    
    void jump()
    {
        if (jumpAction.triggered && groundedPlayer)
        {
            animator.SetTrigger(jumpAnimationParameterId);
            verticalVelocity += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
    }

    void applyGravity()
    {
        verticalVelocity += gravityValue * Time.deltaTime;
        playerVelocity.y = verticalVelocity * Time.deltaTime;
        controller.Move(playerVelocity);
    }

    void rotateTowardsCamera()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
