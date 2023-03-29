
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionItem : InteractableObject
{
    public int objId;
    public int num = 1;
    public override void Action(CharacterAgent source)
    {
        MissionManager.Instance.UpdateQuest(objId, num);
        gameObject.SetActive(false);
        LostFocus();
    }
}
