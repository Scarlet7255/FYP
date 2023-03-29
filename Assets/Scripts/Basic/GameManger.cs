using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    #region Singleton

    private static GameManger _instance;

    public static GameManger Instance => _instance;

    #endregion

    public PlayerAgent Player;
    
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
