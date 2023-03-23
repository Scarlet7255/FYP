using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
public class DoorDetector : MonoBehaviour
{
    public Animator animator;
    public Collider interactionTrigger;
    public CharacterAgent agent;
    private void Start()
    {
        Physics.IgnoreCollision(interactionTrigger,gameObject.GetComponent<Collider>());
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag.Equals("Door"))
        {
            animator.SetLayerWeight(2,1.0f);
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
            animator.SetLayerWeight(2,0.0f);
        }
    }
}
