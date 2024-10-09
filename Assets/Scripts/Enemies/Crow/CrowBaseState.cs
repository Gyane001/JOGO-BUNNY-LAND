using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CrowBaseState
{
    public bool pendingKnockback;
    public float knockbackValue;
    public Vector2 knockbackDirection;
    public float knockbackTimer;
    public abstract void EnterState(CrowEnemyManager crowEnemyManager);

    public abstract void UpdateState(CrowEnemyManager crowEnemyManager);

    public abstract void FixedUpdateState(CrowEnemyManager crowEnemyManager);

    public abstract string CurrentState(CrowEnemyManager crowEnemyManager);

    public abstract void OnRangeEnter2D(CrowEnemyManager crowEnemyManager, Collider2D collider);

    public abstract void OnRangeExit2D(CrowEnemyManager crowEnemyManager, Collider2D collider);

    public abstract void OnHitBoxEnter2D(CrowEnemyManager crowEnemyManager, Collider2D collider);
}
