using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum CameraDirectorInstruct
{
    SetPosition, Tween
}

public class CameraDirector : PlayableAsset
{
    public CameraDirectorInstruct instruct;
    public ExposedReference<Transform> camTrans;
    public ExposedReference<Transform>  destination;
    public float tweenSpeed = 0.8f;
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        CameraDirectorBehaviour b = new CameraDirectorBehaviour();
        b.cam = camTrans.Resolve(graph.GetResolver());
        b.Instruct = instruct;
        b.destination = destination.Resolve(graph.GetResolver());
        b.tweenSpeed = tweenSpeed;
        return ScriptPlayable<CameraDirectorBehaviour>.Create(graph,b);
    }
}

public class CameraDirectorBehaviour : PlayableBehaviour
{
    public CameraDirectorInstruct Instruct;
    public Transform destination;
    public Transform cam;
    public float tweenSpeed;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (Instruct == CameraDirectorInstruct.SetPosition)
        {
            cam.position = destination.position;
        }
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        cam.position = Vector3.Lerp(cam.position, destination.position, tweenSpeed * info.deltaTime);
    }
    
}
