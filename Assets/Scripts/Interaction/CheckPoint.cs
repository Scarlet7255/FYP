using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CheckPoint : MonoBehaviour
{
    public bool displayOnce = true;
    public bool useDialogClip = false;
    public string chName;
    public string content;
    public float continueTime = 3f;
    
    public PlayableDirector dialogClip;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Detector"))
        {
           if(useDialogClip) dialogClip.Play();
           else
           {
               DialogControl.Instance.SetDialogContent(chName,content,continueTime);
           }
        }
        if(displayOnce) Destroy(gameObject);
    }
}
