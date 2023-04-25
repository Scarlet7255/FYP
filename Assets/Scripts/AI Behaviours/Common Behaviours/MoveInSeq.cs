using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TheKiwiCoder;
using UnityEngine;

public class MoveInSeq : ActionNode
{
    private List<Transform> checkPoints;
    public SequenceMode mode;
    private int idx = 0;
    private int dir = 1;
    public string area;
    public enum  SequenceMode
    {
        Once, LoopForward, LoopBackward,LoopInverse
    }
    
    protected override void OnStart()
    {
        checkPoints = MapManager.Instance.GetArea(area).checkPoints;
        blackboard.destination = checkPoints[idx].position;
        
        if (mode == SequenceMode.Once)
        {
            idx = Math.Min(idx, checkPoints.Count - 2);
            dir = 1;
        }
        else if (mode == SequenceMode.LoopForward)
        {
            idx = idx >= checkPoints.Count - 1 ? 0 : idx;
            dir = 1;
        }
        else if (mode == SequenceMode.LoopBackward)
        {
            idx = idx >0? idx : checkPoints.Count();
            dir = -1;
        }
        else if (mode == SequenceMode.LoopInverse)
        {
            if (dir > 0 && checkPoints.Count == idx)
            {
                idx = Math.Max(checkPoints.Count - 2,0);
                dir = -1;
            }
            else if (dir < 0 && idx < 0)
            {
                dir = 1;
                idx = Math.Min(1, checkPoints.Count - 1);
            }
        }
        idx += dir;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        return State.Success;
    }
}
