using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableDoor : InteractableObject
{
    public List<Door> doors;
    public bool AllOpen {
        get
        {
            foreach (var d in doors)
            {
                if (d.Close) return false;
            }

            return true;
        }
    }

    public override void Action(CharacterAgent _source)
    {
        source = _source;
        if(AllOpen) Close();
        else Open();
    }
    
    public virtual void Open()
    {
        if (AllOpen) return;
        if (source)
        {
            float dot = Vector3.Dot(source.transform.position-transform.position, transform.right);
            float target = 0.0f;
            if (dot > 0)
                target = -90;
            else
                target = 90f;
            foreach (var d in doors)
            {
                d.targetAngle = target;
            }
        }
        else
        {
            foreach (var d in doors)
            {
                d.targetAngle = 90f;
            }
        }
    }

    public virtual void Close()
    {
        foreach (var d in doors)
        {
            d.targetAngle = 0f;
        }
    }

    public void DisableIcon()
    {
        if(icon) icon.SetActive(false);
    }

    public void EnableIcon()
    {
       if(icon) icon.SetActive(true);
    }
}
