using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.TextCore.Text;

public class PlayerManager : MonoBehaviour
{
    #region Player States Changer Variables

        public PlayerBaseState currentState;
        public Animator playerAnimator;
        public Rigidbody2D playerRB;
        public InputManager inputManager;
        public GameObject playerAttackInHand;
        public GameObject playerSpecialAttackObj;
        public PlayerSpinningKnifeSpawner playerSpinningKnifeSpawner;
        public GameObject inGameMenu;
        public GameObject gameOverMenu;
        public CinemachineVirtualCamera virtualCamera;
        public CarrotManager carrotManager;
        public PlayerLifeManager playerLifeManager;
        public Transform deathAnimationPoints;
        public SpriteRenderer deathSpriteRenderer;
        public Sprite deathSprite0;
        public Sprite deathSprite1;
        public int carrotQuantity = 0;
        public int hitPoints;
        public AudioSource soundEffects;
        public AudioSource backgroundSounds;
    #endregion
    #region Player Data

        public PlayerData playerData;

    #endregion
    #region Player States Instantiation

        public PlayerIdleState playerIdle = new PlayerIdleState();
        public PlayerWalkState playerWalk = new PlayerWalkState();
        public PlayerJumpState playerJump = new PlayerJumpState();
        public PlayerAttackState playerAttack = new PlayerAttackState();
        public PlayerJumpAttackState playerJumpAttack = new PlayerJumpAttackState();
        public PlayerSpecialAttackState playerSpecialAttack = new PlayerSpecialAttackState();
        public PlayerTakeDamageState playerTakeDamage = new PlayerTakeDamageState();
        public PlayerDeathState playerDeath = new PlayerDeathState();
        public PlayerEnterDoorState playerEnterDoor = new PlayerEnterDoorState();

    #endregion

    void Awake() 
    {
        ApplyGlobalDefinitions();

        currentState = playerIdle;
        currentState.isGrounded = playerData.isGroundedInitialValue;
        currentState.isInvunerable = playerData.isInvunerableInitialValue;
        currentState.invunerableTotalTimer = playerData.invunerableTimeInitialValue;
        hitPoints = playerData.hitPointsInitialValue;
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
        newState.invunerableTotalTimer = currentState.invunerableTotalTimer;
        newState.invunerableFlashTimer = currentState.invunerableFlashTimer;
        newState.spriteVisibility = currentState.spriteVisibility;
        newState.impulseWasGiven = currentState.impulseWasGiven;
        newState.pendingUpImpulse = currentState.pendingUpImpulse;
        newState.previousState = currentState.CurrentState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void TakeDamage(int damage, Vector2 knockbackDirection)
    {
        if (currentState.isInvunerable == false)
        {
            playerSpecialAttackObj.SetActive(false);
            if(hitPoints > 0)
            {
                if (hitPoints - damage <= 0)
                {
                    soundEffects.PlayOneShot(playerData.playerDeathSound);
                    hitPoints = 0;
                    SwitchState(playerDeath);
                }
                else
                {
                    soundEffects.PlayOneShot(playerData.playerTakeDamageSound);
                    hitPoints = hitPoints - damage;
                    playerLifeManager.UpdateLife(hitPoints, playerData.maxHitPointsValue);
                    playerTakeDamage.isGrounded = currentState.isGrounded;
                    playerTakeDamage.isInvunerable = true;
                    playerTakeDamage.invunerableTotalTimer = 0;
                    playerTakeDamage.invunerableFlashTimer = 0;
                    playerTakeDamage.spriteVisibility = true;
                    playerTakeDamage.knockbackDirection = knockbackDirection;
                    currentState = playerTakeDamage;
                    currentState.EnterState(this);
                }
            }
        }
    }

    public void Die()
    {
        playerSpecialAttackObj.SetActive(false);
        if(currentState != playerDeath)
        {
            soundEffects.PlayOneShot(playerData.playerDeathSound);
            hitPoints = 0;
            SwitchState(playerDeath);
        }
    }

    public void AddCarrots(int carrotsCollected)
    {
        carrotQuantity += carrotsCollected;
        if(carrotQuantity > 10)
        {
            carrotQuantity = 10;
        }
        if(carrotQuantity < 0)
        {
            carrotQuantity = 0;
        }
        soundEffects.PlayOneShot(playerData.playerTakeCarrotSound);
        carrotManager.UpdateCarrots(carrotQuantity);
    }

    public void AddLife(int lifeToAdd)
    {
        hitPoints += lifeToAdd;
        if(lifeToAdd>=0)
        {
            soundEffects.PlayOneShot(playerData.playerTakeHeartSound);
        }
        else
        {
            soundEffects.PlayOneShot(playerData.playerTakeDamageSound);
        }

        if (hitPoints > playerData.maxHitPointsValue)
        {
            hitPoints = playerData.maxHitPointsValue;
        }
        if (hitPoints < 0)
        {
            hitPoints = 0;
        }
        playerLifeManager.UpdateLife(hitPoints, playerData.maxHitPointsValue);
    }

    void ApplyGlobalDefinitions()
    {
        playerIdle.maxInvunerableTime = playerData.maxInvunerableTimeValue;

        playerWalk.maxInvunerableTime = playerData.maxInvunerableTimeValue;

        playerJump.maxInvunerableTime = playerData.maxInvunerableTimeValue;

        playerAttack.maxInvunerableTime = playerData.maxInvunerableTimeValue;

        playerSpecialAttack.maxInvunerableTime = playerData.maxInvunerableTimeValue;

        playerTakeDamage.maxInvunerableTime = playerData.maxInvunerableTimeValue;

        playerDeath.maxInvunerableTime = playerData.maxInvunerableTimeValue;
    }
}
