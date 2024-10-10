using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScaler : MonoBehaviour
{
    public List<Transform> menus;
    
    void Awake() 
    {
        UIScaler();
    }

    private void UIScaler()
    {
        float screenWidthRatio = Screen.width/1920f;
        float screenHeightRatio = Screen.height/1080f;
        
        foreach(Transform menu in menus)
        {
            foreach(RectTransform menuObject in menu)
            {
                menuObject.anchoredPosition = new Vector2(screenWidthRatio*menuObject.anchoredPosition.x, screenHeightRatio*menuObject.anchoredPosition.y);
                menuObject.sizeDelta = new Vector2(screenWidthRatio*menuObject.sizeDelta.x, screenHeightRatio*menuObject.sizeDelta.y);
            }
        }
    }
}
