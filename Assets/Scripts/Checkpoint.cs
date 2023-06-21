using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint fallback = new Checkpoint
    {
        spawnPosition = new(0, 2, 0),
        spawnRotation = quaternion.Euler(0, 90, 0)
    };

    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
}
