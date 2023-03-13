using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementControl))]
public class PlayerInputHandler : MonoBehaviour
{
    public float mouseSensitivity = 1.0f;
    
    // camera control properity
    public Camera viewCamera;
    public float _cameraAngleX = 0.0f;
    public float cameraAngleML = 15.1f;
    public float cameraAngleLL = -20.1f;
    private MovementControl _controller;

    private void Start()
    {
        _controller = gameObject.GetComponent<MovementControl>();
    }
    

    public void Rotate(InputAction.CallbackContext callbackContext)
    {
        Vector2 angle = callbackContext.ReadValue<Vector2>();
        _controller.Rotate(angle.x*mouseSensitivity);
        
        float deltaAngle = angle.y * mouseSensitivity * -1.0f;
        if(_cameraAngleX + deltaAngle<cameraAngleML&&_cameraAngleX + deltaAngle>cameraAngleLL)
        {
            viewCamera.transform.Rotate(angle.y * mouseSensitivity * -1.0f, 0.0f, 0.0f);
            _cameraAngleX += deltaAngle;
        }
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
