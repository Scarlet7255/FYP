using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Thunder))]
public class AudioSampler : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Thunder sampler = target as Thunder;
        if(GUILayout.Button("sample")) {
            sampler.Sample();
        }
    }
}
