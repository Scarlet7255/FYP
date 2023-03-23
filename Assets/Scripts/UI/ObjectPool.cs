using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    # region Singleton
    private static ObjectPool _instance;

    public static ObjectPool Instance
    {
        get
        {
            if (!_instance) _instance = new GameObject("ObjectPool").AddComponent<ObjectPool>();
            return _instance;
        }
    }
    
    # endregion
    
    public string gameObjectPrefabURL = "Prefabs/";
    private Dictionary<string, List<GameObject>> _objDic;
    private int _prefixL = 0;
    private StringBuilder _stringBuilder;
    
    public void Put(string objName, GameObject obj)
    {
        if (!obj) return;
        obj.SetActive(false);
        if (!_objDic.ContainsKey(objName))
        {
            List<GameObject> l = new List<GameObject>();
            _objDic.Add(objName,l);
        }
        _objDic[objName].Add(obj);
        obj.name = objName;
    }

    public GameObject Take(string objName)
    {
        if (_objDic.ContainsKey(objName) && _objDic[objName].Count>0)
        {
            GameObject g = _objDic[objName].Last();
            _objDic[objName].Remove(g);
            return g;
        }
        else
        {
            if(!_objDic.ContainsKey(objName)) _objDic.Add(objName,new List<GameObject>());
            _stringBuilder.Append(objName);
            GameObject o = Resources.Load<GameObject>(_stringBuilder.ToString());
            o = Instantiate(o);
            o.name = objName;
            _stringBuilder.Remove(_prefixL, _stringBuilder.Length-_prefixL);
            return o;
        }
    }
    
    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);

        _objDic = new Dictionary<string, List<GameObject>>();
        _stringBuilder = new StringBuilder(gameObjectPrefabURL);
        _prefixL = gameObjectPrefabURL.Length;
    }
}
