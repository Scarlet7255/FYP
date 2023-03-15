using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementControl))]
public class FPlayerInputHandler : MonoBehaviour
{
    public float mouseSensitivity = 1.0f;
    
    // camera control properity
    private MovementControl _controller;

    private void Start()
    {
        _controller = gameObject.GetComponent<MovementControl>();
    }
    

    public void Rotate(InputAction.CallbackContext callbackContext)
    {
        Vector2 angle = callbackContext.ReadValue<Vector2>();
        _controller.Rotate(angle.x*mouseSensitivity);
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        _controller.Move(callbackContext.ReadValue<Vector2>());
    }

    public void Rush(InputAction.CallbackContext callbackContext)
    {
        _controller.Rush(!callbackContext.canceled);
    }

    public void Squat(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.started) _controller.Squat();
    }
}
