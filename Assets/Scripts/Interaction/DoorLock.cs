using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorLock : InteractableObject
{
    public bool locked = false;
    public bool breakable = true;
    public float lifeTime = 30f;
    public InteractableDoor door;
    public override void Action(CharacterAgent source)
    {
        locked = true;
        foreach (var d in door.doors)
        {
            d.Lock = true;
        }
    }

    protected new void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player") && door.AllClose && 
            Common.IsFront(other.transform.position, transform.position, transform.forward, 0.7f))
        {
            Select();
        }
        else 
            LostFocus();
    }
    
}
