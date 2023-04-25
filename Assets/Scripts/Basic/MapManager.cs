using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProbabilityGraphNode<T>
{
    public T obj;
    public float probaility;

    public ProbabilityGraphNode(T o, float pro)
    {
        obj = o;
        probaility = pro;
    }
}

[System.Serializable]
public class Area
{
    public string areaName;
    public List<Transform> checkPoints;
    public List<ProbabilityGraphNode<Vector3>> probabilityGraph = new List<ProbabilityGraphNode<Vector3>>();

    private List<Vector3> pointBuffer = new List<Vector3>();

    /// <summary>
    /// return nearest check point in the area.
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="maxDistance"></param>
    /// <returns></returns>
    public Vector3 GetNearestPoint(Vector3 startPos, float maxDistance = 1000, float minDistance = 2)
    {
        Vector3 nearest = startPos + Vector3.up * maxDistance;
        float dis = maxDistance;
        foreach (var t in checkPoints)
        {
            Vector3 offset = t.position - startPos;
            float d = offset.magnitude;
            if(d<minDistance) continue;
            if (d < dis)
            {
                nearest = t.position;
                dis = offset.magnitude;
            }
        }

        return nearest;
    }

    /// <summary>
    /// check points inside a distance, and fill them in a list
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="points"></param>
    /// <param name="maxDistance"></param>
    public void GetNearestPoints(Vector3 startPos, List<Vector3> points, float maxDistance = 1000f, float minDistance = 3f)
    {
        points.Clear();
        foreach (var t in checkPoints)
        {
            var dis = (t.position - startPos).magnitude;
            if (dis < maxDistance && dis>minDistance)
            {
                points.Add(t.position);
            }
        }
    }
    
    /// <summary>
    /// check points inside a distance, and fill them in a list
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="points"></param>
    /// <param name="maxDistance"></param>
    public List<Vector3> GetNearestPoints(Vector3 startPos, float maxDistance = 1000)
    {
        List<Vector3> points = new List<Vector3>();
        GetNearestPoints(startPos, points, maxDistance);
        return points;
    }

    /// <summary>
    /// return one of the possible point in one direction
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="points"></param>
    /// <param name="maxDistance"></param>
    public Vector3 GetRandomNearbyPoint(Vector3 startPos, float maxDistance = 1000,float minDistance = 3f)
    {
        pointBuffer.Clear();
        GetNearestPoints(startPos,pointBuffer,maxDistance, minDistance);
        return pointBuffer[Random.Range(0, pointBuffer.Count)];
    }

    public Vector3 GetPossibleSearchPoint(Vector3 startPos, Vector3 checkDirection, float maxDistance = 1000, float minDistance = 3f)
    {
        probabilityGraph.Clear();
        
        float totalDot = 0f;
        foreach (var t in checkPoints)
        {
            Vector3 dir = t.position - startPos;
            if (dir.magnitude <= maxDistance)
            {
                float p = Vector3.Dot(dir.normalized, checkDirection.normalized);
                if (p > 0)
                {
                    totalDot += p;
                    probabilityGraph.Add(new ProbabilityGraphNode<Vector3>(t.position,totalDot));
                }
            }
        }

        float rand = Random.Range(0, totalDot);

        int index;
        for (index = 0; index < probabilityGraph.Count; ++index)
        {
            if (rand <= probabilityGraph[index].probaility)
                return probabilityGraph[index].obj;
        }

        if (probabilityGraph.Count == 0)
        {
            return GetRandomNearbyPoint(startPos, maxDistance, minDistance);
        }
        Debug.Log(probabilityGraph.Count);
        return probabilityGraph[0].obj;
    }
}

public class MapManager : MonoBehaviour
{
    
    private static MapManager _instance;
    private Dictionary<string, Area> _mapDic;
    public List<Area> areaList;

    public static MapManager Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        _instance = this;
        _mapDic = new Dictionary<string, Area>();
        foreach (var area in areaList)
        {
            _mapDic.Add(area.areaName,area);            
        }
    }


    public Area GetArea(string areaName)
    {
        return _mapDic[areaName];
    }
}
