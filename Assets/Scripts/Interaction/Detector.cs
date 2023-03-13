using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Detector : MonoBehaviour
{
    private Camera _cam;
    
    public float detectDistance;
    public GameObject Tip;
    
    private InteractableObject _preSelected = null;
    private InteractableObject _currentSelected = null;
    
    
    private void Start()
    {
        _cam = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, transform.forward,out hitInfo, detectDistance);
        Collider c = hitInfo.collider;
        
        if (c != null && c.gameObject.tag.Equals("Interactable"))
        {
            _currentSelected = c.GetComponentInParent<InteractableObject>(true);
            if (_preSelected != _currentSelected && _preSelected != null)
            {
                _preSelected.LostFocus();
                _preSelected = _currentSelected;
            }
            _currentSelected.Select();
            _preSelected = _currentSelected;
            Tip.SetActive(true);
            Vector3 screenPos = _cam.WorldToScreenPoint(_currentSelected.transform.position);
            Tip.transform.position = screenPos;
        }
        else
        {
            Tip.SetActive(false);
            if (_preSelected != null)
            {
                    _preSelected.LostFocus();
                    _preSelected = null;
                    _currentSelected = null;
            }
        }
    }

    public void Interact(InputAction.CallbackContext cbc)
    {
        if (_currentSelected != null)
        {
            _currentSelected.Action();
        }
    }

}
