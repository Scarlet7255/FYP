using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraControl : MonoBehaviour
{
   public Transform follow;
   public float camRotateSpeed;
   [SerializeReference]private Camera _camera;
   
   private Vector3 _targetPosLastFrame;
   private Vector3 _deltaMove = Vector3.zero;
   
   private bool _rotating = false;
   private float _rotateAngle = 0f;
   private float _currentAngle = 0f;
   private float _rotateDirection;
   public bool fllowYaxis = false;

   [SerializeReference] private float maximumViewScale = 10.0f;
   [SerializeReference] private float viewScaleSpeed = 10.0f;
   private float _currentViewScale = 0.0f;

   public void RotateView(InputAction.CallbackContext cbc)
   {
      if (_rotating) return;
      _rotating = true;
      _rotateDirection = cbc.ReadValue<float>();
      _currentAngle = 0.0f;
      _rotateAngle = _rotateDirection * 45.0f;
   }

   public void ScaleView(InputAction.CallbackContext cbc)
   {
      if (!cbc.performed) return; 
      
      float dir = cbc.ReadValue<Vector2>().y;
      if (dir > 0.0f)
      {
         float deltaScale;
         Common.UniformInterpolationInValue(maximumViewScale, _currentViewScale,
            viewScaleSpeed*dir, out deltaScale);
         _camera.transform.position += _camera.transform.forward * deltaScale;
         _currentViewScale += deltaScale;
      }
      else if (dir < 0.0f)
      {
         float deltaScale;
         Common.UniformInterpolationInValue(0, _currentViewScale,
            viewScaleSpeed*dir, out deltaScale);
         _camera.transform.position += _camera.transform.forward * deltaScale;
         _currentViewScale += deltaScale;
      }
      
   }

   private void Start()
   {
      _targetPosLastFrame = follow.position;
   }

   private void FixedUpdate()
   {
      if (_rotating)
      {
         float deltaRotate;
         _rotating =!Common.UniformInterpolationInValue(_rotateAngle, _currentAngle,
            _rotateDirection*camRotateSpeed * Time.fixedDeltaTime, out deltaRotate);
         _currentAngle += deltaRotate;
         transform.Rotate(Vector3.up,deltaRotate);
      }
   }

   private void LateUpdate()
   {
      // follow the target
      Vector3 movement = follow.position - _targetPosLastFrame;
      _deltaMove.x = movement.x;
      _deltaMove.z = movement.z;
      if (fllowYaxis) _deltaMove.y = movement.y;
      transform.position += _deltaMove;
      _targetPosLastFrame = follow.position;
   }
}
