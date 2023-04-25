using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

public class ActionChase : ActionNode
{
    private NavMeshAgent _navMeshAgent;
    private NavMeshPath _path;
    
    public float speed;
    
    protected override void OnStart()
    {
        _navMeshAgent = context.agent;
        _navMeshAgent.autoRepath = false;
        _path = new NavMeshPath();
        context.agent.speed = speed;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        bool see = blackboard.seeTarget;
        if (see)
        {
            var targetPos = blackboard.target.position;
            _navMeshAgent.destination = targetPos;
            blackboard.destination = _navMeshAgent.destination;
            blackboard.searchDirection = targetPos - context.transform.position;
            return State.Running;
        }

        _navMeshAgent.CalculatePath(blackboard.destination,_path);
        if(_navMeshAgent.isPathStale) Debug.Log("Path stale");
        if (_navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            _navMeshAgent.path = _path;
        }
        else
        {
            _navMeshAgent.ResetPath();
            blackboard.destination = context.transform.position;
            Debug.LogFormat("{0}: I can not get there",context.gameObject.name);
        }
        
        return _navMeshAgent.hasPath? State.Running:State.Success;
    }
}
