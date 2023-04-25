using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;
public class MoveWaitPlayer : Move
{
    public float maxDistance;
    public float minMovingDistance;
    public float lookAtSpeed;

    protected override void OnStart()
    {
        base.OnStart();
        blackboard.target = GameManger.Instance.Player.transform;
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        if (blackboard.runningAbortFlag)
        {
            blackboard.runningAbortFlag = false;
            return State.Failure;
        }

        float dis = (context.transform.position - blackboard.target.position).magnitude;

        if (dis >= maxDistance)
        {
            _navMeshAgent.isStopped = true;
            return State.Running;
        }
        if (!_navMeshAgent.isStopped||(_navMeshAgent && dis <= minMovingDistance))
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.CalculatePath(blackboard.destination,_path);
        }
        else
        {
            LookAt();
        }

        if(_navMeshAgent.isPathStale) Debug.Log("Path stale");
        if (_navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            _navMeshAgent.path = _path;
        }
        else
        {
            _navMeshAgent.ResetPath();
            blackboard.destination = context.transform.position;
            Debug.LogFormat("{0}: I can not get there",context.gameObject.name);
        }
        
        return _navMeshAgent.hasPath? State.Running:State.Success;
    }

    private void LookAt()
    {
        Transform self = context.transform;
        Transform target = blackboard.target;
        Vector3 targetDir = (target.position - self.position).normalized;
        
        if (Vector3.Dot(self.forward.normalized, targetDir) > 0.99f)
        {
            self.LookAt(target.position,Vector3.up);
        }
        else
        {
            self.forward = Vector3.Lerp(self.forward.normalized, targetDir,
                lookAtSpeed * Time.deltaTime);
        }

    }
}
