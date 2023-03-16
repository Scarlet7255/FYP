using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public GameObject rotateObject;
    public Vector3 rotateAxis = Vector3.up;
    public float rotateSpeed = 350.0f;
    public float maxRotateDegree = 90.0f;
    
    private bool _rotating = false;
    public Collider targetCollider;
    [SerializeField]private bool open = false;
    [SerializeField]private float currentDegree = 0.0f;

    public bool DoorOpen
    {
        get { return open && !_rotating; }
    }

    public UnityEvent<bool> DoorStateChanged;

    private void Start()
    {
        if (rotateObject == null) rotateObject = gameObject;
        rotateAxis = rotateAxis.normalized;
    }

    public void ChangeState(bool isOpen)
    {
        _rotating = true;
        if (open != isOpen)
        {
            _rotating = true;
            open = isOpen;
        }
    }

    private void FixedUpdate()
    {
        if (!_rotating && targetCollider)
        {
            targetCollider.enabled = true;
            return;
        }

        if(targetCollider) targetCollider.enabled = false;
        float rotateDegree = rotateSpeed*Time.fixedDeltaTime;
        if (open)
        {
            if (rotateDegree + currentDegree > maxRotateDegree)
            {
                rotateDegree = maxRotateDegree - currentDegree;
                DoorStateChanged.Invoke(true);
                _rotating = false;
            }
            rotateObject.transform.Rotate(rotateAxis,rotateDegree);
            currentDegree += rotateDegree;
        }
        else
        {
            if (currentDegree < rotateDegree)
            {
                rotateDegree = currentDegree;
                DoorStateChanged.Invoke(false);
                _rotating = false;
            }
            rotateObject.transform.Rotate(rotateAxis,rotateDegree*-1f);
            currentDegree -= rotateDegree;
        }
    }
}