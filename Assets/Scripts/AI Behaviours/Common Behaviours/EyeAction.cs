using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class EyeAction : ActionNode
{
    public string target;
    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        ViewDetector eye = blackboard.eye;
        if (eye.Query(target)) return State.Success;
        return State.Failure;
    }
}
