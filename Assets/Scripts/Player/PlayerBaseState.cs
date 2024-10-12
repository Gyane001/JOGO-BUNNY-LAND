using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public bool pendingUpImpulse;
    public bool isGrounded;
    public bool isInvunerable;
    public float invunerableTime;
    public float maxInvunerableTime;

    public abstract void EnterState(PlayerManager playerManager);

    public abstract void UpdateState(PlayerManager playerManager);

    public abstract void FixedUpdateState(PlayerManager playerManager);

    public abstract string CurrentState(PlayerManager playerManager);

    public abstract void OnCollisionEnter2D(PlayerManager playerManager, Collision2D collider);

    public abstract void OnCollisionStay2D(PlayerManager playerManager, Collision2D collider) ;

    public abstract void OnCollisionExit2D(PlayerManager playerManager, Collision2D collider);
}
