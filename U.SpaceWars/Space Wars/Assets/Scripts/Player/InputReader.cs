using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public static InputReader Instance { get; private set; }
    public float MoveCompostion { get; private set; }
    public event Action FireEvent;
    public event Action PauseEvent;

    Controls controls;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed) FireEvent?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        return;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveCompostion = context.ReadValue<Vector2>().x;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) PauseEvent?.Invoke();
    }
}
