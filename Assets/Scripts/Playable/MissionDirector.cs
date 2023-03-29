using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum MissionBehaviourType
{
    ActiveQuest, CompleteQuest
}

[System.Serializable]
public class MissionDirector : PlayableAsset
{
    public int missionID;

    public MissionBehaviourType behaviourType;
    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        return ScriptPlayable<MissionDirectorBehaviour>.Create(graph,
            new MissionDirectorBehaviour(missionID, behaviourType));
    }
}

public class MissionDirectorBehaviour : PlayableBehaviour
{
    private int _missionID;
    private MissionBehaviourType _behaviourType;

    public MissionDirectorBehaviour()
    {
        _missionID = 0;
        _behaviourType = MissionBehaviourType.ActiveQuest;
    }

    public MissionDirectorBehaviour(int missionID, MissionBehaviourType behaviourType)
    {
        _missionID = missionID;
        _behaviourType = behaviourType;
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if(_behaviourType == MissionBehaviourType.ActiveQuest) MissionManager.Instance.ActiveQuest(_missionID);
    }
}