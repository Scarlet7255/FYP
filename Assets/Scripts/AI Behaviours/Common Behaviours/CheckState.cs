
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
public class CheckState : ActionNode
{
    public CharacterState CharacterStatestate;
    public State equalState;
    public State falseState;
    
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return CharacterStatestate == context.mainAgent.CurrentState ? equalState : falseState;
    }
}
