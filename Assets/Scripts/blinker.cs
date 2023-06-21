using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinker : MonoBehaviour
{

    Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        InvokeRepeating("Blink", 0, 0.4f);
    }


    void Blink()
    {
        renderer.enabled = !renderer.enabled;
        
    }

}
