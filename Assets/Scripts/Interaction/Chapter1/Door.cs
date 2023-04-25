using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public GameObject rotateObject;
    public Vector3 rotateAxis = Vector3.up;
    public float rotateSpeed;

    public float targetAngle = 0.0f;
    public float _currentAngle = 0.0f;
    public NavMeshObstacle obstacle;

    public bool Close => (_currentAngle == targetAngle) && targetAngle == 0;
    public bool Open => (_currentAngle == targetAngle) && targetAngle != 0;

    [SerializeReference]private bool _isLock = false;
    public bool Lock
    {
        get
        {
            return _isLock;
        }
        set
        {
            if (Close)
            {
                _isLock = value;
                if(obstacle) obstacle.enabled = !_isLock;
            }
        }
    }
    

    private void FixedUpdate()
    {
        float delta;
        
        Common.UniformInterpolationInValue(targetAngle, _currentAngle, rotateSpeed * Time.fixedDeltaTime, out delta);
        _currentAngle += delta;
        transform.Rotate(rotateAxis,delta);

        if (obstacle)
        {
            if (Open) obstacle.enabled = true;
            else if (Close) obstacle.enabled = _isLock;
            else obstacle.enabled = false;
        }
    }
}