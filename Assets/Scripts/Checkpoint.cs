using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static readonly Checkpoint fallback = new Checkpoint
    {
        spawnPosition = new(0, 2, 0),
        spawnRotation = quaternion.Euler(0, 90, 0)
    };

    public Vector3 spawnPosition;
    public Quaternion spawnRotation;
    
    private bool hasBeenSet = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            if (hasBeenSet)
            {
                return;
            }
            
            spawnPosition = player.transform.position;
            spawnRotation = player.transform.rotation;
            
            hasBeenSet = true;
        }
    }
}
