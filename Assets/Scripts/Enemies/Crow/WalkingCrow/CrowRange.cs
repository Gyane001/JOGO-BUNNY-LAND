using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowRange : MonoBehaviour
{
    public CrowEnemyManager crowEnemyManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        crowEnemyManager.OnRangeEnter2D(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        crowEnemyManager.OnRangeExit2D(other);
    }
}
