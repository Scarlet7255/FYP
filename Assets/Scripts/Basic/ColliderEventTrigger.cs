using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public enum TrigCondition
{
    Tag, Name
}

public class ColliderEventTrigger : MonoBehaviour
{
    public UnityEvent signal;
    public TrigCondition Condition;
    public string target;
    public bool once = true;
    private void OnTriggerEnter(Collider other)
    {
        if (Condition == TrigCondition.Name && other.gameObject.name.Equals(target))
        {
            signal.Invoke();
            gameObject.SetActive(!once);
        }
        else if(other.gameObject.tag.Equals(target))
        {
            signal.Invoke();
            gameObject.SetActive(!once);
        }
    }
}
