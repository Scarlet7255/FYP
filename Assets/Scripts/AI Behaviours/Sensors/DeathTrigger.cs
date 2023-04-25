using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public float deathArcAngle;
    public float radius;
    private void Update()
    {
        Transform t = GameManger.Instance.Player.transform;
        Vector3 dir = t.position - transform.position;

        if(dir.magnitude>radius) return;
        
        if (Vector3.Dot(dir.normalized, transform.forward) >= MathF.Cos(deathArcAngle / 2f * MathF.PI/180f))
        {
            GameManger.Instance.RestartGame();
        }
    }
}
