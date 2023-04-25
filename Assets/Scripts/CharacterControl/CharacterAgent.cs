
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

public enum CharacterState
{
    Walk, Run, Hide
}

public class CharacterAgent : MonoBehaviour
{
    public BehaviourTreeRunner treeRunner;
    public NavMeshAgent navAgent;
    public Animator animator;
    public AutoDoorDetector doorDetector;
    
    public InteractableObject interactable
    {
        set => treeRunner.blackboard.interactionTarget = value;
    }

    public Transform LeftHandIK;
    public Transform RightHandIK;

    public CharacterState iniState;

    public virtual CharacterState CurrentState
    {
        get => treeRunner.blackboard.state;
        set => treeRunner.blackboard.state = value;
    }

    protected virtual void Awake()
    {
        treeRunner.blackboard.destination = transform.position;
        if (!navAgent) navAgent = gameObject.GetComponent<NavMeshAgent>();
        if (!treeRunner) treeRunner = gameObject.GetComponent<BehaviourTreeRunner>();
        if (!animator) animator = gameObject.GetComponent<Animator>();
        CurrentState = iniState; 
    }

    public void Move(Vector3 des)
    {
        treeRunner.enabled = true;
        treeRunner.blackboard.destination = des;
    }

    public void AbortCurrentAction()
    {
        treeRunner.blackboard.runningAbortFlag = true;
    }

    protected virtual void Update()
    {
        animator.SetFloat("Speed",navAgent.velocity.magnitude);
    }
}
