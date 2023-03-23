using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using TheKiwiCoder;
public enum PlayerState
{
    Investigate,Hidden
}

public class PlayerAgentOld : MonoBehaviour
{
    public MovementControl moveAgent;
    public NavMeshAgent navAgent;

    private Vector3 _target;

    private GameObject _offLinkOwner;
    
    private bool _waiting;
    private InteractableDoor _waitDoor;
    
    public PlayerState playerState { get; private set; } = PlayerState.Investigate;
    
    public BehaviourTreeRunner behaviourRunner;
    public List<BehaviourTree> behaviourTrees;
    private Dictionary<string, int> _behaviourDic;

    private void Awake()
    {
        _behaviourDic = new Dictionary<string, int>();
        for (int i = 0; i < behaviourTrees.Count; ++i)
        {
            _behaviourDic.Add(behaviourTrees[i].name,i);
        }

        behaviourRunner.enabled = false;
    }

    private void Start()
    {
        //GameManger.Instance.Player = this;
    }

    private void Update()
    {
        if(navAgent.isOnOffMeshLink) OnOffMeshLink();
    }

    public void Move(Vector3 position)
    {
        if (!navAgent.enabled)
        {
            navAgent.enabled = true;
        }

        _target = position;
        moveAgent.Move(position);
    }

    private void OnOffMeshLink()
    {
        _offLinkOwner = navAgent.currentOffMeshLinkData.offMeshLink.gameObject;
        if(_offLinkOwner.tag.Equals("Door Entry"))
            EnterClosedDoor();
        else if(_offLinkOwner.tag.Equals("Stash Point"))
            navAgent.CompleteOffMeshLink();
    }

    private void EnterClosedDoor()
    {
        if (!_waitDoor)
        {
            _waitDoor = _offLinkOwner.GetComponent<OffMeshTrigger>().trigTarget as InteractableDoor;
        }

        _waiting = !_waitDoor.allOpen;
        if (!_waiting)
        {
            moveAgent.ResetPath();
            _waitDoor = null;
        }
        else
        {
            _waitDoor.Open();
            _waiting = true;
        }

    }
/*
    #region Behaviour Control

    public void UseStashSpot(StashSpot sp)
    {
        CallBehaviour("HideInStash");
        var bc = behaviourRunner.blackboard;
        bc.interactionTarget = sp;
        bc.moveToPosition = sp.hidePos.position;
    }

    public void CallBehaviour(string behaviourName)
    {
        behaviourRunner.ChangeTree(behaviourTrees[_behaviourDic[behaviourName]]); 
        behaviourRunner.enabled = true;
    }

    #endregion
    */
}
