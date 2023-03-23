using UnityEditor;
using UnityEngine;
using System.Collections;
using Vector3 = System.Numerics.Vector3;

//项目中应已包含了此类
public class WireArcExample : MonoBehaviour
{
    public float radius;
    public float degree;
    public Color c;
}

// 使用附加到圆盘的 ScaleValueHandle 创建一个 180 度的线弧，
// 允许您修改 WireArcExample 中的 "radius" 的值
[CustomEditor(typeof(WireArcExample))]
public class DrawWireArc : Editor
{
    void OnSceneGUI()
    {
        WireArcExample myObj = (WireArcExample)target;
        Handles.color = myObj.c;
        Handles.DrawSolidArc(myObj.transform.position, myObj.transform.up, myObj.transform.forward, -myObj.degree/2, myObj.radius);
        Handles.DrawSolidArc(myObj.transform.position, myObj.transform.up, myObj.transform.forward, myObj.degree/2, myObj.radius);
        //myObj.radius = (float)Handles.ScaleValueHandle(myObj.radius, myObj.transform.position + myObj.transform.forward * myObj.radius, myObj.transform.rotation, 1, Handles.ConeHandleCap, 1);
    }
}