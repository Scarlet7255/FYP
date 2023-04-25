using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

public class Follow : ActionNode
{
    public float stopDistance;
    public float chasingDistance;
    public bool followPlayer = false;
    private NavMeshAgent _agent;
    private Transform _transform;
    protected override void OnStart()
    {
        _agent = context.agent;
        _agent.stoppingDistance = stopDistance;
        _transform = context.transform;
        if (followPlayer)
        {
            blackboard.target = GameManger.Instance.Player.transform;
        }
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        _transform.LookAt(blackboard.target);
        float dis = (_transform.position - blackboard.target.position).magnitude;
        if (_agent.velocity.magnitude<0.1f && dis >= chasingDistance)
        {
            _agent.destination = blackboard.target.position;
        }
        else if (_agent.velocity.magnitude>1f && dis >= stopDistance)
        {
            _agent.destination = blackboard.target.position;
        }

        return State.Success;
    }
}
