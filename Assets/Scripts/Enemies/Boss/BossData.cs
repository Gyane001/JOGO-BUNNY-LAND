using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossData", menuName = "ScriptableObjets/BossData", order = 0)]
public class BossData : ScriptableObject 
{
    #region Initial Values
        [Header("Initial Values")]
        public bool isInvunerableInitialValue;
        public float invunerableTimerInitialValue;
        [Space(10)]
    #endregion

    #region Variables
        [Header("Variables")]
        public int bossTouchDamage;
        public int bossAttackGrassDamage;
        public int bossAttackSporeDamage;
        public float bossAttackGrassCooldown;
        public float bossAttackSporeCooldown;
        public float bossAttackSporeOffsetPercentage;
        public int bossAttackSporeNumberOfProjectilesPerShoot;
        public int bossAttackSporeNumberOfShots;
        public float bossAttackSporeVerticalSpeed;
        public float bossAttackGrassOffsetPercentage;
        public float bossAttackGrassWarningTime;
        public float bossAttackGrassSize;
        public float bossAttackGrassRiseTime;
        public int bossAttackGrassNumberOfGrass;
        public float bossAttackGrassTimeToDestroy;
        public float bossMaxInvunerabilityTime;
        public float bossFlashTime;    
        [Space(10)]
    #endregion

    #region Max Values
        [Header("Max Values")]
        public int bossMaxHP;
    #endregion    
}