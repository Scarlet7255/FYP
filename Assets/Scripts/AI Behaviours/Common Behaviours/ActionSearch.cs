



using UnityEngine;
using TheKiwiCoder;

public class ActionSearch : ActionNode
{
    protected override void OnStart()
    {

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if (!blackboard.seeTarget)
        {
            blackboard.destination =  MapManager.Instance.GetArea(blackboard.area)
                .GetPossibleSearchPoint(context.transform.position, blackboard.searchDirection, 20f,5f);
        }
        return State.Success;
    }
}
