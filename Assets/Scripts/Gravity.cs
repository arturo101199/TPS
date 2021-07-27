using UnityEngine;

public class Gravity : MonoBehaviour
{
    CharacterController controller;
    [SerializeField] float gravityValue = -9.81f;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public Vector3 ApplyGravity(Vector3 playerVelocity)
    {
        bool groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) playerVelocity.y = 0f;
        playerVelocity.y += gravityValue * Time.deltaTime;
        return playerVelocity;
    }
}