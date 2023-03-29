using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemAmountPair
{
    public int id;
    public int num;
}

[System.Serializable]
public abstract class Quest
{
    public string title;
    public string describe;
    public int questId;
    public UnityEvent QuestCompleteEvent;
    public bool isQuestActive = false;
    
    public void OnQuestComplete()
    {
        QuestCompleteEvent.Invoke();
        isQuestActive = false;
    }
    
}

[System.Serializable]
public class CollectQuest:Quest
{

    public List<ItemAmountPair> questList;
 
    public void Update(int questItemID, int num)
    {
        bool completed = true;
        foreach (var v in questList)
        {
            if (v.id == questItemID)
            {
                v.num -= num;
            }

            if (v.num > 0) completed = false;
        }
        
        if(completed) OnQuestComplete();
    }

}