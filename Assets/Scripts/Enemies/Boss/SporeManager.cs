using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SporeManager : MonoBehaviour
{
    public float yInitialPosition;
    public float yFinalPosition;
    public float xInitialPosition;
    public float xFinalPosition;
    public BossData bossData;
    float timeToFall;

    void Start()
    {
        float randomVerticalSpeed = UnityEngine.Random.Range(bossData.bossAttackSporeVerticalSpeed - 0.15f*bossData.bossAttackSporeVerticalSpeed, bossData.bossAttackSporeVerticalSpeed + 0.15f*bossData.bossAttackSporeVerticalSpeed);
        timeToFall = SolveQuadratic(Physics.gravity.y/2, randomVerticalSpeed, yInitialPosition-yFinalPosition);

        float randomHorizontalSpeed = xFinalPosition - xInitialPosition;
        randomHorizontalSpeed = randomHorizontalSpeed/timeToFall;
        randomHorizontalSpeed = UnityEngine.Random.Range(randomHorizontalSpeed - 0.15f*randomHorizontalSpeed, randomHorizontalSpeed + 0.15f*randomHorizontalSpeed);
        
        GetComponent<Rigidbody2D>().AddForce(new Vector2(randomHorizontalSpeed, randomVerticalSpeed),ForceMode2D.Impulse);
        Destroy(this.gameObject, timeToFall);
    }

    float SolveQuadratic(float a, float b, float c)
    {
        float discriminant = b * b - 4 * a * c;

        if (discriminant > 0)
        {
            float root1 = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
            float root2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);

            if(root1 > 0)
            {
                return root1;
            }
            if(root2 > 0)
            {
                return root2;
            }
            return -1;
        }
        else if (discriminant == 0)
        {
            float root = -b / (2 * a);
            return root;
        }
        else
        {
            return -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                other.gameObject.GetComponent<PlayerManager>().TakeDamage(bossData.bossAttackSporeDamage, Vector2.right);
                break;
        } 
    }
}
