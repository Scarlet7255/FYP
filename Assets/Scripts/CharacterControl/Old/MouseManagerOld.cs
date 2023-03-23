using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;

public class MouseManagerOld : MonoBehaviour
{
    private bool _onUI = false;
    [SerializeField] private MovementControl _controller;

    private float lastPress = -1.0f;
    public PlayerAgentOld mainAgent;

    public void Select(InputAction.CallbackContext cbc)
    {
        if (_onUI) return;
        if (cbc.performed)
        {
            RaycastHit hit; NavMeshHit navHit;
            bool hitted = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100,LayerMask.GetMask("Ground"));

            NavMesh.SamplePosition(hit.point, out navHit, 0.5f, NavMesh.AllAreas);
            if (navHit.mask == 0) return;

            if (hitted)
            {
                mainAgent.Move(hit.point);
            }

            if(Time.time - lastPress<0.3f) _controller.SetState(MovementState.Run);
            else _controller.SetState(MovementState.Stand);
            lastPress = Time.time;
        }
    }

    public void Interact()  
    {
    }
}
