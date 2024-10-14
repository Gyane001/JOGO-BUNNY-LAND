using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeManager : MonoBehaviour
{
    public Scrollbar playerLifeBar;

    public void UpdateLife(int playercurrentHP, int playerMaxHP)
    {
        playerLifeBar.size =  (float)playercurrentHP / playerMaxHP;
    }
}
