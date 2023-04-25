using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public enum DirctorInstruct{
    StopDisplay, WaitForSignal,Loop, GameRestart, ToMainMenu
}

public class DirectorControlTemplete : PlayableAsset
{
    public DirctorInstruct instruct;
    public ExposedReference<PlayableDirector> director;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        PlayableDirector d = director.Resolve(graph.GetResolver());
        DirectorControlTempleteBehaviour b = new DirectorControlTempleteBehaviour();
        b.director = d;
        b.instruct = instruct;
        return ScriptPlayable<DirectorControlTempleteBehaviour>.Create(graph, b);
    }
}

public class DirectorControlTempleteBehaviour : PlayableBehaviour
{
    public DirctorInstruct instruct;
    public PlayableDirector director;
    
    private double startTime = 0f;
    
    
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        switch (instruct)
        {
            case DirctorInstruct.StopDisplay:
            {
                director.gameObject.SetActive(false);
                break;
            }
            case DirctorInstruct.WaitForSignal:
            case DirctorInstruct.Loop:
            {
                startTime = director.time;
                break;
            }
            case DirctorInstruct.GameRestart:
            {
                GameManger.Instance.OnGameRestart.Invoke();
                break;
            }
            case DirctorInstruct.ToMainMenu:
            {
                SceneManager.LoadScene(0);
                break;
            }
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        switch (instruct)
        {
            case DirctorInstruct.Loop:
            case DirctorInstruct.WaitForSignal:
            {
                director.time = startTime;
                break;
            }
        }
    }
    
    
}
