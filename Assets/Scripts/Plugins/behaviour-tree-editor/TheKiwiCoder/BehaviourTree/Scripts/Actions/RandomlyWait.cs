using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {
    public class RandomlyWait : ActionNode {
        private float duration = 1;
        public float minDuration = 0f;
        public float maxDuration = 1f;
        float startTime;

        protected override void OnStart() {
            startTime = Time.time;
            duration = Random.Range(minDuration, maxDuration);
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            if (blackboard.runningAbortFlag)
            {
                blackboard.runningAbortFlag = false;
                return State.Failure;
            }

            if (Time.time - startTime > duration) {
                return State.Success;
            }
            return State.Running;
        }
    }
}
