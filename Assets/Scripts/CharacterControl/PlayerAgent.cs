using System;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

public class PlayerAgent : CharacterAgent
{
    public GameObject DetectionTarget;

    public override CharacterState CurrentState
    {
        get=> treeRunner.blackboard.state;
        set
        {
            treeRunner.blackboard.state = value;
            if (value == CharacterState.Hide)
            {
                DetectionTarget.SetActive(false);
            }
            else DetectionTarget.SetActive(true);
        }
    }
}
