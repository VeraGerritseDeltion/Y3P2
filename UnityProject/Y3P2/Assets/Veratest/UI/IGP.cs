using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IGP : MonoBehaviour
{
    public Image positionImage;
    public Text mL;
    public Text l;

    public void Position(int pos)
    {
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

}
