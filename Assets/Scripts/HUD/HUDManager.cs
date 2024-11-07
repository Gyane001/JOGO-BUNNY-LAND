using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HUDManager : MonoBehaviour
{
    public void ResetCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void ResetToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
