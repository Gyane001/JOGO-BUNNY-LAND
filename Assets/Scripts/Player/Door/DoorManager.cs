using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorManager : MonoBehaviour
{
    public Animator doorAnimator;
    private bool checkAnimation = false;
    private float doorAnimationTimer = 0;

    void Update()
    {
        if (checkAnimation)
        {
            if(doorAnimationTimer >= doorAnimator.GetCurrentAnimatorStateInfo(0).length + 0.25f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                doorAnimationTimer += Time.deltaTime;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Player")
        {
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            doorAnimator.Play("Transition");
            checkAnimation = true;
        }
    }
}