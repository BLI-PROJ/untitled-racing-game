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
        print(GetMove());
        print(IsBreaking());
        print(IsBoosted());
    }
    
    Vector2 GetMove() => input.Player.Move.ReadValue<Vector2>();
    bool IsBreaking() => input.Player.Break.ReadValue<float>() != 0;
    bool IsBoosted() => input.Player.Nitro.ReadValue<float>() != 0;
}
