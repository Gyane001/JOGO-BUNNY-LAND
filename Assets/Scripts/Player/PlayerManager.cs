using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerManager : MonoBehaviour
{
    #region Player States Changer Variables

        public PlayerBaseState currentState;
        public Animator playerAnimator;
        public Rigidbody2D playerRB;
        public InputManager inputManager;
        public GameObject attack;
        public GameObject inGameMenu;
        public GameObject gameOverMenu;
        public CinemachineVirtualCamera virtualCamera;
        public CarrotManager carrotManager;
        public PlayerLifeManager playerLifeManager;
        public Transform deathAnimationPoints;
        public SpriteRenderer deathSpriteRenderer;
        public Sprite deathSprite0;
        public Sprite deathSprite1;
        public float attackAnimationTotalTime;
        public float attackAnimationSwitchSpriteTime;

    #endregion
    #region Player Data

        public PlayerData playerData;

    #endregion
    #region Player States Instantiation

        public PlayerIdleState playerIdle = new PlayerIdleState();
        public PlayerWalkState playerWalk = new PlayerWalkState();
        public PlayerJumpState playerJump = new PlayerJumpState();
        public PlayerAttackState playerAttack = new PlayerAttackState();
        public PlayerSpecialAttackState playerSpecialAttack = new PlayerSpecialAttackState();
        public PlayerTakeDamageState playerTakeDamage = new PlayerTakeDamageState();
        public PlayerDeathState playerDeath = new PlayerDeathState();

    #endregion

    void Awake() 
    {
        ApplyGlobalDefinitions();

        currentState = playerIdle;
        currentState.isGrounded = playerData.isGroundedInitialValue;
        currentState.isInvunerable = playerData.isInvunerableInitialValue;
        currentState.invunerableTime = playerData.invunerableTimeInitialValue;
        currentState.hitPoints = playerData.hitPointsInitialValue;
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

    public void TakeDamage(int damage, Vector2 knockbackDirection)
    {
        if(currentState.hitPoints - damage <= 0)
        {
            playerDeath.hitPoints = 0;
            SwitchState(playerDeath);
        }
        else
        {
            playerTakeDamage.isGrounded = currentState.isGrounded;
            playerTakeDamage.isInvunerable = true;
            playerTakeDamage.invunerableTime = currentState.invunerableTime;
            playerTakeDamage.hitPoints = currentState.hitPoints - damage;
            
            playerTakeDamage.knockbackDirection = knockbackDirection;
            SwitchState(playerTakeDamage);
        }
    }

    void ApplyGlobalDefinitions()
    {
    playerIdle.maxInvunerableTime = playerData.maxInvunerableTimeValue;
    playerIdle.maxHitPoints = playerData.maxHitPointsValue;

    playerWalk.maxInvunerableTime = playerData.maxInvunerableTimeValue;
    playerWalk.maxHitPoints = playerData.maxHitPointsValue;

    playerJump.maxInvunerableTime = playerData.maxInvunerableTimeValue;
    playerJump.maxHitPoints = playerData.maxHitPointsValue;

    playerAttack.maxInvunerableTime = playerData.maxInvunerableTimeValue;
    playerAttack.maxHitPoints = playerData.maxHitPointsValue;

    playerSpecialAttack.maxInvunerableTime = playerData.maxInvunerableTimeValue;
    playerSpecialAttack.maxHitPoints = playerData.maxHitPointsValue;

    playerTakeDamage.maxInvunerableTime = playerData.maxInvunerableTimeValue;
    playerTakeDamage.maxHitPoints = playerData.maxHitPointsValue;

    playerDeath.maxInvunerableTime = playerData.maxInvunerableTimeValue;
    playerDeath.maxHitPoints = playerData.maxHitPointsValue;
    }
}
