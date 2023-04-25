using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class DoorDetect : ActionNode
{
    private AutoDoorDetector _detector;
    protected override void OnStart()
    {
        _detector = context.mainAgent.doorDetector;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
