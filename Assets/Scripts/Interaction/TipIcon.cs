
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipIcon : MonoBehaviour
{
    public InteractableObject interactObj;

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(interactObj.transform.position);
    }

    public void Interact()
    {
        interactObj.Action(GameManger.Instance.Player);
    }
}
