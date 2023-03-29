using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class CharacterDirector : PlayableAsset
{
    public ExposedReference<CharacterAgent> agent;

    public Vector3 pos;
    
    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        CharacterDirectorBehaviour cd = new CharacterDirectorBehaviour();
        cd.agent = agent.Resolve(graph.GetResolver());
        cd.targetPos = pos;
        return ScriptPlayable<CharacterDirectorBehaviour>.Create(graph,cd);
    }
}

public class CharacterDirectorBehaviour : PlayableBehaviour
{
    public CharacterAgent agent;
    public Vector3 targetPos;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        agent.Move(targetPos);
    }
}