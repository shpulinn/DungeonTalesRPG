using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] private float[] fireballsSpeed = { 2.5f, -2.5f };
    [SerializeField] private float distance = 0.25f;
    [SerializeField] private Transform[] fireballs;

    private void Update()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            fireballs[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireballsSpeed[i]) * distance,
                Mathf.Sin(Time.time * fireballsSpeed[i]) * distance, 0);
        }
    }
}
