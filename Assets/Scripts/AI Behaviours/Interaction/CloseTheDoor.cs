using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
public class CloseTheDoor : ActionNode
{
    protected override void OnStart()
    {
        InteractableDoor door = blackboard.interactionTarget as InteractableDoor;
        door.Close();
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
