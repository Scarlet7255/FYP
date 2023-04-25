using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionHandler : MonoBehaviour
{
    public Transform player;
    private List<OcclusionObject> occObjects;
    public RaycastHit[] hitInfo = new RaycastHit[20];
    private void Awake()
    {
        occObjects = new List<OcclusionObject>();
    }

    private void Update()
    {
        if(!gameObject.activeSelf) return;
        
        Vector3 dir = player.position - transform.position;
        float dis = dir.magnitude;
        dir = dir.normalized;
        
        Physics.RaycastNonAlloc(transform.position, dir, hitInfo, dis);

        foreach (var oc in occObjects)
        {
            oc.SetBack();
        }
        
        occObjects.Clear();
        
        for (int i = 0; i<20; ++i)
        {
            var hit = hitInfo[i];
            if(!hit.collider) break;; 
            OcclusionObject ocobj;
            if (!hit.collider.gameObject.TryGetComponent(out ocobj)) continue;
            ocobj.HandleOcculsion();
            occObjects.Add(ocobj);
        }
        
        
    }
}
