using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {

    // This is the blackboard container shared between all nodes.
    // Use this to store temporary data that multiple nodes need read and write access to.
    // Add other properties here that make sense for your specific use case.
    [System.Serializable]
    public class Blackboard {
        public InteractableObject interactionTarget;
        public ViewDetector eye;
        public Transform target;
        public Vector3 destination;
        public CharacterState state;
        public AIStrategy strategy;
        public Vector3 searchDirection;
        public string area;
        public string targetName;
        public bool seeTarget;
        public bool runningAbortFlag = false;
        public string plotName;
    }
}