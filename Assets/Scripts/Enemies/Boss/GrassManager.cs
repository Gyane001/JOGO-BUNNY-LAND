using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GrassManager : MonoBehaviour
{
    public BossData bossData;
    public GameObject exclamation;
    float timerTotal = 0;
    float timerGrassMove = 0;
    void Start()
    {
        Destroy(this.gameObject, bossData.bossAttackGrassWarningTime + bossData.bossAttackGrassTimeToDestroy);
    }

    void Update()
    {
        timerTotal += Time.deltaTime;
        if(timerTotal>= bossData.bossAttackGrassWarningTime)
        {
            exclamation.SetActive(false);
            timerGrassMove += Time.deltaTime;
            if(timerGrassMove<bossData.bossAttackGrassRiseTime)
            {
                this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y + bossData.bossAttackGrassSize*Time.deltaTime/bossData.bossAttackGrassRiseTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                other.gameObject.GetComponent<PlayerManager>().TakeDamage(bossData.bossAttackGrassDamage, Vector2.up);
                break;
        } 
    }
}
