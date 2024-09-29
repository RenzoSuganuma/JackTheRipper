using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 入力ハンドラ
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public sealed class InputHandler : MonoBehaviour, JackTheRipper.IPlayerActions
{
    public Vector2 MoveAxis => _moveAxis;
    public Vector2 LookAxis => _lookAxis;
    public Action OnFired;

    private PlayerInput _input;
    private Vector2 _moveAxis;
    private Vector2 _lookAxis;

    private void OnEnable()
    {
        _input = GetComponent<PlayerInput>();
        _input.actions["Move"].performed += OnMove;
        _input.actions["Look"].performed += OnLook;
        _input.actions["Fire"].performed += OnFire;
    }

    private void OnGUI()
    {
        GUILayout.TextArea(
            $"move:{_moveAxis.ToString()}, look:{_lookAxis.ToString()}"
        );
    }

    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Look"].performed -= OnLook;
        _input.actions["Fire"].performed -= OnFire;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveAxis = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _lookAxis = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            Debug.Log("Fired");
            if (OnFired is not null)
            {
                OnFired.Invoke();
            }
        }
    }
}