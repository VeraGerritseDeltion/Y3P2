using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGP_Manager : MonoBehaviour
{
    public static IGP_Manager instance;
    public List<Sprite> places = new List<Sprite>();
    public List<IGP> allIGP = new List<IGP>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void UpdatePos(int player, int pos)
    {
        if (pos != 0)
        {
            allIGP[player - 1].Position(pos);
        }

    }

    public void MaxC(int max)
    {
        for (int i = 0; i < allIGP.Count; i++)
        {
            allIGP[i].MaxLaps(max);
        }
    }

    public void CurC(int player, int lap)
    {
        allIGP[player - 1].Lap(lap);
        print("test");
    }

    public void ItemImage(int player,Sprite image)
    {
        allIGP[player - 1].Img(image);
    }

    public void DisImg(int player)
    {
        allIGP[player - 1].DisImg();
    }
}
