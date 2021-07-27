using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepOffsetWithCameraForward : MonoBehaviour
{
    [SerializeField] float offset = 10f;
    Transform cameraTransform;

    void Awake()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        transform.position = cameraTransform.position + cameraTransform.forward * offset;
    }
}
