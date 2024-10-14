using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCrowHitBox : MonoBehaviour
{
    public FlyingCrowEnemyManager flyingCrowEnemyManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        flyingCrowEnemyManager.OnHitBoxEnter2D(other);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        flyingCrowEnemyManager.OnHitBoxStay2D(collision);
    }
}
