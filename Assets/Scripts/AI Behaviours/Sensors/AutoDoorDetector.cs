using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
public class AutoDoorDetector : MonoBehaviour
{
    public Animator animator;
    public Collider interactionTrigger;
    public CharacterAgent agent;
    public int upperBodyLayer = 2;

    public bool autoCloseDoor = false;
    public bool autoOpenDoor = true;
    
    
    private void Start()
    {
       if(interactionTrigger) Physics.IgnoreCollision(interactionTrigger,gameObject.GetComponent<Collider>());
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag.Equals("Door") && autoOpenDoor)
        {
            animator.SetLayerWeight(upperBodyLayer,1.0f);
            animator.Play("Opening");
            InteractableDoor door = other.GetComponent<InteractableDoor>();
            door.source = agent;
            door.Open();
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Door"))
        {
            animator.SetLayerWeight(upperBodyLayer,0.0f);
            if (autoCloseDoor)
            {
                InteractableDoor door = other.GetComponent<InteractableDoor>();
                door.Close();
            }
        }
    }
}
