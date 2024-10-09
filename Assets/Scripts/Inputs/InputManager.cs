using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public Vector2 moveInput { get; private set; }
    public bool attackInput { get; private set; }
    public bool specialAttackInput { get; private set; }
    public bool jumpInput { get; private set; }

    private PlayerInput _playerInput;
    private InputAction _moveInputs;
    private InputAction _attackInputs;
    private InputAction _specialAttackInputs;
    private InputAction _jumpInputs;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        SetInputs();
    }

    private void SetInputs()
    {
        _moveInputs = _playerInput.actions["Walk"];
        _attackInputs = _playerInput.actions["Attack"];
        _jumpInputs = _playerInput.actions["Jump"];
        _specialAttackInputs = _playerInput.actions["AttackSpecial"];
    }

    void Update()
    {
        UpdateInputs();
    }

    private void UpdateInputs()
    {   
        if(_moveInputs != null)
        {
            moveInput = _moveInputs.ReadValue<Vector2>();
        }
        else
        {
            moveInput = Vector2.zero;
        }
        attackInput = _attackInputs.WasPressedThisFrame();
        jumpInput = _jumpInputs.WasPressedThisFrame();
        specialAttackInput = _specialAttackInputs.WasPressedThisFrame();
    }



}
