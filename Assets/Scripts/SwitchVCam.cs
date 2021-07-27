using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] int priorityBoostAmount = 10;
    [SerializeField] Canvas thirdPersonCanvas;
    [SerializeField] Canvas aimCanvas;

    CinemachineVirtualCamera vCam;
    InputAction aimAction;

    void Awake()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
    }

    void OnEnable()
    {
        aimAction.performed += _ => startAim();
        aimAction.canceled += _ => cancelAim();
    }

    void OnDisable()
    {
        aimAction.performed -= _ => startAim();
        aimAction.canceled -= _ => cancelAim();
    }

    void startAim()
    {
        vCam.Priority += priorityBoostAmount;
        aimCanvas.enabled = true;
        thirdPersonCanvas.enabled = false;
    }

    void cancelAim()
    {
        vCam.Priority -= priorityBoostAmount;
        aimCanvas.enabled = false;
        thirdPersonCanvas.enabled = true;
    }
}
