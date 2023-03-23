
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UIManager : MonoBehaviour
{

    # region Singleton
    private static UIManager _instance;
    public static UIManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    # endregion

    public Canvas mainCanvas;
    public GameObject tipObjPanel;
    
    
}
