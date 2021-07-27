using UnityEngine;
using UnityEngine.InputSystem;

public class VerticalMovement : MonoBehaviour
{
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float jumpHeight = 1f;

    float verticalVelocity = 0f;
    bool groundedPlayer = false;
    int jumpAnimationParameterId;

    CharacterController controller;
    PlayerInput playerInput;
    Animator animator;
    InputAction jumpAction;

    void Awake()
    {
        getDependencies();
        getInputAction();
        getgetHashAnimationParameter();
    }

    void getDependencies()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    void getInputAction()
    {
        jumpAction = playerInput.actions["Jump"];
    }

    void getgetHashAnimationParameter()
    {
        jumpAnimationParameterId = Animator.StringToHash("Jump");
    }

    public float GetVerticalVelocity()
    {
        groundedPlayer = controller.isGrounded;
        jump();
        applyGravity();
        return verticalVelocity * Time.deltaTime;
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
        bool groundedPlayer = controller.isGrounded;
        if (groundedPlayer && verticalVelocity < 0) verticalVelocity = 0f;
        verticalVelocity += gravityValue * Time.deltaTime;

    }
}