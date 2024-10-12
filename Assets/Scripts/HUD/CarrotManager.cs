using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class CarrotManager : MonoBehaviour
{
    public GameObject carrot1;
    public GameObject carrot2;

    public Sprite carrots0;
    public Sprite carrots1;
    public Sprite carrots2;
    public Sprite carrots3;
    public Sprite carrots4;
    public Sprite carrots5;

    public void UpdateCarrots(int carrotQuantity)
    {
        if(carrotQuantity<0 || carrotQuantity>10)
        {
            return;
        }
        else
        {
            switch(carrotQuantity)
            {
                case 0:
                    carrot1.GetComponent<Image>().sprite = carrots0;
                    carrot2.GetComponent<Image>().sprite = carrots0;
                    break;

                case 1:
                    carrot1.GetComponent<Image>().sprite = carrots1;
                    carrot2.GetComponent<Image>().sprite = carrots0;
                    break;
                
                case 2:
                    carrot1.GetComponent<Image>().sprite = carrots2;
                    carrot2.GetComponent<Image>().sprite = carrots0;
                    break;

                case 3:
                    carrot1.GetComponent<Image>().sprite = carrots3;
                    carrot2.GetComponent<Image>().sprite = carrots0;
                    break;

                case 4:
                    carrot1.GetComponent<Image>().sprite = carrots4;
                    carrot2.GetComponent<Image>().sprite = carrots0;
                    break;

                case 5:
                    carrot1.GetComponent<Image>().sprite = carrots5;
                    carrot2.GetComponent<Image>().sprite = carrots0;
                    break;

                case 6:
                    carrot1.GetComponent<Image>().sprite = carrots5;
                    carrot2.GetComponent<Image>().sprite = carrots1;
                    break;

                case 7:
                    carrot1.GetComponent<Image>().sprite = carrots5;
                    carrot2.GetComponent<Image>().sprite = carrots2;
                    break;

                case 8:
                    carrot1.GetComponent<Image>().sprite = carrots5;
                    carrot2.GetComponent<Image>().sprite = carrots3;
                    break;

                case 9:
                    carrot1.GetComponent<Image>().sprite = carrots5;
                    carrot2.GetComponent<Image>().sprite = carrots4;
                    break;

                case 10:
                    carrot1.GetComponent<Image>().sprite = carrots5;
                    carrot2.GetComponent<Image>().sprite = carrots5;
                    break;
                
            }
        }
    }
}
