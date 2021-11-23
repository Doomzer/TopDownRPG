using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float[] minionsSpeed = { 2.5f, -2.5f };
    public float distance = 0.25f;
    public Transform[] minions;

    void Update()
    {
        for (int i = 0; i < minions.Length; i++)
        {
            minions[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * minionsSpeed[i]) * distance, Mathf.Sin(Time.time * minionsSpeed[i]) * distance, 0);
        }
    }
}
