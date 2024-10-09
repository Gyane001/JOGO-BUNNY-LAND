using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrowData", menuName = "ScriptableObjets/CrowData")]
public class CrowData : ScriptableObject
{
    #region Initial Values
        [Header("Initial Values")]
        public bool isInvunerableInitialValue;
        public float invunerableTimeInitialValue;
        [Space(10)]
    #endregion

    #region Variables
        [Header("Variables")]
        public float crowSpeed;
        public float timeToStartWalk;
        public float questionMarkShowTime;
        public float exclamationShowTime;
        public bool isImmuneToKnockBack;
        public float resistanceToKnockBack;
        [Space(10)]
    #endregion

    #region 
        [Header("Max Values")]
        public int crowMaxHP;
        public float crowMaxInvunerableTime;
    #endregion
}
