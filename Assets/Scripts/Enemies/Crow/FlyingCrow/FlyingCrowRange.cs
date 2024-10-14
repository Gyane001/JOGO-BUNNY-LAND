using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCrowRange : MonoBehaviour
{
    public FlyingCrowEnemyManager flyingCrowEnemyManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        flyingCrowEnemyManager.OnRangeEnter2D(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        flyingCrowEnemyManager.OnRangeExit2D(other);
    }
}
