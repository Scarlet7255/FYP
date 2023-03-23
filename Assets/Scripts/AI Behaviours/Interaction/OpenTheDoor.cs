using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
public class OpenTheDoor : ActionNode
{
    protected override void OnStart()
    {
        InteractableDoor d = blackboard.interactionTarget as InteractableDoor;
        d.Open();
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
