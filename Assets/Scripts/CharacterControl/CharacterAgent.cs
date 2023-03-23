
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

    public InteractableObject interactable
    {
        set => treeRunner.blackboard.interactionTarget = value;
    }

    public Transform LeftHandIK;
    public Transform RightHandIK;

    public CharacterState CurrentState
    {
        get => treeRunner.blackboard.state;
        set => treeRunner.blackboard.state = value;
    }

    public void Awake()
    {
        treeRunner.blackboard.destination = transform.position;
        if (!navAgent) navAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (!treeRunner) treeRunner = gameObject.GetComponent<BehaviourTreeRunner>();
        if (!animator) animator = gameObject.GetComponent<Animator>();
    }

    public void Move(Vector3 des)
    {
        treeRunner.blackboard.destination = des;
    }

    private void Update()
    {
        animator.SetFloat("Speed",navAgent.velocity.magnitude);
    }
}
