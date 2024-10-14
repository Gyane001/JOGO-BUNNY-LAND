using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBaseState
{
    public bool isInvunerable;
    public float invunerableTotalTimer;
    public bool spriteVisibility;
    public float invunerableFlashTimer;
    public abstract void EnterState(BossManager bossManager);

    public abstract void UpdateState(BossManager bossManager);

    public abstract void FixedUpdateState(BossManager bossManager);

    public abstract string CurrentState(BossManager bossManager);

    public abstract void OnRangeEnter2D(BossManager bossManager, Collider2D collider);

    public abstract void OnRangeExit2D(BossManager bossManager, Collider2D collider);

    public abstract void OnHitBoxEnter2D(BossManager bossManager, Collider2D collider);

    public abstract void OnHitBoxStay2D(BossManager bossManager, Collider2D collider);

    public abstract void InvulnerabilityManager(BossManager bossManager);
}
