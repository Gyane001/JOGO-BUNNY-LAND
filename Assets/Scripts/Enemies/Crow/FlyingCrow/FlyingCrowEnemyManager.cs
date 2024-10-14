using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCrowEnemyManager : MonoBehaviour
{
    #region Crow States Changer Variables
        public FlyingCrowBaseState crowCurrentState;
        public Animator crowAnimator;
        public Rigidbody2D crowRB;
        public GameObject exclamation;
        public GameObject questionMark;
        public int crowCurrentHP;
        public Transform crowHitBoxPosition;
    #endregion

    #region Flying Crow Data
        public FlyingCrowData flyingCrowData;
    #endregion

    #region Flying Crow States Instantiation
        public FlyingCrowIdleState crowIdle = new FlyingCrowIdleState();
        public FlyingCrowFlyState crowFly = new FlyingCrowFlyState();
    #endregion

    void Awake()
    {
        crowCurrentHP = flyingCrowData.crowMaxHP;

        crowCurrentState = crowIdle;
        //No needo to call crowCurrentState.EnterState(this), because I want a different behaviour on the first entrance
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        crowCurrentState.UpdateState(this);
    }

    void FixedUpdate()
    {
        crowCurrentState.FixedUpdateState(this);
    }

    public void OnRangeEnter2D(Collider2D other)
    {
        crowCurrentState.OnRangeEnter2D(this, other);
    }

    public void OnRangeExit2D(Collider2D other)
    {
        crowCurrentState.OnRangeExit2D(this, other);
    }

    public void OnHitBoxEnter2D(Collider2D other)
    {
        crowCurrentState.OnHitBoxEnter2D(this, other);
    }

    public void OnHitBoxStay2D(Collider2D other)
    {
        crowCurrentState.OnHitBoxStay2D(this, other);
    }

    public void SwitchState(FlyingCrowBaseState newState)
    {
        newState.isInvunerable = crowCurrentState.isInvunerable;
        newState.invunerableTotalTimer = crowCurrentState.invunerableTotalTimer;
        newState.invunerableFlashTimer = crowCurrentState.invunerableFlashTimer;
        newState.spriteVisibility = crowCurrentState.spriteVisibility;
        crowCurrentState = newState;
        crowCurrentState.EnterState(this);
    }

    public void TakeDamage(int damage)
    {
        crowCurrentHP -= damage;
        crowCurrentState.isInvunerable = true;
        crowCurrentState.invunerableTotalTimer = 0;
        crowCurrentState.invunerableFlashTimer = 0;
        crowCurrentState.spriteVisibility = true;
        if (crowCurrentHP < 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
