using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckStrategy : ActionNode
{
    public AIStrategy Strategy;
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return Strategy == blackboard.strategy ? State.Success : State.Failure;
    }
}
