using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private CustomInput input;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        input = new();
        
        input.Enable();
        input.Player.Enable();
    }
    
    void Update()
    {
       
    }
    
    public Vector2 Move => input.Player.Move.ReadValue<Vector2>();
    
    public float Thruster => Move.y;
    
    public float Rudder => Move.x;

    public bool IsBreaking => input.Player.Break.ReadValue<float>() != 0;
    
    public bool IsBoosted => input.Player.Boost.ReadValue<float>() != 0;
    
    public bool IsJumping => input.Player.Jump.ReadValue<float>() != 0;
}
