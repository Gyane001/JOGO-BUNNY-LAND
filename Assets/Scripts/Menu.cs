using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource soundEffects;
    public AudioClip accept;
    public AudioClip quit;
    public void LoadLevel()
    {
        soundEffects.PlayOneShot(accept);
        Invoke("DelayedLevelLoad", 0.1f);
    }

    public void LoadCredits()
    {
        soundEffects.PlayOneShot(accept);
        Invoke("DelayedCreditsLoad", 0.1f);
    }

    public void LoadMenu()
    {
        soundEffects.PlayOneShot(quit);
        Invoke("DelayedMenuLoad", 0.1f);
    }
    public void Quit()
    {
        soundEffects.PlayOneShot(quit);
        Invoke("DelayedQuitGame", 0.1f);
    }

    //  How to play
    public void HowToPlay()
    {
        soundEffects.PlayOneShot(quit);
        Invoke("DelayedHowToPlayLoad", 0.1f);
    }


    private void DelayedLevelLoad()
    {
        SceneManager.LoadScene("Fase 1"); // Load scene after delay
    }

    private void DelayedCreditsLoad()
    {
        SceneManager.LoadScene("Creditos"); // Load scene after delay
    }

    private void DelayedMenuLoad()
    {
        SceneManager.LoadScene("Menu");
    }

    private void DelayedQuitGame()
    {
        Application.Quit();
    }

    private void DelayedHowToPlayLoad()
    {
        SceneManager.LoadScene("ComoJogar");
    }
}

