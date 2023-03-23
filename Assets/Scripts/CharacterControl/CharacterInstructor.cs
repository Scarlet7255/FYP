using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInstructor : MonoBehaviour
{

    public PlayerAgent player;
    public LayerMask validLayers;
    public LayerMask selectableLayer;
    
    public void Select(InputAction.CallbackContext cbc)
    {
        if (cbc.performed)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f,selectableLayer))
            {
                if((1<<hitInfo.collider.gameObject.layer & validLayers)>0)
                    player.Move(hitInfo.point);
                else Debug.Log("INVALID MOVEMENT");
            }
            else Debug.Log("INVALID MOVEMENT");
        }
    }
}
