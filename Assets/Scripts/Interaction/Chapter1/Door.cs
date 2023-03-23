using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public GameObject rotateObject;
    public Vector3 rotateAxis = Vector3.up;
    public float rotateSpeed = 350.0f;
    public float maxRotateDegree = 90.0f;

    private bool _rotating = false;
    
    [SerializeField]private bool open = false;
    [SerializeField]private float currentDegree = 0.0f;

    public bool DoorOpen => open && !_rotating;

    public bool Rotating
    {
        get => _rotating;
        private set
        {
            _rotating = value;
            if(_rotating) OnRotateStart.Invoke();
            else OnRotateEnd.Invoke();
            DoorStateChanged.Invoke(!DoorOpen);
        }
    }

    public UnityEvent<bool> DoorStateChanged;
    public UnityEvent OnRotateStart;
    public UnityEvent OnRotateEnd;
    
    private void Start()
    {
        if (rotateObject == null) rotateObject = gameObject;
        rotateAxis = rotateAxis.normalized;
    }

    public void ChangeState(bool isOpen)
    {
        if(DoorOpen!=isOpen)Rotating = true;
        open = isOpen;
    }

    private void FixedUpdate()
    {
        if (!_rotating) return;
        float rotateDegree = rotateSpeed*Time.fixedDeltaTime;
        if (open)
        {
            if (rotateDegree + currentDegree > maxRotateDegree)
            {
                rotateDegree = maxRotateDegree - currentDegree;
                Rotating = false;
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
                Rotating = false;
            }
            rotateObject.transform.Rotate(rotateAxis,rotateDegree*-1f);
            currentDegree -= rotateDegree;
        }
    }
}