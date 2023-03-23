using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

public class PlayerAgent : MonoBehaviour
{
    public BehaviourTreeRunner treeRunner;
    public NavMeshAgent navAgent;
    public Animator animator;

    public Transform LeftHandIK;
    public Transform RightHandIK;
    public void Awake()
    {
        treeRunner.blackboard.destination = transform.position;
        if (!navAgent) navAgent = gameObject.GetComponent<NavMeshAgent>();
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
