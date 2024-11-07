using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlyingCrowData", menuName = "ScriptableObjets/FlyingCrowData")]
public class FlyingCrowData : ScriptableObject
{
    #region Initial Values
        [Header("Initial Values")]
        public bool isInvunerableInitialValue;
        public float invunerableTimerInitialValue;
        [Space(10)]
    #endregion

    #region Variables
        [Header("Variables")]
        public float crowSpeed;
        public int crowDamage;

        public float crowMaxInvunerabilityTime;
        public float crowFlashTime;

        public float timeToStartFlying;
        public float questionMarkShowTime;
        public float exclamationShowTime;
        public float knockbackTimeDuration;
        public float knockbackMultiplier;
        public AudioClip crowTakeDamageSound;
        public AudioClip crowDeathSound;
        [Space(10)] 
    #endregion

    #region Max Values
        [Header("Max Values")]
        public int crowMaxHP;
        public float crowMaxInvunerableTime;
    #endregion
}
