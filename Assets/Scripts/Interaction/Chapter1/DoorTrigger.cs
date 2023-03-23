using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DoorTrigger : MonoBehaviour
{
    public Door door;
    public OffMeshLink link;

    private void Start()
    {
        if (!link) link = gameObject.GetComponent<OffMeshLink>();
    }

    public void OnDoorStateChange(bool isOpen)
    {
        link.activated = !isOpen;
    }
}
