using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BossBeforeSpawnState : BossBaseState
{
    public override void EnterState(BossManager bossManager)
    {
        bossManager.bossAnimator.Play("BeforeSpawn");
    }

    public override void UpdateState(BossManager bossManager)
    {

    }

    public override void FixedUpdateState(BossManager bossManager)
    {

    }

    public override string CurrentState(BossManager bossManager)
    {
        return "BeforeSpawn";
    }

    public override void OnRangeEnter2D(BossManager bossManager, Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            bossManager.SwitchState(bossManager.bossSpawn);
            return;
        }
    }

    public override void OnRangeExit2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnHitBoxEnter2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnHitBoxStay2D(BossManager bossManager, Collider2D collider)
    {

    }
}

public class BossSpawnState : BossBaseState
{
    float timer;
    float bossSpawnAnimationTotalTime;
    float bossSpawnAnimationSpeed;
    public override void EnterState(BossManager bossManager)
    {
        bossManager.bossAnimator.Play("Spawn");
        
        timer=0;
        AnimatorController controller = bossManager.bossAnimator.runtimeAnimatorController as AnimatorController;
        foreach (var layer in controller.layers)
        {
            foreach (var state in layer.stateMachine.states)
            {
                if(state.state.name == "Spawn")
                {
                    bossSpawnAnimationTotalTime = (state.state.motion as AnimationClip).length;
                    bossSpawnAnimationSpeed = state.state.speed;
                }
            }
        }
    }

    public override void UpdateState(BossManager bossManager)
    {
        timer += Time.deltaTime * bossSpawnAnimationSpeed;
        if(timer>=bossSpawnAnimationTotalTime)
        {
            bossManager.SwitchState(bossManager.bossIdle);
            return;
        }
    }

    public override void FixedUpdateState(BossManager bossManager)
    {

    }

    public override string CurrentState(BossManager bossManager)
    {
        return "Spawn";
    }

    public override void OnRangeEnter2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnRangeExit2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnHitBoxEnter2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnHitBoxStay2D(BossManager bossManager, Collider2D collider)
    {

    }
}

public class BossIdleState : BossBaseState
{
    public override void EnterState(BossManager bossManager)
    {
        bossManager.bossAnimator.Play("Idle");
        bossManager.LifeBarObj.SetActive(true);
    }

    public override void UpdateState(BossManager bossManager)
    {
        bossManager.bossCurrentState.InvulnerabilityManager(bossManager);

        attackGrassTimer += Time.deltaTime;
        attackSporeTimer += Time.deltaTime;
        if(attackGrassTimer >= bossManager.bossData.bossAttackGrassCooldown)
        {
            bossManager.SwitchState(bossManager.bossAttackGrass);
            return;
        }
        if(attackSporeTimer >= bossManager.bossData.bossAttackSporeCooldown)
        {
            bossManager.SwitchState(bossManager.bossAttackSpore);
            return;
        }
    }

    public override void FixedUpdateState(BossManager bossManager)
    {

    }

    public override string CurrentState(BossManager bossManager)
    {
        return "Idle";
    }

    public override void OnRangeEnter2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnRangeExit2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnHitBoxEnter2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnHitBoxStay2D(BossManager bossManager, Collider2D collider)
    {

    }
}

public class BossAttackSporeState : BossBaseState
{
    float timer;
    float attackTimer;
    float bossAttackSporeAnimationTotalTime;
    float bossAttackSporeAnimationSpeed;
    int shotsFired;
    public override void EnterState(BossManager bossManager)
    {
        bossManager.bossAnimator.Play("AttackSpore");

        bossManager.bossCurrentState.attackSporeTimer=0;

        timer = 0;
        attackTimer = 0;
        shotsFired = 0;
        AnimatorController controller = bossManager.bossAnimator.runtimeAnimatorController as AnimatorController;
        foreach (var layer in controller.layers)
        {
            foreach (var state in layer.stateMachine.states)
            {
                if(state.state.name == "AttackSpore")
                {
                    bossAttackSporeAnimationTotalTime = (state.state.motion as AnimationClip).length;
                    bossAttackSporeAnimationSpeed = state.state.speed;
                }
            }
        }
    }

    public override void UpdateState(BossManager bossManager)
    {
        bossManager.bossCurrentState.InvulnerabilityManager(bossManager);

        timer += Time.deltaTime * bossAttackSporeAnimationSpeed;
        if(timer>= bossAttackSporeAnimationTotalTime)
        {
            bossManager.SwitchState(bossManager.bossIdle);
            return;
        }
        if(timer>= bossManager.bossData.bossAttackSporeOffsetPercentage * bossAttackSporeAnimationTotalTime)
        {
            attackTimer += Time.deltaTime * bossAttackSporeAnimationSpeed;
            var fireTiming= (shotsFired * (bossAttackSporeAnimationTotalTime - bossManager.bossData.bossAttackSporeOffsetPercentage * bossAttackSporeAnimationTotalTime)/bossManager.bossData.bossAttackSporeNumberOfShots) + (bossAttackSporeAnimationTotalTime - bossManager.bossData.bossAttackSporeOffsetPercentage * bossAttackSporeAnimationTotalTime)/(2*bossManager.bossData.bossAttackSporeNumberOfShots);
            if(attackTimer >= fireTiming)
            {
                bossManager.SpawnSpores();
                shotsFired +=1;
            }
        }
    }

    public override void FixedUpdateState(BossManager bossManager)
    {

    }

    public override string CurrentState(BossManager bossManager)
    {
        return "AttackSpore";
    }

    public override void OnRangeEnter2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnRangeExit2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnHitBoxEnter2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnHitBoxStay2D(BossManager bossManager, Collider2D collider)
    {

    }
}

public class BossAttackGrassState : BossBaseState
{
    float timer;
    float attackTimer;
    float bossAttackGrassAnimationTotalTime;
    float bossAttackGrassAnimationSpeed;
    bool hasAttacked;
    public override void EnterState(BossManager bossManager)
    {
        bossManager.bossAnimator.Play("AttackGrass");

        bossManager.bossCurrentState.attackGrassTimer=0;

        timer = 0;
        attackTimer = 0;
        hasAttacked = false;
        AnimatorController controller = bossManager.bossAnimator.runtimeAnimatorController as AnimatorController;
        foreach (var layer in controller.layers)
        {
            foreach (var state in layer.stateMachine.states)
            {
                if(state.state.name == "AttackGrass")
                {
                    bossAttackGrassAnimationTotalTime = (state.state.motion as AnimationClip).length;
                    bossAttackGrassAnimationSpeed = state.state.speed;
                }
            }
        }
    }

    public override void UpdateState(BossManager bossManager)
    {
        bossManager.bossCurrentState.InvulnerabilityManager(bossManager);

        timer += Time.deltaTime * bossAttackGrassAnimationSpeed;
        if(timer>= bossAttackGrassAnimationTotalTime)
        {
            bossManager.SwitchState(bossManager.bossIdle);
            return;
        }
        if(timer>= bossManager.bossData.bossAttackGrassOffsetPercentage * bossAttackGrassAnimationTotalTime)
        {
            attackTimer += Time.deltaTime * bossAttackGrassAnimationSpeed;
            var fireTiming= (bossAttackGrassAnimationTotalTime - bossManager.bossData.bossAttackGrassOffsetPercentage * bossAttackGrassAnimationTotalTime)/2f;
            if(attackTimer >= fireTiming && !hasAttacked)
            {
                bossManager.SpawnGrass();
                attackTimer = 0;
                hasAttacked=true;
            }
        }
    }

    public override void FixedUpdateState(BossManager bossManager)
    {

    }

    public override string CurrentState(BossManager bossManager)
    {
        return "AttackGrass";
    }

    public override void OnRangeEnter2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnRangeExit2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnHitBoxEnter2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnHitBoxStay2D(BossManager bossManager, Collider2D collider)
    {

    }
}

public class BossDeathState : BossBaseState
{
    float bossDeathAnimationTotalTime;
    float bossDeathAnimationSpeed;
    float timer;
    public override void EnterState(BossManager bossManager)
    {
        bossManager.bossAnimator.Play("Death");

        bossManager.LifeBarObj.SetActive(false);
        timer=0;
        AnimatorController controller = bossManager.bossAnimator.runtimeAnimatorController as AnimatorController;
        foreach (var layer in controller.layers)
        {
            foreach (var state in layer.stateMachine.states)
            {
                if(state.state.name == "Death")
                {
                    bossDeathAnimationTotalTime = (state.state.motion as AnimationClip).length;
                    bossDeathAnimationSpeed = state.state.speed;
                }
            }
        }
    }

    public override void UpdateState(BossManager bossManager)
    {
        timer += Time.deltaTime * Mathf.Abs(bossDeathAnimationSpeed);
        if(timer>= bossDeathAnimationTotalTime + 0.5)
        {
            bossManager.GoToCredits();
        }
    }

    public override void FixedUpdateState(BossManager bossManager)
    {

    }

    public override string CurrentState(BossManager bossManager)
    {
        return "Death";
    }

    public override void OnRangeEnter2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnRangeExit2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnHitBoxEnter2D(BossManager bossManager, Collider2D collider)
    {

    }

    public override void OnHitBoxStay2D(BossManager bossManager, Collider2D collider)
    {

    }
}