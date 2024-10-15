using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCrowIdleState : FlyingCrowBaseState
{
    float showTime = float.PositiveInfinity;
    public override void EnterState(FlyingCrowEnemyManager flyingCrowEnemyManager)
    {
        showTime = 0;
        flyingCrowEnemyManager.questionMark.SetActive(true);
        if (flyingCrowEnemyManager.crowRB.transform.rotation.eulerAngles.y == 0)
        {
            flyingCrowEnemyManager.questionMark.transform.eulerAngles = new Vector3(0, 0);
        }
        else
        {
            flyingCrowEnemyManager.questionMark.transform.eulerAngles = new Vector3(0, 180);
        }
        flyingCrowEnemyManager.crowAnimator.Play("Idle");
    }

    public override void UpdateState(FlyingCrowEnemyManager flyingCrowEnemyManager)
    {
        InvulnerabilityManager(flyingCrowEnemyManager);
        if (showTime < flyingCrowEnemyManager.flyingCrowData.questionMarkShowTime)
        {
            showTime += Time.deltaTime;
        }
        else
        {
            flyingCrowEnemyManager.questionMark.SetActive(false);

        }
    }

    public override void FixedUpdateState(FlyingCrowEnemyManager flyingCrowEnemyManager)
    {
        flyingCrowEnemyManager.crowRB.velocity = new Vector2(0, flyingCrowEnemyManager.crowRB.velocity.y);
    }

    public override string CurrentState(FlyingCrowEnemyManager flyingCrowEnemyManager)
    {
        return "Idle";
    }

    public override void OnRangeEnter2D(FlyingCrowEnemyManager flyingCrowEnemyManager, Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            flyingCrowEnemyManager.questionMark.SetActive(false);
            flyingCrowEnemyManager.crowFly.player = collider.gameObject;
            flyingCrowEnemyManager.SwitchState(flyingCrowEnemyManager.crowFly);
            return;
        }
    }

    public override void OnRangeExit2D(FlyingCrowEnemyManager flyingCrowEnemyManager, Collider2D collider)
    {

    }

    public override void OnHitBoxEnter2D(FlyingCrowEnemyManager flyingCrowEnemyManager, Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (collider.transform.position.y > flyingCrowEnemyManager.crowHitBoxPosition.position.y )
            {
                collider.transform.GetComponent<PlayerManager>().currentState.pendingUpImpulse = true;
                if (!isInvunerable)
                {
                    flyingCrowEnemyManager.crowCurrentState.pendingKnockback = true;
                    flyingCrowEnemyManager.crowCurrentState.knockbackValue = collider.transform.GetComponent<PlayerManager>().playerData.attackKnockback;
                    flyingCrowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(flyingCrowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);
                    flyingCrowEnemyManager.TakeDamage(collider.transform.GetComponent<PlayerManager>().playerData.attackDamage);
                }
            }
            else
            {
                collider.transform.GetComponent<PlayerManager>().TakeDamage(flyingCrowEnemyManager.flyingCrowData.crowDamage, new Vector2(collider.transform.position.x - flyingCrowEnemyManager.crowHitBoxPosition.position.x, 0));
            }
        }
        if (collider.gameObject.tag == "PlayerAttack" && !isInvunerable)
        {
            flyingCrowEnemyManager.crowCurrentState.pendingKnockback = true;
            flyingCrowEnemyManager.crowCurrentState.knockbackValue = collider.transform.parent.GetComponent<PlayerManager>().playerData.attackKnockback;
            flyingCrowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(flyingCrowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);
        }
        if (collider.gameObject.tag == "SpinningKnife" && !isInvunerable)
        {
            flyingCrowEnemyManager.crowCurrentState.pendingKnockback = true;
            flyingCrowEnemyManager.crowCurrentState.knockbackValue = collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackKnockback;
            flyingCrowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(flyingCrowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);

            //flyingCrowEnemyManager.crowRB.AddForce(new Vector2(collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackKnockback, 0), ForceMode2D.Impulse);
            flyingCrowEnemyManager.TakeDamage(collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackDamage);
            collider.transform.GetComponent<SpinningKnifeManager>().DestroyEarlier();
        }
        if (collider.gameObject.tag == "PlayerSpecialAttack" && !isInvunerable)
        {
            flyingCrowEnemyManager.crowCurrentState.pendingKnockback = true;
            flyingCrowEnemyManager.crowCurrentState.knockbackValue = collider.transform.GetComponent<PlayerManager>().playerData.attackKnockback;
            flyingCrowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(flyingCrowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);

            //flyingCrowEnemyManager.crowRB.AddForce(new Vector2(collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackKnockback, 0), ForceMode2D.Impulse);
            flyingCrowEnemyManager.TakeDamage(collider.transform.GetComponent<PlayerManager>().playerData.specialAttackDamage);
        }
        if(collider.gameObject.tag == "DeathBarrier")
        {
            flyingCrowEnemyManager.DeactivateCrow();
        }
    }

    public override void OnHitBoxStay2D(FlyingCrowEnemyManager flyingCrowEnemyManager, Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.transform.GetComponent<PlayerManager>().TakeDamage(flyingCrowEnemyManager.flyingCrowData.crowDamage, new Vector2(collider.transform.position.x - flyingCrowEnemyManager.crowHitBoxPosition.position.x, 0));
        }
    }

    public override void InvulnerabilityManager(FlyingCrowEnemyManager flyingCrowEnemyManager)
    {
        if (isInvunerable)
        {
            invunerableFlashTimer += Time.deltaTime;
            invunerableTotalTimer += Time.deltaTime;
            if (invunerableFlashTimer >= flyingCrowEnemyManager.flyingCrowData.crowFlashTime)
            {
                if (spriteVisibility)// Toggle sprite visibility
                {
                    flyingCrowEnemyManager.crowRB.GetComponent<SpriteRenderer>().enabled = false;
                    spriteVisibility = false;
                }
                else
                {
                    flyingCrowEnemyManager.crowRB.GetComponent<SpriteRenderer>().enabled = true;
                    spriteVisibility = true;
                }
                invunerableFlashTimer = 0;
            }
            if (invunerableTotalTimer >= flyingCrowEnemyManager.flyingCrowData.crowMaxInvunerabilityTime)
            {
                flyingCrowEnemyManager.crowRB.GetComponent<SpriteRenderer>().enabled = true;
                spriteVisibility = true;
                isInvunerable = false;
            }
        }
    }
}
public class FlyingCrowFlyState : FlyingCrowBaseState
{
    public GameObject player;

    float showExclamationTime;

    bool isWalking;
    float notWalkingTime;

    public override void EnterState(FlyingCrowEnemyManager flyingCrowEnemyManager)
    {
        showExclamationTime = 0;
        flyingCrowEnemyManager.exclamation.SetActive(true);

        isWalking = false;
        notWalkingTime = 0;

        knockbackTimer = 0;
    }

    public override void UpdateState(FlyingCrowEnemyManager flyingCrowEnemyManager)
    {
        InvulnerabilityManager(flyingCrowEnemyManager);
        if (showExclamationTime > flyingCrowEnemyManager.flyingCrowData.exclamationShowTime)
        {
            flyingCrowEnemyManager.exclamation.SetActive(false);
        }
        else
        {
            showExclamationTime += Time.deltaTime;
        }
        if (notWalkingTime > flyingCrowEnemyManager.flyingCrowData.timeToStartFlying)
        {
            if (isWalking)
            {

            }
            else
            {
                isWalking = true;
                flyingCrowEnemyManager.crowAnimator.Play("Flying");
            }
        }
        else
        {
            notWalkingTime += Time.deltaTime;
        }
    }

    public override void FixedUpdateState(FlyingCrowEnemyManager flyingCrowEnemyManager)
    {
        if (knockbackTimer >= flyingCrowEnemyManager.flyingCrowData.knockbackTimeDuration)
        {
            if (isWalking)
            {
                var direction = new Vector2(player.transform.position.x - flyingCrowEnemyManager.crowRB.position.x, player.transform.position.y - flyingCrowEnemyManager.crowRB.position.y);
                if (direction.magnitude < 0.05)
                {
                    flyingCrowEnemyManager.crowRB.AddForce(new Vector2(0 - flyingCrowEnemyManager.crowRB.velocity.x, 0 - flyingCrowEnemyManager.crowRB.velocity.y), ForceMode2D.Impulse);
                }
                else
                {
                    direction.Normalize();
                    if (direction.x > 0)
                    {
                        flyingCrowEnemyManager.crowRB.transform.eulerAngles = new Vector3(0, 180);
                    }
                    else
                    {
                        flyingCrowEnemyManager.crowRB.transform.eulerAngles = new Vector3(0, 0);
                    }
                    var crowHorizontalSpeed = direction.x * flyingCrowEnemyManager.flyingCrowData.crowSpeed;
                    crowHorizontalSpeed = Mathf.Abs(crowHorizontalSpeed);
                    var crowVerticalSpeed = direction.y * flyingCrowEnemyManager.flyingCrowData.crowSpeed;
                    crowVerticalSpeed = Mathf.Abs(crowVerticalSpeed);
                    flyingCrowEnemyManager.crowRB.AddForce(new Vector2(crowHorizontalSpeed * direction.x - flyingCrowEnemyManager.crowRB.velocity.x, crowVerticalSpeed * direction.y - flyingCrowEnemyManager.crowRB.velocity.y), ForceMode2D.Impulse);
                }
            }
        }
        else
        {
            knockbackTimer += Time.fixedDeltaTime;
        }
        if (pendingKnockback)
        {
            knockbackDirection.Normalize();
            knockbackDirection.x = knockbackValue * knockbackDirection.x - flyingCrowEnemyManager.crowRB.velocity.x;
            flyingCrowEnemyManager.crowRB.AddForce(knockbackDirection, ForceMode2D.Impulse);
            knockbackTimer = 0;
            pendingKnockback = false;
        }
    }

    public override string CurrentState(FlyingCrowEnemyManager flyingCrowEnemyManager)
    {
        return "Flying";
    }

    public override void OnRangeEnter2D(FlyingCrowEnemyManager flyingCrowEnemyManager, Collider2D collider)
    {

    }

    public override void OnRangeExit2D(FlyingCrowEnemyManager flyingCrowEnemyManager, Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            flyingCrowEnemyManager.exclamation.SetActive(false);
            flyingCrowEnemyManager.SwitchState(flyingCrowEnemyManager.crowIdle);
            return;
        }
    }

    public override void OnHitBoxEnter2D(FlyingCrowEnemyManager flyingCrowEnemyManager, Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (collider.transform.position.y > flyingCrowEnemyManager.crowHitBoxPosition.position.y && !isInvunerable)
            {
                collider.transform.GetComponent<PlayerManager>().currentState.pendingUpImpulse = true;
                if (!isInvunerable)
                {
                    flyingCrowEnemyManager.crowCurrentState.pendingKnockback = true;
                    flyingCrowEnemyManager.crowCurrentState.knockbackValue = collider.transform.GetComponent<PlayerManager>().playerData.attackKnockback;
                    flyingCrowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(flyingCrowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);
                    flyingCrowEnemyManager.TakeDamage(collider.transform.GetComponent<PlayerManager>().playerData.attackDamage);
                }
            }
            else
            {
                collider.transform.GetComponent<PlayerManager>().TakeDamage(flyingCrowEnemyManager.flyingCrowData.crowDamage, new Vector2(collider.transform.position.x - flyingCrowEnemyManager.crowHitBoxPosition.position.x, 0));
            }
        }
        if (collider.gameObject.tag == "PlayerAttack" && !isInvunerable)
        {
            flyingCrowEnemyManager.crowCurrentState.pendingKnockback = true;
            flyingCrowEnemyManager.crowCurrentState.knockbackValue = collider.transform.parent.GetComponent<PlayerManager>().playerData.attackKnockback;
            flyingCrowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(flyingCrowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);
        }
        if (collider.gameObject.tag == "SpinningKnife" && !isInvunerable)
        {
            flyingCrowEnemyManager.crowCurrentState.pendingKnockback = true;
            flyingCrowEnemyManager.crowCurrentState.knockbackValue = collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackKnockback;
            flyingCrowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(flyingCrowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);

            //flyingCrowEnemyManager.crowRB.AddForce(new Vector2(collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackKnockback, 0), ForceMode2D.Impulse);
            flyingCrowEnemyManager.TakeDamage(collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackDamage);
            collider.transform.GetComponent<SpinningKnifeManager>().DestroyEarlier();
        }
        if (collider.gameObject.tag == "PlayerSpecialAttack" && !isInvunerable)
        {
            flyingCrowEnemyManager.crowCurrentState.pendingKnockback = true;
            flyingCrowEnemyManager.crowCurrentState.knockbackValue = collider.transform.GetComponent<PlayerManager>().playerData.attackKnockback;
            flyingCrowEnemyManager.crowCurrentState.knockbackDirection = new Vector2(flyingCrowEnemyManager.crowHitBoxPosition.position.x - collider.transform.position.x, 0);

            //flyingCrowEnemyManager.crowRB.AddForce(new Vector2(collider.transform.GetComponent<SpinningKnifeManager>().playerData.attackKnockback, 0), ForceMode2D.Impulse);
            flyingCrowEnemyManager.TakeDamage(collider.transform.GetComponent<PlayerManager>().playerData.specialAttackDamage);
        }
        if(collider.gameObject.tag == "DeathBarrier")
        {
            flyingCrowEnemyManager.DeactivateCrow();
        }
    }

    public override void OnHitBoxStay2D(FlyingCrowEnemyManager flyingCrowEnemyManager, Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.transform.GetComponent<PlayerManager>().TakeDamage(flyingCrowEnemyManager.flyingCrowData.crowDamage, new Vector2(collider.transform.position.x - flyingCrowEnemyManager.crowHitBoxPosition.position.x, 0));
        }
    }

    public override void InvulnerabilityManager(FlyingCrowEnemyManager flyingCrowEnemyManager)
    {
        if (isInvunerable)
        {
            invunerableFlashTimer += Time.deltaTime;
            invunerableTotalTimer += Time.deltaTime;
            if (invunerableFlashTimer >= flyingCrowEnemyManager.flyingCrowData.crowFlashTime)
            {
                if (spriteVisibility)// Toggle sprite visibility
                {
                    flyingCrowEnemyManager.crowRB.GetComponent<SpriteRenderer>().enabled = false;
                    spriteVisibility = false;
                }
                else
                {
                    flyingCrowEnemyManager.crowRB.GetComponent<SpriteRenderer>().enabled = true;
                    spriteVisibility = true;
                }
                invunerableFlashTimer = 0;
            }
            if (invunerableTotalTimer >= flyingCrowEnemyManager.flyingCrowData.crowMaxInvunerabilityTime)
            {
                flyingCrowEnemyManager.crowRB.GetComponent<SpriteRenderer>().enabled = true;
                spriteVisibility = true;
                isInvunerable = false;
            }
        }
    }
}