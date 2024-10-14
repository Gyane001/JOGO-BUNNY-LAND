using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingCrowBaseState
{
    public bool pendingKnockback;
    public float knockbackValue;
    public Vector2 knockbackDirection;
    public float knockbackTimer;
    public bool isInvunerable;
    public float invunerableTotalTimer;
    public bool spriteVisibility;
    public float invunerableFlashTimer;
    public abstract void EnterState(FlyingCrowEnemyManager flyingCrowEnemyManager);

    public abstract void UpdateState(FlyingCrowEnemyManager flyingCrowEnemyManager);

    public abstract void FixedUpdateState(FlyingCrowEnemyManager flyingCrowEnemyManager);

    public abstract string CurrentState(FlyingCrowEnemyManager flyingCrowEnemyManager);

    public abstract void OnRangeEnter2D(FlyingCrowEnemyManager flyingCrowEnemyManager, Collider2D collider);

    public abstract void OnRangeExit2D(FlyingCrowEnemyManager flyingCrowEnemyManager, Collider2D collider);

    public abstract void OnHitBoxEnter2D(FlyingCrowEnemyManager flyingCrowEnemyManager, Collider2D collider);

    public abstract void OnHitBoxStay2D(FlyingCrowEnemyManager flyingCrowEnemyManager, Collider2D collider);

    public abstract void InvulnerabilityManager(FlyingCrowEnemyManager flyingCrowEnemyManager);
}
