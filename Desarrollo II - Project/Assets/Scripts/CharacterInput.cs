using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HeadBobbing))]
[RequireComponent(typeof(FirstPersonController))]
public class CharacterInput : MonoBehaviour
{
    public event Action OnInteractButtonPressed;

    private HeadBobbing _headBobbingRef;
    private FirstPersonController _firstPersonControllerRef;

    private void Start()
    {
        _headBobbingRef = GetComponent<HeadBobbing>();
        _firstPersonControllerRef = GetComponent<FirstPersonController>();
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) { _firstPersonControllerRef.Move(); } 

        if (Input.GetButton("Run")) { _firstPersonControllerRef.IsRunning = true; }
        else { _firstPersonControllerRef.IsRunning = false; }

        if (Input.GetButtonDown("Jump")) { _firstPersonControllerRef.Jump(); }

        if (Input.GetButtonDown("Fire1")) { OnInteractButtonPressed.Invoke(); }
    }
}
