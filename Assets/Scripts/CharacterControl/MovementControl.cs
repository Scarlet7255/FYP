using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class MovementControl : MonoBehaviour
{
    // basic movement attributes
    [SerializeReference]private CharacterController Controller;
    [SerializeField]private float Gravity = -9.81f;
    [SerializeField]private float WalkVelocity = 1.0f;
    [SerializeField]private float RunVelocity = 3f;
    [SerializeField]private float SquatVelocity = 0.8f;

    // Collider Height Control
    [SerializeField] private float StandHeight;
    [SerializeField] private float SquatHeight;
    [SerializeField] private float CrouchHeight;
    // record controller collider height and radius basic info 
    private float _controlCenterY;
    
    // control collider height when squat
    [SerializeField]private float _squatY;

    // collide detect for actions
    [SerializeField] private Transform headDetector;
    
    [SerializeField]private Transform LeftFootIKTarget;
    [SerializeField]private Transform RightFootIKTarget;
    [SerializeField]private Transform LeftHandIKTarget;
    [SerializeField]private Transform RightHandIKTarget;
    
    private Vector3 _inputDirection;
    private Vector3 _velocity;
    private float _yVelocity;

    private Animator _animator;

    private bool _moving = false;
    private bool _squat = false;
    private bool _rush = false;
    
    #region Game Loop
    private void Start()
    {
        if (Controller == null) Controller = gameObject.GetComponent<CharacterController>();
        _animator = gameObject.GetComponent<Animator>();
        _controlCenterY = Controller.center.y;
    }

    private void FixedUpdate()
    {

        _velocity = transform.forward * _inputDirection.y - transform.right * _inputDirection.x;
        // walk
        if (_moving && !_squat)
        {
            if (!_rush) 
                _velocity *= WalkVelocity;
            else 
                _velocity *= RunVelocity;
            _animator.SetFloat("Speed",_velocity.magnitude);
        } 
        else if (_squat)
        {
            if(_moving)
                _velocity *= SquatVelocity;
            _animator.SetFloat("Speed",_velocity.magnitude);
        }

        // Gravity Effect
        if (Controller.isGrounded)
        {
            _yVelocity = 0f;
        }
        else
            _yVelocity += Gravity;
        _velocity.y = _yVelocity;
        Controller.Move(_velocity*Time.fixedDeltaTime);
    }

    #endregion

    #region  Movement Action

    public void Rotate(float degreeX)
    {
        transform.Rotate(transform.up,degreeX);
    }

    public void Move(Vector2 direction)
    {
        _moving = true;
        _inputDirection = direction;
    }

    public void Rush(bool isRush)
    {
        _rush = isRush;
    }

    
    public void Squat()
    {
        if (!_squat) _squat = true;
        else
        {
            RaycastHit hitInfo;
            //Physics.Raycast()
        }

        _animator.SetBool("Squat",_squat);
        if (_squat)
        {
            Vector3 center = Controller.center;
            center.y = _squatY;
            Controller.center = center;
            Controller.height = SquatHeight;
        }
        else
        {
            Vector3 center = Controller.center;
            center.y = _controlCenterY;
            Controller.center = center;
            Controller.height = StandHeight;
        }
    }


    #endregion

    #region IK

    public void OnAnimatorIK(int layerIndex)
    {
        _animator.SetIKPosition(AvatarIKGoal.LeftFoot,LeftFootIKTarget.position);
        _animator.SetIKPosition(AvatarIKGoal.RightFoot,RightFootIKTarget.position);
        
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,1f);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,1f);
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1f);
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
    }

    #endregion

}
