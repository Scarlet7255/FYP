using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CheckableObject : InteractableObject
{
    public GameObject dialogClip;

    public bool useTimeline = true;
    public string characterName;
    [TextArea]public string content;
    public float dialogTime = 1f;
    public override void Action(CharacterAgent source)
    {
        if (useTimeline)
        {
            dialogClip.SetActive(true);
            dialogClip.GetComponent<PlayableDirector>().Play();
        }
        else
        {
            DialogControl.Instance.SetDialogContent(characterName,content,dialogTime);
        }
    }
}
