using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : InteractableDoor
{

    public Door L;
    public Door R;

    public Transform hidePlace;

    public override void Open()
    {
        L.targetAngle = 90.0f;
        R.targetAngle = -90f;
    }

    public override void Close()
    {
        L.targetAngle = 0f;
        R.targetAngle = 0f;
    }

    public override void Action(CharacterAgent _source)
    {
        Open();
        _source.CurrentState = CharacterState.Hide;
        _source.Move(hidePlace.position);
        _source.interactable = this;
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        Close();
    }
}
