
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SneakEntry : InteractableObject
{
    public Transform entry;
    public override void Action()
    {
        GameManger.Instance.Player.SneakBelow(entry.position);
    }
}
