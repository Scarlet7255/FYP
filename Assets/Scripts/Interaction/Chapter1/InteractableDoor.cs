using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableDoor : InteractableObject
{
    public List<Door> doors;
    public bool allOpen { get; private set; } = false;

    public bool isAllOpen()
    {
        foreach (var v in doors)
        {
            if (!v.DoorOpen) return false;
        }

        return true;
    }

    public override void Action()
    {
        allOpen = true;
        foreach (var v in doors)
        {
            v.ChangeState(!v.DoorOpen);
            allOpen = allOpen && v.DoorOpen;
        }
    }
    
    public void Open()
    {
        allOpen = true;
        foreach (var v in doors)
        {
            v.ChangeState(true);
            allOpen = allOpen && v.DoorOpen;
        }
    }

    public void Close()
    {
        allOpen = false;
        foreach (var v in doors)
        {
            v.ChangeState(false);
            allOpen = allOpen && v.DoorOpen;
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
