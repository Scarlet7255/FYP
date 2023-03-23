using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    Enemy,NPC,Player,Toolkit
}

public class DetectionTarget : MonoBehaviour
{
    public GameObject owner;
    public TargetType type;
}
