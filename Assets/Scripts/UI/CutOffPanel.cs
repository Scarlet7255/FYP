using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutOffPanel : MonoBehaviour
{
    private void Awake()
    {
        if(!MainMenu.Instance.gameStart) gameObject.SetActive(false);
    }
}
