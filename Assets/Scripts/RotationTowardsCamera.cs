using UnityEngine;

public class RotationTowardsCamera : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 200f;
    Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        rotateTowardsCamera();
    }

    void rotateTowardsCamera()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, cameraTransform.eulerAngles.y, 0f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
