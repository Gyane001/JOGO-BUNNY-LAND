using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBaseState
{
    public bool isInvunerable;
    public float invunerableTotalTimer;
    public bool spriteVisibility;
    public float invunerableFlashTimer;
    public float attackGrassTimer = 0;
    public float attackSporeTimer = 0;
    public abstract void EnterState(BossManager bossManager);

    public abstract void UpdateState(BossManager bossManager);

    public abstract void FixedUpdateState(BossManager bossManager);

    public abstract string CurrentState(BossManager bossManager);

    public abstract void OnRangeEnter2D(BossManager bossManager, Collider2D collider);

    public abstract void OnRangeExit2D(BossManager bossManager, Collider2D collider);

    public abstract void OnHitBoxEnter2D(BossManager bossManager, Collider2D collider);

    public abstract void OnHitBoxStay2D(BossManager bossManager, Collider2D collider);

    public void InvulnerabilityManager(BossManager bossManager)
    {
        if (isInvunerable)
        {
            invunerableFlashTimer += Time.deltaTime;
            invunerableTotalTimer += Time.deltaTime;
            if (invunerableFlashTimer >= bossManager.bossData.bossFlashTime)
            {
                if (spriteVisibility)// Toggle sprite visibility
                {
                    bossManager.bossAnimator.GetComponent<SpriteRenderer>().enabled = false;
                    spriteVisibility = false;
                }
                else
                {
                    bossManager.bossAnimator.GetComponent<SpriteRenderer>().enabled = true;
                    spriteVisibility = true;
                }
                invunerableFlashTimer = 0;
            }
            if (invunerableTotalTimer >= bossManager.bossData.bossMaxInvunerabilityTime)
            {
                bossManager.bossAnimator.GetComponent<SpriteRenderer>().enabled = true;
                spriteVisibility = true;
                isInvunerable = false;
            }
        }
    }
}
