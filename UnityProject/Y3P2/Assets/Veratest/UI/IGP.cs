﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IGP : MonoBehaviour
{
    public Image positionImage;
    public Image itemImage;
    public TMP_Text mL;
    public TMP_Text l;
    public bool mine;

    public void Position(int pos)
    {
        if (mine)
        {
            print(pos);
        }
        positionImage.sprite = IGP_Manager.instance.places[pos - 1];
    }

    public void MaxLaps(int lap)
    {
        mL.text = lap.ToString();
    }

    public void Lap(int lap)
    {
        l.text = lap.ToString();
    }

    public void Img(Sprite spr)
    {
        itemImage.sprite = spr;
        itemImage.gameObject.SetActive(true);
        print("tezt");
    }

    public void DisImg()
    {
        itemImage.gameObject.SetActive(false);
    }
}
