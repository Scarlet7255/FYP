using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ViewDetector : MonoBehaviour
{
    public FOV mainFov;
    public FOV secondFov;
    public float radius;

    public LayerMask detectLayers;
    public LayerMask visibleLayers;
    
    public float lReactTime;
    public float hReactTime;
    public float continueTime;
    
    public Collider[] detectionColliderBuffer = new Collider[20];
    private Dictionary<string, Detection> _detections;
    
    
    private bool tagFlag = false; // use to track the detection information valid each frame
    private List<string> _untrackList; // use store keys in dicitionary

    /// <summary>
    /// check if there is a expected target
    /// </summary>
    /// <param name="targetName"></param>
    /// <returns></returns>
    public bool Query(string targetName)
    {
        return _detections.ContainsKey(targetName) && _detections[targetName].detected;
    }
    
    /// <summary>
    /// check if there is a expected target
    /// </summary>
    /// <param name="targetName"></param>
    /// <returns></returns>
    public bool Query(string targetName, out GameObject target)
    {
        target = null;
        if (_detections.ContainsKey(targetName))
        {
            target = _detections[targetName].Owner;
            return Query(targetName);
        }

        return false;
    }
    private void Awake()
    {
        _detections = new Dictionary<string, Detection>();
        _untrackList = new List<string>();
    }

    private void Update()
    {
        tagFlag = !tagFlag;
        Detect();
        ClearInvisibleTarget();
    }

    private void Detect()
    {
       int len = Physics.OverlapSphereNonAlloc(transform.position,radius,detectionColliderBuffer,detectLayers);
       
       for (int i = 0; i<len; ++i)
       {
           DetectionTarget target;
           if(!detectionColliderBuffer[i].TryGetComponent<DetectionTarget>(out target))continue;
           
           string targetName = target.owner.name;
           
           // check if detected and in which area 
           int v = CheckArea(target.transform.position);
           if (v == 0)
           {
               DelayRemoveDetection(targetName);
               continue;
           }

           RaycastHit hitInfo;
           
           if (Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized,
                   out hitInfo, radius,
                   visibleLayers))
           {
               int hitLayerMask = 1 << hitInfo.collider.gameObject.layer;

               if ((hitLayerMask & detectLayers.value )== 0)//blocked by obstacle
               {
                   DelayRemoveDetection(targetName);
                   continue;
               }
                // update or add information if detected
               if (_detections.ContainsKey(targetName))
               {
                   var value = _detections[targetName];
                   value.Timer += Time.deltaTime;
                   value.tag = tagFlag;
                   value.life = continueTime;
                   if (v == 1 && value.Timer >= lReactTime)
                       value.detected = true;
                   else if (value.Timer >= hReactTime)
                       value.detected = true;
                   _detections[targetName] = value;
               }
               else
               {
                   Detection val = new Detection(target.owner,tagFlag,continueTime);
                   _detections.Add(targetName,val);
               }
           }
           else
           {
               _detections.Remove(targetName);
           }
       }
       
    }

    /// <summary>
    /// clear targets even not in sphere
    /// </summary>
    private void ClearInvisibleTarget()
    {
        int len = Physics.OverlapSphereNonAlloc(transform.position,radius,detectionColliderBuffer,detectLayers);
        foreach (var v in _detections)
        {
            if(v.Value.tag != tagFlag) _untrackList.Add(v.Key);
        }

        foreach (var v in _untrackList)
        {
            DelayRemoveDetection(v);
        }
        
        _untrackList.Clear();
        
    }

    /// <summary>
    /// check target if inside fov area
    /// </summary>
    /// <param name="targetPos">return 1 if in first FOV, return 2 if in second FOV, return -1 if outside FOV</param>
    /// <returns></returns>
    private int CheckArea(Vector3 targetPos)
    {
        Vector3 d = (targetPos - transform.position).normalized;
        float cos = Vector3.Dot(d, transform.forward.normalized);
        float degree = Mathf.Abs(Mathf.Acos(cos)*Mathf.Rad2Deg);
        if (degree < mainFov.angle / 2) return 1;
        if (degree < secondFov.angle / 2) return 2;
        return 0;
    }

    private void DelayRemoveDetection(string targetName)
    {
        if (!_detections.ContainsKey(targetName)) return;
        var t = _detections[targetName];
        t.life -= Time.deltaTime;
        
        if (t.life <= 0f) 
            _detections.Remove(targetName);
        else 
            _detections[targetName] = t;
    }

    private struct Detection
    {
        public GameObject Owner;
        public float Timer;
        public bool detected;
        public bool tag;// the flag to track detection information
        public float life;
        
        public Detection(GameObject o, bool t, float continueTime)
        {
            Owner = o;
            Timer = 0.0f;
            detected = false;
            tag = t;
            life = continueTime;
        }
    }
}

[System.Serializable]
public struct FOV
{
    public float reactTime;
    public float angle;
}
