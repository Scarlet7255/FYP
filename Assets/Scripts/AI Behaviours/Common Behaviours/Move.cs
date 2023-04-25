using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

public class Move : ActionNode
{
    protected NavMeshAgent _navMeshAgent;
    protected NavMeshPath _path;
    protected override void OnStart()
    {
        _navMeshAgent = context.agent;
        _navMeshAgent.autoRepath = false;
        _path = new NavMeshPath();
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (blackboard.runningAbortFlag)
        {
            blackboard.runningAbortFlag = false;
            _navMeshAgent.ResetPath();
            return State.Failure;
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
