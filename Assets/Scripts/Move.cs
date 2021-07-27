using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    [SerializeField] float animationSmoothTime = 0.1f;
    [SerializeField] float playerSpeed = 2f;

    Vector2 currentAnimationBlendVector;
    Vector2 animationVelocity;    
    int moveXAnimationParameterId;
    int moveZAnimationParameterId;

    CharacterController controller;
    Transform cameraTransform;
    InputAction moveAction;
    Animator animator;
    PlayerInput playerInput;

    void Awake()
    {
        getDependencies();
        getInputAction();
        getHashAnimationParameters();
    }

    void getDependencies()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    void getInputAction()
    {
        moveAction = playerInput.actions["Move"];
    }

    void getHashAnimationParameters()
    {
        moveXAnimationParameterId = Animator.StringToHash("MoveX");
        moveZAnimationParameterId = Animator.StringToHash("MoveZ");
    }

    public Vector3 MovePlayer(Vector3 playerVelocity)
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = getSmoothMovementVector(input);
        move = alignMovementWithCamera(move);
        setAnimationParameters();
        Vector3 newPlayerVelocity = move * playerSpeed * Time.deltaTime;
        newPlayerVelocity.y = playerVelocity.y;
        return newPlayerVelocity;
    }

    Vector3 alignMovementWithCamera(Vector3 move)
    {
        move = move.x * cameraTransform.right + move.z * cameraTransform.forward;
        move.y = 0f;
        return move;
    }

    Vector3 getSmoothMovementVector(Vector2 input)
    {
        currentAnimationBlendVector = Vector2.SmoothDamp(currentAnimationBlendVector, input, ref animationVelocity, animationSmoothTime);
        Vector3 move = new Vector3(currentAnimationBlendVector.x, 0, currentAnimationBlendVector.y);
        return move;
    }

    void setAnimationParameters()
    {
        animator.SetFloat(moveXAnimationParameterId, currentAnimationBlendVector.x);
        animator.SetFloat(moveZAnimationParameterId, currentAnimationBlendVector.y);
    }
}
