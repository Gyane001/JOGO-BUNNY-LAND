using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCarrotManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerManager>().AddCarrots(5);
            this.gameObject.SetActive(false);
        }
    }
}
