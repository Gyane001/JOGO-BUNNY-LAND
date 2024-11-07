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
        public AudioSource soundEffects;
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
        if(crowCurrentHP>0)
        {
            crowCurrentHP -= damage;

            if (crowCurrentHP <= 0)
            {
                crowCurrentState.isInvunerable = false;
                crowCurrentState.invunerableTotalTimer = 0;
                crowCurrentState.invunerableFlashTimer = 0;
                crowCurrentState.spriteVisibility = false;
                soundEffects.PlayOneShot(flyingCrowData.crowDeathSound);
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                this.gameObject.GetComponent<Animator>().enabled = false;
                this.gameObject.transform.GetChild(3).GetComponent<BoxCollider2D>().enabled = false;
                this.gameObject.transform.GetChild(2).GetComponent<BoxCollider2D>().enabled = false;
                //Invoke("DeactivateCrow",0.5f);
            }
            else
            {
                crowCurrentState.isInvunerable = true;
                crowCurrentState.invunerableTotalTimer = 0;
                crowCurrentState.invunerableFlashTimer = 0;
                crowCurrentState.spriteVisibility = true;
                soundEffects.PlayOneShot(flyingCrowData.crowTakeDamageSound);
            }
        }
    }

    public void DeactivateCrow()
    {
        this.gameObject.SetActive(false);
    }
}
