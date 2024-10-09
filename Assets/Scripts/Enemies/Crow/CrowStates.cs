using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowIdleState : CrowBaseState
{
    float showTime = float.PositiveInfinity;
    public override void EnterState(CrowEnemyManager crowEnemyManager)
    {
        showTime = 0;
        crowEnemyManager.questionMark.SetActive(true);
        crowEnemyManager.crowAnimator.Play("Idle");
    }

    public override void UpdateState(CrowEnemyManager crowEnemyManager)
    {
        if(showTime<crowEnemyManager.crowData.questionMarkShowTime)
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
        if(collider.gameObject.tag == "Player")
        {
            Debug.Log("Player tocou no corvo");
        }
        if(collider.gameObject.tag == "PlayerAttack")
        {
            Debug.Log("Player atacou no corvo");
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
    }

    public override void UpdateState(CrowEnemyManager crowEnemyManager)
    {
        if(showExclamationTime>crowEnemyManager.crowData.exclamationShowTime)
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
        if(isWalking)
        {
            var direction = new Vector2(player.transform.position.x - crowEnemyManager.crowRB.position.x,0);
            if(direction.magnitude<0.05)
            {
                //crowEnemyManager.crowRB.velocity = new Vector2(0, crowEnemyManager.crowRB.velocity.y);
            }
            else
            {
                if(direction.x>0)
                {
                    crowEnemyManager.crowRB.transform.eulerAngles = new Vector3(0, 180);
                }
                else
                {
                    crowEnemyManager.crowRB.transform.eulerAngles = new Vector3(0, 0);
                }
                //crowEnemyManager.crowRB.velocity = new Vector2(crowEnemyManager.crowData.crowSpeed*direction.normalized.x, crowEnemyManager.crowRB.velocity.y);    
            }
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
            if(collider.transform.position.y > crowEnemyManager.crowHitBoxPosition.position.y)
            {
                collider.transform.GetComponent<PlayerManager>().currentState.pendingUpImpulse = true;
            }
            else
            {
                Debug.Log("Player tocou de lado ou por baixo");
            }
        }
        if(collider.gameObject.tag == "PlayerAttack")
        {
            crowEnemyManager.crowRB.AddForce(new Vector2(collider.transform.parent.GetComponent<PlayerManager>().playerData.attackKnockback,0), ForceMode2D.Impulse);
        }
    }
}
