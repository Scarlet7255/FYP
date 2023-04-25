using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
public class AIRandomMove : ActionNode
{
    public string area;
    public float maxDistance;
    protected override void OnStart()
    {
       blackboard.destination = MapManager.Instance.GetArea(area).GetRandomNearbyPoint(context.transform.position, 30f);
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
