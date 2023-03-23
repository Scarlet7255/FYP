using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
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
    private float _currentSpeed;
    
    private Animator _animator;

    public MovementState currentState { get; private set; }

    private bool _onOffMeshLink = false;
    private Vector3 _offMeshLinkEnd;
    public bool forceMoving { get; private set; } = false;
    private Vector3 _target;
    
    #region Game Loop
    private void Start()
    {
        if (navAgent == null) navAgent = gameObject.GetComponent<NavMeshAgent>();
        _animator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case MovementState.Run:
            {
                _speed = RunVelocity;
                break;
            }
            case MovementState.Stand:
            {
                _speed = WalkVelocity;
                break;
            }
            case MovementState.Squat:
            {
                _speed = SquatVelocity;
                break;
            }
        }

        
        navAgent.speed = _speed;
        _currentSpeed = navAgent.velocity.magnitude;
        _animator.SetFloat("Speed",_currentSpeed);

        //if(_onOffMeshLink) MoveNormalSpeedOnOffMesh();
    }

    #endregion

    #region  Movement Action
    public void Move(Vector3 target)
    {
        navAgent.destination = target;
        _target = target;
    }


    public void SetState(MovementState state)
    {
        if (currentState == MovementState.Squat && state != MovementState.Squat)
        {
        }
        else
        {
            currentState = state;
            if (currentState != MovementState.Squat)
                navAgent.height = StandHeight;
            else 
                navAgent.height = SquatHeight;
        }
    }

    public void ResetPath()
    {
        StartCoroutine(ResetPathE());
    }

    public IEnumerator ResetPathE()
    {
        navAgent.ResetPath();
        navAgent.destination = transform.position;
        yield return new FixedUpdate();
        navAgent.destination = _target;
    }

    public void CompleteOffMeshLinkNormalSpeed(Vector3 target)
    {
        _onOffMeshLink = true;
        navAgent.ResetPath();
        navAgent.destination = target;
    }

    private void MoveNormalSpeedOnOffMesh()
    {
        navAgent.Move(Vector3.MoveTowards(transform.position, _offMeshLinkEnd, _speed * Time.fixedDeltaTime)
        - transform.position);
        transform.forward = Vector3.Lerp(transform.forward, (_offMeshLinkEnd - transform.position).normalized, 0.8f);
        if (transform.position == _offMeshLinkEnd)
        {
            _onOffMeshLink = false;
            navAgent.CompleteOffMeshLink();
        }
        _animator.SetFloat("Speed",_speed);
    }

    #endregion

    #region IK
/*
    public void OnAnimatorIK(int layerIndex)
    {
        _animator.SetIKPosition(AvatarIKGoal.LeftFoot,LeftFootIKTarget.position);
        _animator.SetIKPosition(AvatarIKGoal.RightFoot,RightFootIKTarget.position);
        
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,1f);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,1f);
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,1f);
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
    }
*/
    #endregion

}
