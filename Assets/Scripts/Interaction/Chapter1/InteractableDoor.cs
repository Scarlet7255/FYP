using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : InteractableObject
{
    public List<Door> doors;
    public override void Action()
    {
        foreach (var d in doors)
        {
            d.ChangeState();
        }
    }
}
