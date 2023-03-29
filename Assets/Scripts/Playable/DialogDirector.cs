using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class DialogDirector : PlayableAsset
{
    public string characterName;
    [TextArea]public string dialogContent;
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        DialogDirectorBehaviour ddb = new DialogDirectorBehaviour();
        ddb.context = dialogContent;
        ddb.characterName = characterName;
        return ScriptPlayable<DialogDirectorBehaviour>.Create(graph, ddb);
    }
    
}

public class DialogDirectorBehaviour : PlayableBehaviour
{
    public string characterName;
    public string context;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        DialogControl.Instance.SetDialogContent(characterName,context,(float)playable.GetDuration());
    }
    
}