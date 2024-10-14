using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 moveInput { get; private set; }
    public bool moveUpInput { get; private set; }
    public bool moveLeftInput { get; private set; }
    public bool moveDownInput { get; private set; }
    public bool moveRightInput { get; private set; }
    public bool attackInput { get; private set; }
    public bool specialAttackInput { get; private set; }
    public bool jumpInput { get; private set; }

    private PlayerInput _playerInput;
    private InputAction _moveInputs;
    private InputAction _moveUpInputs;
    private InputAction _moveLeftInputs;
    private InputAction _moveDownInputs;
    private InputAction _moveRightInputs;
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
        _moveUpInputs = _playerInput.actions["Walk Up"];
        _moveLeftInputs = _playerInput.actions["Walk Left"];
        _moveDownInputs = _playerInput.actions["Walk Down"];
        _moveRightInputs = _playerInput.actions["Walk Right"];
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
        moveUpInput = _moveUpInputs.IsPressed();
        moveLeftInput = _moveLeftInputs.IsPressed();
        moveDownInput = _moveDownInputs.IsPressed();
        moveRightInput = _moveRightInputs.IsPressed();

        moveInput = Vector2.zero;
        moveInput = _moveInputs.ReadValue<Vector2>();
        if(moveUpInput)
        {
            moveInput += Vector2.up;
        }
        if(moveLeftInput)
        {
            moveInput += Vector2.left;
        }
        if(moveDownInput)
        {
            moveInput += Vector2.down;
        }
        if(moveRightInput)
        {
            moveInput += Vector2.right;
        }

        attackInput = _attackInputs.WasPressedThisFrame();
        jumpInput = _jumpInputs.WasPressedThisFrame();
        specialAttackInput = _specialAttackInputs.WasPressedThisFrame();
    }



}
