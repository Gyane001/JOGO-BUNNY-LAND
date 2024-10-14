using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.playerAnimator.Play("Idle");
        playerManager.playerRB.velocity = new Vector2(0, playerManager.playerRB.velocity.y);
    }

    public override void UpdateState(PlayerManager playerManager)
    {
        InvulnerabilityManager(playerManager);
        if (playerManager.inputManager.jumpInput)
        {
            playerManager.SwitchState(playerManager.playerJump);
            return;
        }
        if(playerManager.inputManager.specialAttackInput && playerManager.carrotQuantity >=5)
        {
            playerManager.SwitchState(playerManager.playerSpecialAttack);
            playerManager.AddCarrots(-5);
            return;
        }
        if(playerManager.inputManager.attackInput)
        {
            playerManager.SwitchState(playerManager.playerAttack);
            return;
        }
        if(playerManager.inputManager.moveInput.x != 0)
        {
            playerManager.SwitchState(playerManager.playerWalk);
            return;
        }
    }

    public override void FixedUpdateState(PlayerManager playerManager)
    {
        MovePlayer(playerManager);
    }

    public override string CurrentState(PlayerManager playerManager)
    {
        return "Idle";
    }

    public override void OnCollisionEnter2D(PlayerManager playerManager, Collision2D collider)
    {
        if(collider.gameObject.tag == "DeathBarrier")
        {
            playerManager.SwitchState(playerManager.playerDeath);
        }
    }

    public override void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    void MovePlayer(PlayerManager playerManager)
    {
        float xImpulse=0;
        if(playerManager.inputManager.moveInput.x > 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 0);
            xImpulse = playerManager.playerData.playerSpeed - playerManager.playerRB.velocity.x;
        }
        else if (playerManager.inputManager.moveInput.x < 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 180);
            xImpulse = -playerManager.playerData.playerSpeed - playerManager.playerRB.velocity.x;
        }
        else if(playerManager.inputManager.moveInput.x == 0)
        {
            xImpulse = 0 - playerManager.playerRB.velocity.x;
        }

        playerManager.playerRB.AddForce(new Vector2(xImpulse, 0), ForceMode2D.Impulse);
    }

    public override void InvulnerabilityManager(PlayerManager playerManager)
    {
        if (isInvunerable)
        {
            invunerableFlashTimer += Time.deltaTime;
            invunerableTotalTimer += Time.deltaTime;
            if (invunerableFlashTimer >= playerManager.playerData.invunerabilityFlashTime)
            {
                if (spriteVisibility)// Toggle sprite visibility
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = false;
                    spriteVisibility = false;
                }
                else
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                    spriteVisibility = true;
                }
                invunerableFlashTimer = 0;
            }
            if (invunerableTotalTimer >= playerManager.playerData.maxInvunerableTimeValue)
            {
                playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                spriteVisibility = true;
                isInvunerable = false;
            }
        }
    }
}

public class PlayerWalkState : PlayerBaseState
{
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.playerAnimator.Play("Walk");
    }

    public override void UpdateState(PlayerManager playerManager)
    {
        InvulnerabilityManager(playerManager);
        if (playerManager.inputManager.jumpInput)
        {
            playerManager.SwitchState(playerManager.playerJump);
            return;
        }
        if (playerManager.inputManager.specialAttackInput && playerManager.carrotQuantity >= 5)
        {
            playerManager.SwitchState(playerManager.playerSpecialAttack);
            playerManager.AddCarrots(-5);
            return;
        }
        if (playerManager.inputManager.attackInput)
        {
            playerManager.SwitchState(playerManager.playerAttack);
            return;
        }
        if(playerManager.inputManager.moveInput.x == 0)
        {
            playerManager.SwitchState(playerManager.playerIdle);
            return;
        }
    }

    public override void FixedUpdateState(PlayerManager playerManager)
    {
        if(playerManager.inputManager.moveInput.x == 0)
        {
            playerManager.SwitchState(playerManager.playerIdle);
            return;
        }
        MovePlayer(playerManager);
    }

    public override string CurrentState(PlayerManager playerManager)
    {
        return "Walk";
    }

    public override void OnCollisionEnter2D(PlayerManager playerManager, Collision2D collider)
    {
        if(collider.gameObject.tag == "DeathBarrier")
        {
            playerManager.SwitchState(playerManager.playerDeath);
        }
    }

    public override void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    void MovePlayer(PlayerManager playerManager)
    {
        float xImpulse=0;
        if(playerManager.inputManager.moveInput.x > 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 0);
            xImpulse = playerManager.playerData.playerSpeed - playerManager.playerRB.velocity.x;
        }
        else if (playerManager.inputManager.moveInput.x < 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 180);
            xImpulse = -playerManager.playerData.playerSpeed - playerManager.playerRB.velocity.x;
        }
        else if(playerManager.inputManager.moveInput.x == 0)
        {
            xImpulse = 0 - playerManager.playerRB.velocity.x;
        }

        playerManager.playerRB.AddForce(new Vector2(xImpulse, 0), ForceMode2D.Impulse);
    }

    public override void InvulnerabilityManager(PlayerManager playerManager)
    {
        if (isInvunerable)
        {
            invunerableFlashTimer += Time.deltaTime;
            invunerableTotalTimer += Time.deltaTime;
            if (invunerableFlashTimer >= playerManager.playerData.invunerabilityFlashTime)
            {
                if (spriteVisibility)// Toggle sprite visibility
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = false;
                    spriteVisibility = false;
                }
                else
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                    spriteVisibility = true;
                }
                invunerableFlashTimer = 0;
            }
            if (invunerableTotalTimer >= playerManager.playerData.maxInvunerableTimeValue)
            {
                playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                spriteVisibility = true;
                isInvunerable = false;
            }
        }
    }
}

public class PlayerJumpState : PlayerBaseState
{   
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.playerAnimator.Play("Jump");
        if(previousState != "JumpAttack")
        { 
            impulseWasGiven = false;
        }
    }

    public override void UpdateState(PlayerManager playerManager)
    {
        InvulnerabilityManager(playerManager);
        if (playerManager.inputManager.attackInput)
        {
            playerManager.SwitchState(playerManager.playerJumpAttack);
            return;
        }
    }

    public override void FixedUpdateState(PlayerManager playerManager)
    {
        if(!impulseWasGiven)
        {
            playerManager.playerRB.AddForce(new Vector2(0, playerManager.playerData.jumpForce), ForceMode2D.Impulse);
            impulseWasGiven = true;
            isGrounded = false;
            UpImpulse(playerManager);
        }
        else if(pendingUpImpulse)
        {
            playerManager.playerRB.AddForce(new Vector2(0, 2*playerManager.playerData.jumpForce), ForceMode2D.Impulse);
            pendingUpImpulse = false;
            UpImpulse(playerManager);
        }
        MovePlayer(playerManager);
    }

    public override string CurrentState(PlayerManager playerManager)
    {
        return "Jump";
    }

    public override void OnCollisionEnter2D(PlayerManager playerManager, Collision2D collider)
    {   
        if(collider.gameObject.tag == "DeathBarrier")
        {
            playerManager.SwitchState(playerManager.playerDeath);
        }
        
        foreach (ContactPoint2D contactPoint in collider.contacts)
        {
            if(collider.gameObject.tag == "ground" && contactPoint.normal.y > 0.5f)
            {
                isGrounded = true;
                if(playerManager.inputManager.moveInput.x != 0)
                {
                    playerManager.SwitchState(playerManager.playerWalk);
                    return;
                }
                else
                {
                    playerManager.SwitchState(playerManager.playerIdle);
                    return;
                }
            }
        }
    }

    public override void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    void MovePlayer(PlayerManager playerManager)
    {
        float xImpulse=0;
        if(playerManager.inputManager.moveInput.x > 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 0);
            xImpulse = playerManager.playerData.playerSpeed - playerManager.playerRB.velocity.x;
        }
        else if (playerManager.inputManager.moveInput.x < 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 180);
            xImpulse = -playerManager.playerData.playerSpeed - playerManager.playerRB.velocity.x;
        }
        else if(playerManager.inputManager.moveInput.x == 0)
        {
            xImpulse = 0 - playerManager.playerRB.velocity.x;
        }

        playerManager.playerRB.AddForce(new Vector2(xImpulse, 0), ForceMode2D.Impulse);
    }

    void UpImpulse(PlayerManager playerManager)
    {
        float yImpulse;
        yImpulse = playerManager.playerData.jumpForce - playerManager.playerRB.velocity.y;

        playerManager.playerRB.AddForce(new Vector2(0, yImpulse), ForceMode2D.Impulse);
    }

    public override void InvulnerabilityManager(PlayerManager playerManager)
    {
        if (isInvunerable)
        {
            invunerableFlashTimer += Time.deltaTime;
            invunerableTotalTimer += Time.deltaTime;
            if (invunerableFlashTimer >= playerManager.playerData.invunerabilityFlashTime)
            {
                if (spriteVisibility)// Toggle sprite visibility
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = false;
                    spriteVisibility = false;
                }
                else
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                    spriteVisibility = true;
                }
                invunerableFlashTimer = 0;
            }
            if (invunerableTotalTimer >= playerManager.playerData.maxInvunerableTimeValue)
            {
                playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                spriteVisibility = true;
                isInvunerable = false;
            }
        }
    }
}

public class PlayerAttackState : PlayerBaseState
{
    float timer;
    float attackAnimationTotalTime;
    float attackAnimationSpriteChangeTime;
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.playerAnimator.Play("Attack");
        foreach (var clip in playerManager.playerAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Attack")
            {
                attackAnimationTotalTime = clip.length;
            }
        }
        timer = 0;
        attackAnimationSpriteChangeTime = attackAnimationTotalTime / 2;
        playerManager.playerAttackInHand.SetActive(true);
        playerManager.playerAttackInHand.GetComponent<BoxCollider2D>().offset = new Vector2(4.5f, 1.25f);
        playerManager.playerAttackInHand.GetComponent<BoxCollider2D>().size = new Vector2(2f, 5f);
    }

    public override void UpdateState(PlayerManager playerManager)
    {
        InvulnerabilityManager(playerManager);
        if (timer >= attackAnimationSpriteChangeTime)
        {
            playerManager.playerAttackInHand.GetComponent<BoxCollider2D>().offset = new Vector2(5f, -2.75f);
            playerManager.playerAttackInHand.GetComponent<BoxCollider2D>().size = new Vector2(5.5f, 1.75f);
        }
        if(timer >= attackAnimationTotalTime)
        {
            playerManager.playerAttackInHand.SetActive(false);
            var playerAttackInHandTransformPosition = playerManager.playerAttackInHand.transform.position;
            if (playerManager.playerRB.transform.rotation.eulerAngles.y > -1 && playerManager.playerRB.transform.rotation.eulerAngles.y < 1)
            {
                playerAttackInHandTransformPosition.x += 1;
            }
            else
            {
                playerAttackInHandTransformPosition.x -= 1;
            }
            playerAttackInHandTransformPosition.y -= 0.25f;
            playerManager.playerSpinningKnifeSpawner.spawnSpinningKnife(playerAttackInHandTransformPosition, playerManager.gameObject.transform.rotation.y);
            if(playerManager.inputManager.jumpInput)
            {
                playerManager.SwitchState(playerManager.playerJump);
                return;
            }
            if(playerManager.inputManager.moveInput.x != 0)
            {
                playerManager.SwitchState(playerManager.playerWalk);
                return;
            }
            else
            {
                playerManager.SwitchState(playerManager.playerIdle);
                return;
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public override void FixedUpdateState(PlayerManager playerManager)
    {
        MovePlayer(playerManager);
    }

    public override string CurrentState(PlayerManager playerManager)
    {
        return "Attack";
    }

    public override void OnCollisionEnter2D(PlayerManager playerManager, Collision2D collider)
    {
        if(collider.gameObject.tag == "DeathBarrier")
        {
            playerManager.SwitchState(playerManager.playerDeath);
        }
    }

    public override void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    void MovePlayer(PlayerManager playerManager)
    {
        float xImpulse=0;
        if(playerManager.inputManager.moveInput.x > 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 0);
            xImpulse = playerManager.playerData.playerSpeed - playerManager.playerRB.velocity.x;
        }
        else if (playerManager.inputManager.moveInput.x < 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 180);
            xImpulse = -playerManager.playerData.playerSpeed - playerManager.playerRB.velocity.x;
        }
        else if(playerManager.inputManager.moveInput.x == 0)
        {
            xImpulse = 0 - playerManager.playerRB.velocity.x;
        }

        playerManager.playerRB.AddForce(new Vector2(xImpulse, 0), ForceMode2D.Impulse);
    }

    public override void InvulnerabilityManager(PlayerManager playerManager)
    {
        if (isInvunerable)
        {
            invunerableFlashTimer += Time.deltaTime;
            invunerableTotalTimer += Time.deltaTime;
            if (invunerableFlashTimer >= playerManager.playerData.invunerabilityFlashTime)
            {
                if (spriteVisibility)// Toggle sprite visibility
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = false;
                    spriteVisibility = false;
                }
                else
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                    spriteVisibility = true;
                }
                invunerableFlashTimer = 0;
            }
            if (invunerableTotalTimer >= playerManager.playerData.maxInvunerableTimeValue)
            {
                playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                spriteVisibility = true;
                isInvunerable = false;
            }
        }
    }
}

public class PlayerJumpAttackState : PlayerBaseState
{
    float timer;
    float attackAnimationTotalTime;
    float attackAnimationSpriteChangeTime;
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.playerAnimator.Play("JumpAttack");
        timer = 0;
        foreach (var clip in playerManager.playerAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "JumpAttack")
            {
                attackAnimationTotalTime = clip.length;
            }
        }
        attackAnimationSpriteChangeTime = attackAnimationTotalTime / 2;
        playerManager.playerAttackInHand.SetActive(true);
        playerManager.playerAttackInHand.GetComponent<BoxCollider2D>().offset = new Vector2(4.5f, 1.25f);
        playerManager.playerAttackInHand.GetComponent<BoxCollider2D>().size = new Vector2(2f, 5f);
    }

    public override void UpdateState(PlayerManager playerManager)
    {
        InvulnerabilityManager(playerManager);
        if (timer >= playerManager.playerAnimator.GetCurrentAnimatorStateInfo(0).length)
        {
            playerManager.SwitchState(playerManager.playerJump);
            var playerAttackInHandTransformPosition = playerManager.playerAttackInHand.transform.position;
            if(playerManager.playerRB.transform.rotation.eulerAngles.y > -1 && playerManager.playerRB.transform.rotation.eulerAngles.y < 1)
            {
                playerAttackInHandTransformPosition.x += 1;
            }
            else
            {
                playerAttackInHandTransformPosition.x -= 1;
            }
            playerAttackInHandTransformPosition.y -= 0.25f;
            playerManager.playerSpinningKnifeSpawner.spawnSpinningKnife(playerAttackInHandTransformPosition, playerManager.gameObject.transform.rotation.y);
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public override void FixedUpdateState(PlayerManager playerManager)
    {
        if (!impulseWasGiven)
        {
            playerManager.playerRB.AddForce(new Vector2(0, playerManager.playerData.jumpForce), ForceMode2D.Impulse);
            impulseWasGiven = true;
            isGrounded = false;
            UpImpulse(playerManager);
        }
        else if (pendingUpImpulse)
        {
            playerManager.playerRB.AddForce(new Vector2(0, 2 * playerManager.playerData.jumpForce), ForceMode2D.Impulse);
            pendingUpImpulse = false;
            UpImpulse(playerManager);
        }
        MovePlayer(playerManager);
    }

    public override string CurrentState(PlayerManager playerManager)
    {
        return "JumpAttack";
    }

    public override void OnCollisionEnter2D(PlayerManager playerManager, Collision2D collider)
    {
        if (collider.gameObject.tag == "DeathBarrier")
        {
            playerManager.SwitchState(playerManager.playerDeath);
        }

        foreach (ContactPoint2D contactPoint in collider.contacts)
        {
            if (collider.gameObject.tag == "ground" && contactPoint.normal.y > 0.5f)
            {
                isGrounded = true;
                playerManager.SwitchState(playerManager.playerAttack);
            }
        }
    }

    public override void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    void MovePlayer(PlayerManager playerManager)
    {
        float xImpulse = 0;
        if (playerManager.inputManager.moveInput.x > 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 0);
            xImpulse = playerManager.playerData.playerSpeed - playerManager.playerRB.velocity.x;
        }
        else if (playerManager.inputManager.moveInput.x < 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 180);
            xImpulse = -playerManager.playerData.playerSpeed - playerManager.playerRB.velocity.x;
        }
        else if (playerManager.inputManager.moveInput.x == 0)
        {
            xImpulse = 0 - playerManager.playerRB.velocity.x;
        }

        playerManager.playerRB.AddForce(new Vector2(xImpulse, 0), ForceMode2D.Impulse);
    }

    void UpImpulse(PlayerManager playerManager)
    {
        float yImpulse;
        yImpulse = playerManager.playerData.jumpForce - playerManager.playerRB.velocity.y;

        playerManager.playerRB.AddForce(new Vector2(0, yImpulse), ForceMode2D.Impulse);
    }

    public override void InvulnerabilityManager(PlayerManager playerManager)
    {
        if (isInvunerable)
        {
            invunerableFlashTimer += Time.deltaTime;
            invunerableTotalTimer += Time.deltaTime;
            if (invunerableFlashTimer >= playerManager.playerData.invunerabilityFlashTime)
            {
                if (spriteVisibility)// Toggle sprite visibility
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = false;
                    spriteVisibility = false;
                }
                else
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                    spriteVisibility = true;
                }
                invunerableFlashTimer = 0;
            }
            if (invunerableTotalTimer >= playerManager.playerData.maxInvunerableTimeValue)
            {
                playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                spriteVisibility = true;
                isInvunerable = false;
            }
        }
    }
}

public class PlayerSpecialAttackState : PlayerBaseState
{
    float timer;
    float specialAttackTotalAnimationTime;
    float specialAttackTimeToShowHitbox;
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.playerAnimator.Play("SpecialAttack");
        timer = 0;
        foreach (var clip in playerManager.playerAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "SpecialAttack")
            {
                specialAttackTotalAnimationTime = clip.length;
            }
        }
        specialAttackTimeToShowHitbox = specialAttackTotalAnimationTime / 2;
    }

    public override void UpdateState(PlayerManager playerManager)
    {
        InvulnerabilityManager(playerManager);
        if(timer >= specialAttackTimeToShowHitbox)
        {
            playerManager.playerSpecialAttackObj.SetActive(true);
        }
        if (timer >= specialAttackTotalAnimationTime)
        {
            playerManager.playerSpecialAttackObj.SetActive(false);
            if (playerManager.inputManager.moveInput.x != 0)
            {
                playerManager.SwitchState(playerManager.playerWalk);
                return;
            }
            else
            {
                playerManager.SwitchState(playerManager.playerIdle);
                return;
            }
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    public override void FixedUpdateState(PlayerManager playerManager)
    {

    }

    public override string CurrentState(PlayerManager playerManager)
    {
        return "SpecialAttack";
    }

    public override void OnCollisionEnter2D(PlayerManager playerManager, Collision2D collider)
    {
        if(collider.gameObject.tag == "DeathBarrier")
        {
            playerManager.SwitchState(playerManager.playerDeath);
        }
    }

    public override void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void InvulnerabilityManager(PlayerManager playerManager)
    {
        if (isInvunerable)
        {
            invunerableFlashTimer += Time.deltaTime;
            invunerableTotalTimer += Time.deltaTime;
            if (invunerableFlashTimer >= playerManager.playerData.invunerabilityFlashTime)
            {
                if (spriteVisibility)// Toggle sprite visibility
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = false;
                    spriteVisibility = false;
                }
                else
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                    spriteVisibility = true;
                }
                invunerableFlashTimer = 0;
            }
            if (invunerableTotalTimer >= playerManager.playerData.maxInvunerableTimeValue)
            {
                playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                spriteVisibility = true;
                isInvunerable = false;
            }
        }
    }
}

public class PlayerTakeDamageState : PlayerBaseState
{
    public Vector2 knockbackDirection;
    float timer;
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.playerAnimator.Play("TakeDamage");
        timer = 0;
        impulseWasGiven = false;
    }

    public override void UpdateState(PlayerManager playerManager)
    {
        InvulnerabilityManager(playerManager);
        if (timer > playerManager.playerAnimator.GetCurrentAnimatorStateInfo(0).length)
        {
            playerManager.SwitchState(playerManager.playerIdle);
        }
        else
        {
            timer += Time.deltaTime;
        }

    }

    public override void FixedUpdateState(PlayerManager playerManager)
    {
        if(!impulseWasGiven)
        {
            knockbackDirection.Normalize();
            knockbackDirection.x = playerManager.playerData.knockbackFromAttacksHorizontal * knockbackDirection.x - playerManager.playerRB.velocity.x;
            knockbackDirection.y = playerManager.playerData.knockbackFromAttacksVertical * knockbackDirection.y - playerManager.playerRB.velocity.y;
            playerManager.playerRB.AddForce(knockbackDirection, ForceMode2D.Impulse);
            impulseWasGiven = true;
        }
    }

    public override string CurrentState(PlayerManager playerManager)
    {
        return "TakeDamage";
    }

    public override void OnCollisionEnter2D(PlayerManager playerManager, Collision2D collider)
    {
        if(collider.gameObject.tag == "DeathBarrier")
        {
            playerManager.SwitchState(playerManager.playerDeath);
        }
    }

    public override void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void InvulnerabilityManager(PlayerManager playerManager)
    {
        if (isInvunerable)
        {
            invunerableFlashTimer += Time.deltaTime;
            invunerableTotalTimer += Time.deltaTime;
            if (invunerableFlashTimer >= playerManager.playerData.invunerabilityFlashTime)
            {
                if (spriteVisibility)// Toggle sprite visibility
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = false;
                    spriteVisibility = false;
                }
                else
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                    spriteVisibility = true;
                }
                invunerableFlashTimer = 0;
            }
            if (invunerableTotalTimer >= playerManager.playerData.maxInvunerableTimeValue)
            {
                playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                spriteVisibility = true;
                isInvunerable = false;
            }
        }
    }
}

public class PlayerDeathState : PlayerBaseState
{
    float deathAnimationTotalTime;
    float timer;
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        playerManager.deathSpriteRenderer.gameObject.SetActive(true);
        deathAnimationTotalTime = playerManager.playerAnimator.GetCurrentAnimatorClipInfo(0).Length;
        timer = 0;
        playerManager.playerRB.bodyType = RigidbodyType2D.Static;
        playerManager.playerLifeManager.UpdateLife(0,playerManager.playerData.maxHitPointsValue);
    }

    public override void UpdateState(PlayerManager playerManager)
    {
        InvulnerabilityManager(playerManager);
        if (timer <= deathAnimationTotalTime)
        {
            for(int i=0; i<playerManager.deathAnimationPoints.childCount; i++)
            {
                if(timer>= i*deathAnimationTotalTime/playerManager.deathAnimationPoints.childCount && timer<= (i+1)*deathAnimationTotalTime/playerManager.deathAnimationPoints.childCount)
                {
                    playerManager.deathSpriteRenderer.transform.position =  playerManager.deathSpriteRenderer.transform.position + (playerManager.deathAnimationPoints.GetChild(i).position - playerManager.deathSpriteRenderer.transform.position)/0.2f * Time.deltaTime;
                }
            }
            timer += Time.deltaTime;
        }
        else
        {
            playerManager.inGameMenu.SetActive(false);
            playerManager.gameOverMenu.SetActive(true);
        }
    }

    public override void FixedUpdateState(PlayerManager playerManager)
    {

    }

    public override string CurrentState(PlayerManager playerManager)
    {
        return "Death";
    }

    public override void OnCollisionEnter2D(PlayerManager playerManager, Collision2D collider)
    {
        if(collider.gameObject.tag == "DeathBarrier")
        {
            playerManager.SwitchState(playerManager.playerDeath);
        }
    }

    public override void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void InvulnerabilityManager(PlayerManager playerManager)
    {
        if (isInvunerable)
        {
            invunerableFlashTimer += Time.deltaTime;
            invunerableTotalTimer += Time.deltaTime;
            if (invunerableFlashTimer >= playerManager.playerData.invunerabilityFlashTime)
            {
                if (spriteVisibility)// Toggle sprite visibility
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = false;
                    spriteVisibility = false;
                }
                else
                {
                    playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                    spriteVisibility = true;
                }
                invunerableFlashTimer = 0;
            }
            if (invunerableTotalTimer >= playerManager.playerData.maxInvunerableTimeValue)
            {
                playerManager.playerRB.GetComponent<SpriteRenderer>().enabled = true;
                spriteVisibility = true;
                isInvunerable = false;
            }
        }
    }
}

public class PlayerEnterDoorState : PlayerBaseState
{
    public override void EnterState(PlayerManager playerManager)
    {
        isInvunerable = true;
    }

    public override void UpdateState(PlayerManager playerManager)
    {

    }

    public override void FixedUpdateState(PlayerManager playerManager)
    {
        var xImpulse = 0 - playerManager.playerRB.velocity.x;

        playerManager.playerRB.AddForce(new Vector2(xImpulse, 0), ForceMode2D.Impulse);
    }

    public override string CurrentState(PlayerManager playerManager)
    {
        return "EnterDoor";
    }

    public override void OnCollisionEnter2D(PlayerManager playerManager, Collision2D collider)
    {
        
    }

    public override void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void InvulnerabilityManager(PlayerManager playerManager)
    {
        
    }
}
