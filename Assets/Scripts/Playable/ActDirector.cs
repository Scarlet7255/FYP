using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

[System.Serializable]
public class ActDirector : PlayableAsset
{
    public ExposedReference<TMP_Text> text;
    [TextArea]public string content;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        ActDirectorBehaviour ab = new ActDirectorBehaviour();
        ab.content = content;
        ab.textArea = text.Resolve(graph.GetResolver());
        return ScriptPlayable<ActDirectorBehaviour>.Create(graph, ab);
    }
}

public class ActDirectorBehaviour : PlayableBehaviour
{
    public TMP_Text textArea;
    public string content;
    
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        textArea.text = content;
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        textArea.text = "";
    }
}

