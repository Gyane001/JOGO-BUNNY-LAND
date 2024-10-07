using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerManager : MonoBehaviour
{
    public PlayerBaseState currentState;

    public Animator playerAnimator;

    public bool isGroundedInitialValue;
    public bool isInvunerableInitialValue;
    public float invunerableTimeInitialValue;
    public float maxInvunerableTimeValue;
    public int hitPointsInitialValue;
    public int maxHitPointsValue;

    public PlayerIdleState playerIdle = new PlayerIdleState();
    public PlayerWalkState playerWalk = new PlayerWalkState();
    public PlayerJumpState playerJump = new PlayerJumpState();
    public PlayerAttackState playerAttack = new PlayerAttackState();
    public PlayerSpecialAttackState playerSpecialAttack = new PlayerSpecialAttackState();
    public PlayerTakeDamageState playerTakeDamage = new PlayerTakeDamageState();
    public PlayerDeathState playerDeath = new PlayerDeathState();

    void Awake() 
    {
        ApplyGlobalDefinitions();

        currentState = playerIdle;
        currentState.isGrounded = isGroundedInitialValue;
        currentState.isInvunerable = isInvunerableInitialValue;
        currentState.invunerableTime = invunerableTimeInitialValue;
        currentState.hitPoints = hitPointsInitialValue;
        currentState.EnterState(this);
    }

    void Start()
    {
        
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        currentState.OnCollisionEnter2D(this,other);
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        currentState.OnCollisionStay2D(this,other);
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        currentState.OnCollisionExit2D(this,other);
    }
    
    public void SwitchState(PlayerBaseState newState)
    {
        newState.isGrounded = currentState.isGrounded;
        newState.isInvunerable = currentState.isInvunerable;
        newState.invunerableTime = currentState.invunerableTime;
        newState.hitPoints = currentState.hitPoints;
        currentState = newState;
        currentState.EnterState(this);
    }

    void ApplyGlobalDefinitions()
    {
    playerIdle.maxInvunerableTime = maxInvunerableTimeValue;
    playerIdle.maxHitPoints = maxHitPointsValue;

    playerWalk.maxInvunerableTime = maxInvunerableTimeValue;
    playerWalk.maxHitPoints = maxHitPointsValue;

    playerJump.maxInvunerableTime = maxInvunerableTimeValue;
    playerJump.maxHitPoints = maxHitPointsValue;

    playerAttack.maxInvunerableTime = maxInvunerableTimeValue;
    playerAttack.maxHitPoints = maxHitPointsValue;

    playerSpecialAttack.maxInvunerableTime = maxInvunerableTimeValue;
    playerSpecialAttack.maxHitPoints = maxHitPointsValue;

    playerTakeDamage.maxInvunerableTime = maxInvunerableTimeValue;
    playerTakeDamage.maxHitPoints = maxHitPointsValue;

    playerDeath.maxInvunerableTime = maxInvunerableTimeValue;
    playerDeath.maxHitPoints = maxHitPointsValue;
    }
}
