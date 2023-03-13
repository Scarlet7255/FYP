using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : InteractableObject
{
    public Transform positionA;
    public Transform positionB;
    private Transform _target;
    public float speed = 5.0f;

    private void Start()
    {
        _target = positionA;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,_target.position,speed*Time.fixedDeltaTime);
    }

    public override void Action()
    {
        if (_target == positionA) _target = positionB;
        else _target = positionA;
    }
}
