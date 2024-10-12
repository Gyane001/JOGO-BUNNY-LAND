using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningKnifeManager : MonoBehaviour
{
    public PlayerData playerData;

    void Awake()
    {
        Destroy(this.gameObject, playerData.spinningKnifeDuration);
    }

    public void DestroyEarlier()
    {
        Destroy(this.gameObject);
    }
}
