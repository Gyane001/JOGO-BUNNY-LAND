using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CrowIdleState : CrowBaseState
{
    float showTime = float.PositiveInfinity;
    public override void EnterState(CrowEnemyManager crowEnemyManager)
    {
        showTime = 0;
        crowEnemyManager.questionMark.SetActive(true);
        if (crowEnemyManager.crowRB.transform.rotation.eulerAngles.y == 0)
        {
            crowEnemyManager.questionMark.transform.eulerAngles = new Vector3(0, 0);
        }
        else
        {
            crowEnemyManager.questionMark.transform.eulerAngles = new Vector3(0, 180);
        }
        crowEnemyManager.crowAnimator.Play("Idle");
    }

    public override void UpdateState(CrowEnemyManager crowEnemyManager)
    {
        InvulnerabilityManager(crowEnemyManager);
        if (showTime<crowEnemyManager.crowData.questionMarkShowTime)
        {
            showTime += Time.deltaTime;
        }
        else
        {
            crowEnemyManager.questionMark.SetActive(false);

        }
    }

    public override void FixedUpdateState(CrowEnemyManager crowEnemyManager)
    {
        crowEnemyManager.crowRB.velocity = new Vector2(0,crowEnemyManager.crowRB.velocity.y);
    }

    public override string CurrentState(CrowEnemyManager crowEnemyManager)
    {
        return "Idle";
    }

    public override void OnRangeEnter2D(CrowEnemyManager crowEnemyManager, Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            crowEnemyManager.questionMark.SetActive(false);
            crowEnemyManager.crowWalk.player = collider.gameObject;
            crowEnemyManager.SwitchState(crowEnemyManager.crowWalk);
            return;
        }
    }

    public override void OnRangeExit2D(CrowEnemyManager crowEnemyManager, Collider2D collider)
    {
        
    }

    public override void OnHitBoxEnter2D(CrowEnemyManager crowEnemyManager, Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (collider.transform.position.y > crowEnemyManager.crowHitBoxPosition.position.y)
            {
                collider.transform.GetComponent<PlayerManager>().currentState.pendingUpImpulse = true;
                if (!isInvunerable)
                {
                    crowEnemyManager.crowCurrentState.pendingKnockback = true;
                    crowEnemyManager.crowCurrentState.knockbackValue = collider.transform.GetComponent<PlayerManager>().playerData.attackKnockback;
                    crowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(crowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);
                    crowEnemyManager.TakeDamage(collider.transform.GetComponent<PlayerManager>().playerData.attackDamage);
                }
            }
            else
            {
                collider.transform.GetComponent<PlayerManager>().TakeDamage(crowEnemyManager.crowData.crowDamage, new Vector2(collider.transform.position.x - crowEnemyManager.crowHitBoxPosition.position.x, 0));
            }
        }
        if (collider.gameObject.tag == "PlayerAttack")
        {
            crowEnemyManager.crowCurrentState.pendingKnockback = true;
            crowEnemyManager.crowCurrentState.knockbackValue = collider.transform.parent.GetComponent<PlayerManager>().playerData.attackKnockback;
            crowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(crowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);
        }
        if (collider.gameObject.tag == "SpinningKnife")
        {
            crowEnemyManager.crowCurrentState.pendingKnockback = true;
            crowEnemyManager.crowCurrentState.knockbackValue = collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackKnockback;
            crowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(crowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);

            crowEnemyManager.crowRB.AddForce(new Vector2(collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackKnockback, 0), ForceMode2D.Impulse);
            crowEnemyManager.TakeDamage(collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackDamage);
            collider.transform.GetComponent<SpinningKnifeManager>().DestroyEarlier();
        }
        if (collider.gameObject.tag == "PlayerSpecialAttack" && !isInvunerable)
        {
            crowEnemyManager.crowCurrentState.pendingKnockback = true;
            crowEnemyManager.crowCurrentState.knockbackValue = collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackKnockback;
            crowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(crowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);

            //flyingCrowEnemyManager.crowRB.AddForce(new Vector2(collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackKnockback, 0), ForceMode2D.Impulse);
            crowEnemyManager.TakeDamage(collider.transform.GetComponent<PlayerManager>().playerData.specialAttackDamage);
        }
        if(collider.gameObject.tag == "DeathBarrier")
        {
            crowEnemyManager.DeactivateCrow();
        }
    }

    public override void OnHitBoxStay2D(CrowEnemyManager crowEnemyManager, Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.transform.GetComponent<PlayerManager>().TakeDamage(crowEnemyManager.crowData.crowDamage, new Vector2(collider.transform.position.x - crowEnemyManager.crowHitBoxPosition.position.x, 0));
        }
    }

    public override void InvulnerabilityManager(CrowEnemyManager crowEnemyManager)
    {
        if (isInvunerable)
        {
            invunerableFlashTimer += Time.deltaTime;
            invunerableTotalTimer += Time.deltaTime;
            if (invunerableFlashTimer >= crowEnemyManager.crowData.crowFlashTime)
            {
                if (spriteVisibility)// Toggle sprite visibility
                {
                    crowEnemyManager.crowRB.GetComponent<SpriteRenderer>().enabled = false;
                    spriteVisibility = false;
                }
                else
                {
                    crowEnemyManager.crowRB.GetComponent<SpriteRenderer>().enabled = true;
                    spriteVisibility = true;
                }
                invunerableFlashTimer = 0;
            }
            if (invunerableTotalTimer >= crowEnemyManager.crowData.crowMaxInvunerabilityTime)
            {
                crowEnemyManager.crowRB.GetComponent<SpriteRenderer>().enabled = true;
                spriteVisibility = true;
                isInvunerable = false;
            }
        }
    }
}

public class CrowWalkState : CrowBaseState
{
    public GameObject player;

    float showExclamationTime;

    bool isWalking;
    float notWalkingTime;

    public override void EnterState(CrowEnemyManager crowEnemyManager)
    {
        showExclamationTime = 0;
        crowEnemyManager.exclamation.SetActive(true);

        isWalking = false;
        notWalkingTime = 0;
        
        knockbackTimer = 0;
    }

    public override void UpdateState(CrowEnemyManager crowEnemyManager)
    {
        InvulnerabilityManager(crowEnemyManager);
        if (showExclamationTime>crowEnemyManager.crowData.exclamationShowTime)
        {
            crowEnemyManager.exclamation.SetActive(false);
        }
        else
        {
            showExclamationTime += Time.deltaTime;
        }
        if(notWalkingTime > crowEnemyManager.crowData.timeToStartWalk)
        {
            if(isWalking)
            {

            }
            else
            {
                isWalking = true;
                crowEnemyManager.crowAnimator.Play("Walk");
            }
        }
        else
        {
            notWalkingTime += Time.deltaTime;
        }
    }

    public override void FixedUpdateState(CrowEnemyManager crowEnemyManager)
    {
        if(knockbackTimer >= crowEnemyManager.crowData.knockbackTimeDuration)
        {
            if(isWalking)
            {
                var direction = new Vector2(player.transform.position.x - crowEnemyManager.crowRB.position.x,0);
                if(direction.magnitude<0.05)
                {
                    crowEnemyManager.crowRB.AddForce(new Vector2(0 - crowEnemyManager.crowRB.velocity.x, 0), ForceMode2D.Impulse);
                }
                else
                {
                    direction.Normalize();
                    if(direction.x>0)
                    {
                        crowEnemyManager.crowRB.transform.eulerAngles = new Vector3(0, 180);
                    }
                    else
                    {
                        crowEnemyManager.crowRB.transform.eulerAngles = new Vector3(0, 0);
                    }
                    crowEnemyManager.crowRB.AddForce(new Vector2(crowEnemyManager.crowData.crowSpeed*direction.x - crowEnemyManager.crowRB.velocity.x, 0), ForceMode2D.Impulse);
                }
            }
        }
        else
        {
            knockbackTimer +=Time.fixedDeltaTime;
        }
        if(pendingKnockback)
        {
            knockbackDirection.Normalize();
            knockbackDirection.x = knockbackValue * knockbackDirection.x - crowEnemyManager.crowRB.velocity.x;
            crowEnemyManager.crowRB.AddForce(knockbackDirection, ForceMode2D.Impulse);
            knockbackTimer = 0;
            pendingKnockback = false;
        }
    }

    public override string CurrentState(CrowEnemyManager crowEnemyManager)
    {
        return "Walk";
    }

    public override void OnRangeEnter2D(CrowEnemyManager crowEnemyManager, Collider2D collider)
    {

    }

    public override void OnRangeExit2D(CrowEnemyManager crowEnemyManager, Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            crowEnemyManager.exclamation.SetActive(false);
            crowEnemyManager.SwitchState(crowEnemyManager.crowIdle);
            return;
        }
    }

    public override void OnHitBoxEnter2D(CrowEnemyManager crowEnemyManager, Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if(collider.transform.position.y > crowEnemyManager.crowHitBoxPosition.position.y && !isInvunerable)
            {
                collider.transform.GetComponent<PlayerManager>().currentState.pendingUpImpulse = true;
                if (!isInvunerable)
                {
                    crowEnemyManager.crowCurrentState.pendingKnockback = true;
                    crowEnemyManager.crowCurrentState.knockbackValue = collider.transform.GetComponent<PlayerManager>().playerData.attackKnockback;
                    crowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(crowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);
                    crowEnemyManager.TakeDamage(collider.transform.GetComponent<PlayerManager>().playerData.attackDamage);
                }
            }
            else
            {
                collider.transform.GetComponent<PlayerManager>().TakeDamage(crowEnemyManager.crowData.crowDamage, new Vector2(collider.transform.position.x - crowEnemyManager.crowHitBoxPosition.position.x, 0));
            }
        }
        if(collider.gameObject.tag == "PlayerAttack" && !isInvunerable)
        {
            crowEnemyManager.crowCurrentState.pendingKnockback = true;
            crowEnemyManager.crowCurrentState.knockbackValue = collider.transform.parent.GetComponent<PlayerManager>().playerData.attackKnockback;
            crowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(crowEnemyManager.crowHitBoxPosition.position.x- collider.transform.position.x,0);
        }
        if (collider.gameObject.tag == "SpinningKnife" && !isInvunerable)
        {
            crowEnemyManager.crowCurrentState.pendingKnockback = true;
            crowEnemyManager.crowCurrentState.knockbackValue = collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackKnockback;
            crowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(crowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);

            crowEnemyManager.crowRB.AddForce(new Vector2(collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackKnockback, 0), ForceMode2D.Impulse);
            crowEnemyManager.TakeDamage(collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackDamage);
            collider.transform.GetComponent<SpinningKnifeManager>().DestroyEarlier();
        }
        if (collider.gameObject.tag == "PlayerSpecialAttack" && !isInvunerable)
        {
            crowEnemyManager.crowCurrentState.pendingKnockback = true;
            crowEnemyManager.crowCurrentState.knockbackValue = collider.transform.parent.GetComponent<PlayerManager>().playerData.attackKnockback;
            crowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(crowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);

            //flyingCrowEnemyManager.crowRB.AddForce(new Vector2(collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackKnockback, 0), ForceMode2D.Impulse);
            crowEnemyManager.TakeDamage(collider.transform.parent.GetComponent<PlayerManager>().playerData.specialAttackDamage);
        }
        if(collider.gameObject.tag == "DeathBarrier")
        {
            crowEnemyManager.DeactivateCrow();
        }
    }

    public override void OnHitBoxStay2D(CrowEnemyManager crowEnemyManager, Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.transform.GetComponent<PlayerManager>().TakeDamage(crowEnemyManager.crowData.crowDamage, new Vector2(collider.transform.position.x - crowEnemyManager.crowHitBoxPosition.position.x, 0));
        }
        
    }

    public override void InvulnerabilityManager(CrowEnemyManager crowEnemyManager)
    {
        if (isInvunerable)
        {
            invunerableFlashTimer += Time.deltaTime;
            invunerableTotalTimer += Time.deltaTime;
            if (invunerableFlashTimer >= crowEnemyManager.crowData.crowFlashTime)
            {
                if (spriteVisibility)// Toggle sprite visibility
                {
                    crowEnemyManager.crowRB.GetComponent<SpriteRenderer>().enabled = false;
                    spriteVisibility = false;
                }
                else
                {
                    crowEnemyManager.crowRB.GetComponent<SpriteRenderer>().enabled = true;
                    spriteVisibility = true;
                }
                invunerableFlashTimer = 0;
            }
            if (invunerableTotalTimer >= crowEnemyManager.crowData.crowMaxInvunerabilityTime)
            {
                crowEnemyManager.crowRB.GetComponent<SpriteRenderer>().enabled = true;
                spriteVisibility = true;
                isInvunerable = false;
            }
        }
    }
}
