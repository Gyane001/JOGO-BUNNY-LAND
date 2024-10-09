using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowHitBox : MonoBehaviour
{
    public CrowEnemyManager crowEnemyManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        crowEnemyManager.OnHitBoxEnter2D(other);
    }
}