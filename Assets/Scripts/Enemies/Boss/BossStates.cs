using System.Collections;
using System.Collections.Generic;
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
        if (collider.gameObject.tag == "Player")
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

        timer = 0;
        AnimationClip[] clips = bossManager.bossAnimator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            if (clip.name == "Spawn")
            {
                bossSpawnAnimationTotalTime = clip.length;
                bossManager.bossAnimator.SetFloat("AnimationSpeedMultiplier", bossManager.bossData.bossSpawnAnimationSpeedMultiplier);
            }
        }
        bossManager.backgroundMusic.Stop();
        bossManager.backgroundMusic.PlayOneShot(bossManager.bossData.bossBackgroundMusic);
        bossManager.soundEffects.PlayOneShot(bossManager.bossData.bossLaugh);
    }

    public override void UpdateState(BossManager bossManager)
    {
        timer += Time.deltaTime * bossManager.bossData.bossSpawnAnimationSpeedMultiplier;
        if (timer >= bossSpawnAnimationTotalTime)
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

        AnimationClip[] clips = bossManager.bossAnimator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            if (clip.name == "Spawn")
            {
                bossManager.bossAnimator.SetFloat("AnimationSpeedMultiplier", bossManager.bossData.bossIdleAnimationSpeedMultiplier);
            }
        }
    }

    public override void UpdateState(BossManager bossManager)
    {
        bossManager.bossCurrentState.InvulnerabilityManager(bossManager);

        attackGrassTimer += Time.deltaTime;
        attackSporeTimer += Time.deltaTime;
        if (attackGrassTimer >= bossManager.bossData.bossAttackGrassCooldown)
        {
            bossManager.SwitchState(bossManager.bossAttackGrass);
            return;
        }
        if (attackSporeTimer >= bossManager.bossData.bossAttackSporeCooldown)
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
    int shotsFired;
    public override void EnterState(BossManager bossManager)
    {
        bossManager.bossAnimator.Play("AttackSpore");

        bossManager.bossCurrentState.attackSporeTimer = 0;

        timer = 0;
        attackTimer = 0;
        shotsFired = 0;
        AnimationClip[] clips = bossManager.bossAnimator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            if (clip.name == "AttackSpore")
            {
                bossAttackSporeAnimationTotalTime = clip.length;
                bossManager.bossAnimator.SetFloat("AnimationSpeedMultiplier", bossManager.bossData.bossAttackSporeAnimationSpeedMultiplier);
            }
        }
    }

    public override void UpdateState(BossManager bossManager)
    {
        bossManager.bossCurrentState.InvulnerabilityManager(bossManager);

        timer += Time.deltaTime * bossManager.bossData.bossAttackSporeAnimationSpeedMultiplier;
        if (timer >= bossAttackSporeAnimationTotalTime)
        {
            bossManager.SwitchState(bossManager.bossIdle);
            return;
        }
        if (timer >= bossManager.bossData.bossAttackSporeOffsetPercentage * bossAttackSporeAnimationTotalTime)
        {
            attackTimer += Time.deltaTime * bossManager.bossData.bossAttackSporeAnimationSpeedMultiplier;
            var fireTiming = (shotsFired * (bossAttackSporeAnimationTotalTime - bossManager.bossData.bossAttackSporeOffsetPercentage * bossAttackSporeAnimationTotalTime) / bossManager.bossData.bossAttackSporeNumberOfShots) + (bossAttackSporeAnimationTotalTime - bossManager.bossData.bossAttackSporeOffsetPercentage * bossAttackSporeAnimationTotalTime) / (2 * bossManager.bossData.bossAttackSporeNumberOfShots);
            if (attackTimer >= fireTiming)
            {
                bossManager.SpawnSpores();
                bossManager.soundEffects.PlayOneShot(bossManager.bossData.bossSporeSpawnSound);
                shotsFired += 1;
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
    bool hasAttacked;
    public override void EnterState(BossManager bossManager)
    {
        bossManager.bossAnimator.Play("AttackGrass");

        bossManager.bossCurrentState.attackGrassTimer = 0;

        timer = 0;
        attackTimer = 0;
        hasAttacked = false;
        AnimationClip[] clips = bossManager.bossAnimator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            if (clip.name == "AttackGrass")
            {
                bossAttackGrassAnimationTotalTime = clip.length;
                bossManager.bossAnimator.SetFloat("AnimationSpeedMultiplier", bossManager.bossData.bossAttackGrassAnimationSpeedMultiplier);
            }
        }
    }

    public override void UpdateState(BossManager bossManager)
    {
        bossManager.bossCurrentState.InvulnerabilityManager(bossManager);

        timer += Time.deltaTime * bossManager.bossData.bossAttackGrassAnimationSpeedMultiplier;
        if (timer >= bossAttackGrassAnimationTotalTime)
        {
            bossManager.SwitchState(bossManager.bossIdle);
            return;
        }
        if (timer >= bossManager.bossData.bossAttackGrassOffsetPercentage * bossAttackGrassAnimationTotalTime)
        {
            attackTimer += Time.deltaTime * bossManager.bossData.bossAttackGrassAnimationSpeedMultiplier;
            var fireTiming = (bossAttackGrassAnimationTotalTime - bossManager.bossData.bossAttackGrassOffsetPercentage * bossAttackGrassAnimationTotalTime) / 2f;
            if (attackTimer >= fireTiming && !hasAttacked)
            {
                bossManager.SpawnGrass();
                bossManager.soundEffects.PlayOneShot(bossManager.bossData.bossVinesSpawnSound);
                attackTimer = 0;
                hasAttacked = true;
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

        bossManager.soundEffects.PlayOneShot(bossManager.bossData.bossDeathSound);

        bossManager.LifeBarObj.SetActive(false);
        timer = 0;
        AnimationClip[] clips = bossManager.bossAnimator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            if (clip.name == "Death")
            {
                bossDeathAnimationTotalTime = clip.length;
                bossManager.bossAnimator.SetFloat("AnimationSpeedMultiplier", bossManager.bossData.bossDeathAnimationSpeedMultiplier);
            }
        }
    }

    public override void UpdateState(BossManager bossManager)
    {
        timer += Time.deltaTime * bossManager.bossData.bossDeathAnimationSpeedMultiplier;
        if (timer >= bossDeathAnimationTotalTime + 3.5)
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