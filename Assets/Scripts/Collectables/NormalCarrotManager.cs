using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCarrotManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerManager>().AddCarrots(1);
            this.gameObject.SetActive(false);
        }
    }
}
