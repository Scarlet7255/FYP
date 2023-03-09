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
    public Material selectedEffect;
    
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
            Tip.SetActive(true);
            Vector3 screenPos = _cam.WorldToScreenPoint(c.transform.position);
            Tip.transform.position = screenPos;
            
            _currentSelected = c.GetComponent<InteractableObject>();
            if (_preSelected != _currentSelected && _preSelected != null)
            {
                _preSelected.LostFocus();
                _preSelected = _currentSelected;
            }
            _currentSelected.Select(selectedEffect);
            _preSelected = _currentSelected;
        }
        else
        {
            Tip.SetActive(false);
            if (_preSelected != null)
            {
                    _preSelected.LostFocus();
                    _preSelected = null;
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
