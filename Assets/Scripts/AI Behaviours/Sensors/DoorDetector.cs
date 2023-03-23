using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
public class DoorDetector : MonoBehaviour
{
    public Animator animator;
    public Collider interactionTrigger;
    
    private void Start()
    {
        Physics.IgnoreCollision(interactionTrigger,gameObject.GetComponent<Collider>());
    }

    private void OnTriggerEnter(Collider other)
    { 
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag.Equals("Door"))
        {
            animator.SetLayerWeight(2,1.0f);
            animator.Play("Opening");
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
