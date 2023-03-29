using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogControl : MonoBehaviour
{
    public static DialogControl Instance
    {
        get
        {
            return _instane;
        }
    }
    private static DialogControl _instane;
    

    public TMP_Text characterName;
    public TMP_Text content;
    public GameObject dialogPanel;
    
    public float defaultContinueTime = 1f;
    [HideInInspector]public float continueTime = 1f;
    private float timer = 0f;
    
    public void SetDialogContent(string cname, string text)
    {
        characterName.text = cname;
        content.text = text;
        continueTime = defaultContinueTime;
        timer = 0f;
        dialogPanel.SetActive(true);
    }
    
    public void SetDialogContent(string cname, string text, float continueTime)
    {
        SetDialogContent(cname,text);
        this.continueTime = continueTime;
    }

    private void Awake()
    {
        if(_instane) Destroy(this);
        _instane = this;
        dialogPanel.SetActive(false);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= continueTime) dialogPanel.SetActive(false);
    }
}
