
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipIcon : MonoBehaviour
{
    public InteractableObject interactObj;
    public Image iconImage;

    public void SetIconInfo(InteractableObject iobj, Sprite iconSprite)
    {
        interactObj = iobj;
        iconImage.sprite = iconSprite;
    }

    private void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(interactObj.transform.position);
    }

    public void Interact()
    {
        interactObj.Action(GameManger.Instance.Player);
    }
}
