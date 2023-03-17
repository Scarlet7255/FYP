using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public enum MovementState
{
    Stand, Squat, Run
}

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class MovementControl : MonoBehaviour
{
    // AI agents
    [SerializeField] private NavMeshAgent navAgent;
    
    // control movement
    [SerializeField]private float WalkVelocity = 1.0f;
    [SerializeField]private float RunVelocity = 3f;
    [SerializeField]private float SquatVelocity = 0.8f;

    // Collider Height Control
    [SerializeField] private float StandHeight;
    [SerializeField] private float SquatHeight;
    [SerializeField] private float CrouchHeight;
    
    
    [SerializeField]private Transform LeftFootIKTarget;
    [SerializeField]private Transform RightFootIKTarget;
    [SerializeField]private Transform LeftHandIKTarget;
    [SerializeField]private Transform RightHandIKTarget;

    private float _speed;

    private Animator _animator;

    private bool _squat = false;
    private bool _rush = false;

    private bool _onOffMeshLink = false;
    private Vector3 _offMeshLinkEnd;
    
    #region Game Loop
    private void Start()
    {
        if (navAgent == null) navAgent = gameObject.GetComponent<NavMeshAgent>();
        _animator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!_squat)
        {
            if (!_rush) 
                _speed = WalkVelocity;
            else 
                _speed = RunVelocity;
        } 
        else if (_squat) 
            _speed = SquatVelocity;
        
        navAgent.speed = _speed;
        _animator.SetFloat("Speed",navAgent.velocity.magnitude);
        //transform.forward = Vector3.Lerp(transform.forward, navAgent.velocity.normalized,RotateSpeed*Time.fixedDeltaTime);
        if (navAgent.velocity.magnitude < 0.1f) _rush = false;
        
        if(_onOffMeshLink) MoveNormalSpeedOnOffMesh();
    }

    #endregion

    #region  Movement Action
    public void Move(Vector3 target)
    {
        navAgent.destination = target;
    }

    public void Rush(bool isRush)
    {
        _rush = isRush;
    }

    
    public void Squat()
    {
        _squat = !_squat;

        _animator.SetBool("Squat",_squat);
        
        if (_squat) 
            navAgent.height = SquatHeight;
        else
            navAgent.height = StandHeight;
        
    }

    public void CompleteOffMeshLinkNormalSpeed(Vector3 target)
    {
        _onOffMeshLink = true;
        _offMeshLinkEnd = target;
    }

    private void MoveNormalSpeedOnOffMesh()
    {
        navAgent.Move(Vector3.MoveTowards(transform.position, _offMeshLinkEnd, _speed * Time.fixedDeltaTime)
        - transform.position);
        //transform.forward = Vector3.Lerp(transform.forward, (_offMeshLinkEnd - transform.position).normalized, 0.8f);
        if (transform.position == _offMeshLinkEnd)
        {
            _onOffMeshLink = false;
            navAgent.CompleteOffMeshLink();
        }
        _animator.SetFloat("Speed",_speed);
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
