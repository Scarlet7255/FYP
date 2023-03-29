using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum QuestType : int
{
    Collect = 1
}

public class MissionManager : MonoBehaviour
{
    #region Singleton

    private static MissionManager _instance;

    public static MissionManager Instance
    {
        get => _instance;
    }

    #endregion

    public List<CollectQuest> collectQuests;

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateQuest(int objId, int num)
    {
        for (int i = 0; i < (int)QuestType.Collect; ++i)
        {
            if(collectQuests[i].isQuestActive)
                collectQuests[i].Update(objId,num);
        }
    }

    public void ActiveQuest(int questId)
    {
        if (questId < (int)QuestType.Collect)
        {
            collectQuests[questId].isQuestActive = true;
        }
    }

    # region QuestUI

    public TMP_Text text;

    #endregion
}
