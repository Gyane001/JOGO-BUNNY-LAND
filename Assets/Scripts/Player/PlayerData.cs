using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjets/PlayerData")]
public class PlayerData : ScriptableObject
{
    #region Initial Values
        [Header("Initial Values")]
        public bool isGroundedInitialValue;
        public bool isInvunerableInitialValue;
        public float invunerableTimeInitialValue;
        public int hitPointsInitialValue;
        [Space(10)]
    #endregion

    #region Variables
        [Header("Variables")]
        public float playerSpeed;
        public float jumpForce;
        public float knockbackFromAttacks;
        public int attackDamage;
        public float attackKnockback;
        [Space(10)]
    #endregion

    #region 
        [Header("Max Values")]
        public float maxInvunerableTimeValue;
        public int maxHitPointsValue;
    #endregion
}
