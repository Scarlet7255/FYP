using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

public class AIDoorDetector : ActionNode
{
    private NavMeshAgent _agent;
    protected override void OnStart()
    {
        _agent = context.agent;
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        if (_agent.isOnOffMeshLink)
        {
            OffMeshLink link = _agent.currentOffMeshLinkData.offMeshLink;
            if (link.tag.Equals("Door Entry"))
            {
                blackboard.interactionTarget = link.GetComponent<OffMeshTrigger>().trigTarget;
                return State.Success;
            }
        }
        return State.Running;
    }
}
