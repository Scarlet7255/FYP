using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class AnimatorParameterControl : PlayableAsset
{
    public ExposedReference<Animator> ani;
    public char pType;
    public string pName;
    public float fValue;
    public int iValue;
    public bool bValue;
    
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        Animator animator = ani.Resolve(graph.GetResolver());
        AnimatorParameterControlBehaviour apb = new AnimatorParameterControlBehaviour();
        apb._ani = animator;
        apb.pType = pType;
        apb.pName = pName;
        apb.bValue = bValue;
        
        return ScriptPlayable<AnimatorParameterControlBehaviour>.Create(graph, apb);
    }
}

public class AnimatorParameterControlBehaviour : PlayableBehaviour
{
    public Animator _ani;
    public char pType;
    public string pName;
    public float fValue;
    public int iValue;
    public bool bValue;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (pType == 'b')
        {
            _ani.SetBool(pName,bValue);
        }
    }
}
