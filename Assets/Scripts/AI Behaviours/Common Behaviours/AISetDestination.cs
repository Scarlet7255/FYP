using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
public class AISetDestination : ActionNode
{
    public Vector3 destination;
    protected override void OnStart()
    {
        blackboard.destination = destination;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
