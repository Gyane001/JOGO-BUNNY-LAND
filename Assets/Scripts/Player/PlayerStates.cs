using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
        if(playerManager.inputManager.jumpInput)
        {
            playerManager.SwitchState(playerManager.playerJump);
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

    }

    public override string CurrentState(PlayerManager playerManager)
    {
        return "Idle";
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
}

public class PlayerWalkState : PlayerBaseState
{
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.playerAnimator.Play("Walk");
    }

    public override void UpdateState(PlayerManager playerManager)
    {
        if(playerManager.inputManager.jumpInput)
        {
            playerManager.SwitchState(playerManager.playerJump);
            return;
        }
        if(playerManager.inputManager.attackInput)
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

    }

    public override void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    void MovePlayer(PlayerManager playerManager)
    {
        if(playerManager.inputManager.moveInput.x > 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 0);
        }
        else if (playerManager.inputManager.moveInput.x < 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 180);
        }

        //var nextPosition = playerManager.playerRB.position + new Vector2(playerManager.playerData.playerSpeed*Time.fixedDeltaTime*playerManager.inputManager.moveInput.x, 0);        
        //playerManager.playerRB.MovePosition(nextPosition);

        //playerManager.playerRB.AddForce(new Vector2(playerManager.inputManager.moveInput.x * playerManager.playerData.playerSpeed,0), ForceMode2D.Force);

        playerManager.playerRB.velocity = new Vector2(playerManager.playerData.playerSpeed * playerManager.inputManager.moveInput.x, playerManager.playerRB.velocity.y);
    }
}

public class PlayerJumpState : PlayerBaseState
{   
    bool impulseWasGiven;
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.playerAnimator.Play("Jump");
        impulseWasGiven = false;
    }

    public override void UpdateState(PlayerManager playerManager)
    {

    }

    public override void FixedUpdateState(PlayerManager playerManager)
    {
        if(!impulseWasGiven)
        {
            playerManager.playerRB.AddForce(new Vector2(0, playerManager.playerData.jumpForce), ForceMode2D.Impulse);
            impulseWasGiven = true;
            isGrounded = false;
        }
        else if(pendingUpImpulse)
        {
            playerManager.playerRB.AddForce(new Vector2(0, 2*playerManager.playerData.jumpForce), ForceMode2D.Impulse);
            pendingUpImpulse = false;
        }
        if(playerManager.inputManager.moveInput.x != 0)
        {
            MovePlayer(playerManager);
        }
    }

    public override string CurrentState(PlayerManager playerManager)
    {
        return "Jump";
    }

    public override void OnCollisionEnter2D(PlayerManager playerManager, Collision2D collider)
    {   
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
        Debug.Log(playerManager.inputManager.moveInput == null);
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
        

        //playerManager.playerData.playerSpeed - playerManager.playerRB.velocity.x

        //var nextPosition = playerManager.playerRB.position + new Vector2(playerManager.playerData.playerSpeed*Time.fixedDeltaTime*playerManager.inputManager.moveInput.x, 0);        
        //playerManager.playerRB.MovePosition(nextPosition);
        //playerManager.playerRB.velocity = new Vector2(playerManager.playerData.playerSpeed * playerManager.inputManager.moveInput.x, playerManager.playerRB.velocity.y);
    }
}

public class PlayerAttackState : PlayerBaseState
{
    float time;
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.playerAnimator.PlayInFixedTime("Attack");
        time = 0;
        playerManager.attack.SetActive(true);
        playerManager.attack.GetComponent<BoxCollider2D>().offset = new Vector2(4.5f, 0.5f);
        playerManager.attack.GetComponent<BoxCollider2D>().size = new Vector2(2f, 3f);
    }

    public override void UpdateState(PlayerManager playerManager)
    {
        if(time >=playerManager.attackAnimationSwitchSpriteTime)
        {
            playerManager.attack.GetComponent<BoxCollider2D>().offset = new Vector2(4.5f, -2.75f);
            playerManager.attack.GetComponent<BoxCollider2D>().size = new Vector2(4f, 1.75f);
        }
        if(time >=playerManager.attackAnimationTotalTime)
        {
            playerManager.attack.SetActive(false);
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
            time += Time.deltaTime;
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

    }

    public override void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    void MovePlayer(PlayerManager playerManager)
    {
        if(playerManager.inputManager.moveInput.x > 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 0);
        }
        else if (playerManager.inputManager.moveInput.x < 0)
        {
            playerManager.playerRB.transform.eulerAngles = new Vector2(0, 180);
        }

        //var nextPosition = playerManager.playerRB.position + new Vector2(playerManager.playerData.playerSpeed*Time.fixedDeltaTime*playerManager.inputManager.moveInput.x, 0);        
        //playerManager.playerRB.MovePosition(nextPosition);

        //playerManager.playerRB.AddForce(new Vector2(playerManager.inputManager.moveInput.x * playerManager.playerData.playerSpeed,0), ForceMode2D.Force);

        playerManager.playerRB.velocity = new Vector2(playerManager.playerData.playerSpeed * playerManager.inputManager.moveInput.x, playerManager.playerRB.velocity.y);
    }
}

public class PlayerSpecialAttackState : PlayerBaseState
{
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.playerAnimator.Play("SpecialAttack");
    }

    public override void UpdateState(PlayerManager playerManager)
    {

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

    }

    public override void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider)
    {

    }
}

public class PlayerTakeDamageState : PlayerBaseState
{
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.playerAnimator.Play("TakeDamage");
    }

    public override void UpdateState(PlayerManager playerManager)
    {

    }

    public override void FixedUpdateState(PlayerManager playerManager)
    {

    }

    public override string CurrentState(PlayerManager playerManager)
    {
        return "TakeDamage";
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
}

public class PlayerDeathState : PlayerBaseState
{
    public override void EnterState(PlayerManager playerManager)
    {
        playerManager.playerAnimator.Play("Death");
    }

    public override void UpdateState(PlayerManager playerManager)
    {

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

    }

    public override void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider)
    {

    }

    public override void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider)
    {

    }
}
