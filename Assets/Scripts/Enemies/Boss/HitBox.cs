using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public BossData bossData;
    public PlayerData playerData;
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                other.gameObject.GetComponent<PlayerManager>().TakeDamage(bossData.bossTouchDamage, Vector2.right);
                break;

            case "PlayerAttack":
                transform.parent.GetComponent<BossManager>().TakeDamage(playerData.attackDamage);
                break;

            case "SpinningKnife":
                transform.parent.GetComponent<BossManager>().TakeDamage(playerData.attackDamage);
                other.gameObject.GetComponent<SpinningKnifeManager>().DestroyEarlier();
                break;

            case "PlayerSpecialAttack":
                transform.parent.GetComponent<BossManager>().TakeDamage(playerData.specialAttackDamage);
                break;
        }    
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                other.gameObject.GetComponent<PlayerManager>().TakeDamage(bossData.bossTouchDamage, Vector2.right);
                break; 

            case "PlayerSpecialAttack":
                transform.parent.GetComponent<BossManager>().TakeDamage(playerData.specialAttackDamage);
                break;
        }
    }
}
