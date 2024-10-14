using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpinningKnifeSpawner : MonoBehaviour
{
    public GameObject spinningKnifePrefab;
    public PlayerData playerData;
    public void spawnSpinningKnife(Vector3 position, float yRotation)
    {
        var spinningknife = Instantiate(spinningKnifePrefab, position, Quaternion.identity);
        if (yRotation == 0)
        {
            spinningknife.GetComponent<Rigidbody2D>().velocity = new Vector2(playerData.spinningKnifeSpeed, 0);
        }
        else
        {
            spinningknife.GetComponent<Rigidbody2D>().velocity = new Vector2(-playerData.spinningKnifeSpeed, 0);
        }
    }
}
