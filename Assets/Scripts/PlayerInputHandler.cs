using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public float Vertical { get; private set; }
    public float Horizontal { get; private set; }
    public bool Jump { get; private set; }
    public bool Attack { get; private set; }
    public bool Run { get; private set; }

    void Update()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        Jump = Input.GetButtonDown("Jump");
        Attack = Input.GetButtonDown("Fire1");
        Run = Input.GetKey(KeyCode.LeftShift);
        if (Horizontal != 0.0f)
        {
            print("Horizontal");
        }
    }
}
