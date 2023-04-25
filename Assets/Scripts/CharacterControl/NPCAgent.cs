
using UnityEngine;

public enum AIStrategy
{
    None,Search, Hanging, Patrol
}

public class NPCAgent : CharacterAgent
{
    [SerializeReference]private AIStrategy strategy;
    public bool isEnemy;
    public ViewDetector eye;
    public string target;
    public AIStrategy Strategy
    {
        get => strategy;
        set
        {
            strategy = value;
            treeRunner.ChangeStrategy(value);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        Strategy = strategy;
        treeRunner.blackboard.eye = eye;
    }

    protected override void Update()
    {
        base.Update();
        if(isEnemy) DetectPlayer();
    }

    private void DetectPlayer()
    {
        treeRunner.blackboard.seeTarget = eye.Query(target);
        if (treeRunner.blackboard.seeTarget && strategy != AIStrategy.Search)
        {
            treeRunner.blackboard.target = GameManger.Instance.Player.transform;
            Strategy = AIStrategy.Search;
            treeRunner.blackboard.runningAbortFlag = true;
        }
        
        
    }
}
