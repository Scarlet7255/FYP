using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  Cinemachine;
public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CinemachineVirtualCamera[] c = FindObjectsOfType(typeof(CinemachineVirtualCamera)) as CinemachineVirtualCamera[];
        foreach (var VARIABLE in c)
        {
            Debug.Log(VARIABLE.gameObject);
        }
    }
    
}
