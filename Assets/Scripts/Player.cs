using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Checkpoint lastCheckpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Checkpoint currentCheckpoint))
        {
            lastCheckpoint = currentCheckpoint;
        }
    }
    
    public void Respawn()
    {
        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        
        if (lastCheckpoint == null)
        {
            transform.position = Checkpoint.fallback.spawnPosition;
            transform.rotation = Checkpoint.fallback.spawnRotation;
            
            return;
        }
        
        transform.position = lastCheckpoint.spawnPosition;
        transform.rotation = lastCheckpoint.spawnRotation;
    }

    private void FixedUpdate()
    {
        if (InputManager.Instance.IsRespawning)
        {
            Respawn();
        }
    }
}
