using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoorDetector : MonoBehaviour
{
    public Animator animator;
    public Collider interactionTrigger;
    public CharacterAgent agent;
    public int upperBodyLayer = 2;

    public bool autoCloseDoor = false;
    public bool autoOpenDoor = true;
    public bool autoOpenCloset = false;
    public InteractableDoor door;

    private void Start()
    {
       if(interactionTrigger) Physics.IgnoreCollision(interactionTrigger,gameObject.GetComponent<Collider>());
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag.Equals("Door"))
        {
            door = other.GetComponent<InteractableDoor>();
            if (autoOpenDoor)
            {
                animator.SetLayerWeight(upperBodyLayer,1.0f);
                animator.Play("Opening");
                door.source = agent;
                door.Open();
            }
        }

        if (other.gameObject.tag.Equals("Closet") && autoOpenCloset)
        {
            Wardrobe w = other.GetComponent<Wardrobe>();
            w.Open();
        }
    }

    public void OpenDoorAction()
    {
        if (!door) return;
        animator.SetLayerWeight(upperBodyLayer,1.0f);
        animator.Play("Opening");
        door.source = agent;
        door.Open();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Door"))
        {
            animator.SetLayerWeight(upperBodyLayer,0.0f);
            if (autoCloseDoor)
            {
                door = other.GetComponent<InteractableDoor>();
                door.Close();
            }
            door = null;
        }
    }
}
