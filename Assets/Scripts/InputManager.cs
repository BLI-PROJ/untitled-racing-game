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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var move = input.Player.Move.ReadValue<Vector2>();
        var break_ = input.Player.Break.ReadValue<float>();
        var nitro = input.Player.Nitro.ReadValue<float>();

        print(move);
        print(break_);
        print(nitro);
    }
}
