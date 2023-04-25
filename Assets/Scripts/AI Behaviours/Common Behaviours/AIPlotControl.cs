using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AIPlotControl : ActionNode
{
    public string plotName;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (blackboard.plotName.Equals(plotName)) return State.Success;
        return State.Failure;
    }
}
