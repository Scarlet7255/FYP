using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
public class CloseTheDoor : ActionNode
{
    protected override void OnStart()
    {
        InteractableDoor d = blackboard.interactionTarget as InteractableDoor;
        d.Close();
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
