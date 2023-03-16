using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAgent : MonoBehaviour
{
    public MovementControl moveAgent;
    public NavMeshAgent navAgent;
    private bool _waitForDoor;
    private Door _waitDoor;

    private void Update()
    {
        if (navAgent.isOnOffMeshLink)
        {
            _waitDoor = navAgent.currentOffMeshLinkData.offMeshLink.GetComponent<DoorTrigger>().door;
            if (_waitDoor.DoorOpen) 
                moveAgent.CompleteOffMeshLinkNormalSpeed(navAgent.currentOffMeshLinkData.endPos + navAgent.baseOffset*Vector3.up);
            else
            {
                _waitDoor.ChangeState(true);
                _waitForDoor = true;
            }
        }

        if (_waitForDoor)
        {
            _waitForDoor = !_waitDoor.DoorOpen;
            //if(!_waitDoor) moveAgent.CompleteOffMeshLinkNormalSpeed(navAgent.currentOffMeshLinkData.endPos + navAgent.baseOffset*Vector3.up);
        }
    }

    public void Move(RaycastHit hitInfo)
    {
        moveAgent.Move(hitInfo.point);
    }
    
      
}
