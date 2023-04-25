
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
public class AISetStrategy : ActionNode
{
    public AIStrategy Strategy;

    protected override void OnStart()
    {
        blackboard.strategy = Strategy;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
