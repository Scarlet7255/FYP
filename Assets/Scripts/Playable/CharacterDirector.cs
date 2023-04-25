using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class CharacterDirector : PlayableAsset
{
    public ExposedReference<CharacterAgent> agent;

    public Vector3 pos;

    public bool warpTo = true;
    
    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        CharacterDirectorBehaviour cd = new CharacterDirectorBehaviour();
        cd.agent = agent.Resolve(graph.GetResolver());
        cd.targetPos = pos;
        cd.warpTo = warpTo;
        return ScriptPlayable<CharacterDirectorBehaviour>.Create(graph,cd);
    }
}

public class CharacterDirectorBehaviour : PlayableBehaviour
{
    public CharacterAgent agent;
    public Vector3 targetPos;
    public bool warpTo = true;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        agent.AbortCurrentAction();
        if(warpTo) agent.navAgent.Warp(targetPos);
        agent.Move(targetPos);
    }
}