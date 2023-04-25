using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInstructor : MonoBehaviour
{

    public PlayerAgent player;
    public LayerMask validLayers;
    public LayerMask selectableLayer;

    private bool _enableInput;
    public bool EnableInput
    {
        get=> _enableInput;
        set
        {
            _enableInput = value;
            if (_enableInput)
            {
                player.Move(transform.position);
                
            }
        }
    }

    public void Select(InputAction.CallbackContext cbc)
    {
        if (!_enableInput) return;
        if (cbc.performed)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f,selectableLayer))
            {
                if ((1 << hitInfo.collider.gameObject.layer & validLayers) > 0)
                {
                    player.Move(hitInfo.point);
                    player.CurrentState = CharacterState.Walk;
                }
                else Debug.Log("INVALID MOVEMENT");
            }
            else Debug.Log("INVALID MOVEMENT");
        }
    }
    
    
}
