
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class OffMeshTrigger : MonoBehaviour
{
    public UnityEvent OnEnterMesh;
    public InteractableObject trigTarget;
    public OffMeshLink _offMeshLink;

    private void Awake()
    {
        _offMeshLink = gameObject.GetComponent<OffMeshLink>();
    }

    public void Trig()
    {
        OnEnterMesh.Invoke();
    }

    public void SetOffMeshLinkActive(bool isActive)
    {
        _offMeshLink.activated = isActive;
    }
}
