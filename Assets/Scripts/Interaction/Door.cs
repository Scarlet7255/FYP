using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    public GameObject rotateObject;
    public Vector3 rotateAxis = Vector3.up;
    public float rotateSpeed = 350.0f;
    public float maxRotateDegree = 90.0f;
    
    private bool _rotating = false;
    public Collider targetCollider;
    [SerializeField]private bool open = false;
    [SerializeField]private float currentDegree = 0.0f;

    private void Start()
    {
        if (rotateObject == null) rotateObject = gameObject;
        rotateAxis = rotateAxis.normalized;
    }

    public override void Action()
    {
        _rotating = true;
        open = !open;
    }

    private void FixedUpdate()
    {
        if (!_rotating)
        {
            targetCollider.enabled = true;
            return;
        }

        targetCollider.enabled = false;
        float rotateDegree = rotateSpeed*Time.fixedDeltaTime;
        if (open)
        {
            if (rotateDegree + currentDegree > maxRotateDegree)
            {
                rotateDegree = maxRotateDegree - currentDegree;
                _rotating = false;
            }
            rotateObject.transform.Rotate(rotateAxis,rotateDegree);
            currentDegree += rotateDegree;
        }
        else
        {
            if (currentDegree < rotateDegree)
            {
                rotateDegree = currentDegree;
                _rotating = false;
            }
            rotateObject.transform.Rotate(rotateAxis,rotateDegree*-1f);
            currentDegree -= rotateDegree;
        }
    }
}
