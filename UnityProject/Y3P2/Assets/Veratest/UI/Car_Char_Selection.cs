using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car_Char_Selection : MonoBehaviour
{
    public int controller;
    public int panel;

    private int currentGender;
    private int currentHat;
    private int currentBeard;
    private int currentCar;
    private int currentWheels;

    public List<Image> gender = new List<Image>();
    public List<Image> hat = new List<Image>();
    public List<Image> beard = new List<Image>();
    public List<Image> car = new List<Image>();
    public List<Image> wheel = new List<Image>();

    bool cD;

    public void StartMe()
    {
        Beard();
        Gender();
        Hat();
        Car();
        Wheel();
    }

    private void Update()
    {
        if (controller != 0)
        {

            if (Input.GetAxis("C" + controller + " Vert") < 0 && !cD)
            {
                switch (Car_Char_Sel_Manager.instance.current[panel])
                {
                    case Car_Char_Sel_Manager.CustomazationIndex.gender:
                        currentGender++;
                        Gender();
                        break;
                    case Car_Char_Sel_Manager.CustomazationIndex.beard:
                        currentBeard++;
                        Beard();
                        break;
                    case Car_Char_Sel_Manager.CustomazationIndex.hat:
                        currentHat++;
                        Hat();
                        break;
                    case Car_Char_Sel_Manager.CustomazationIndex.car:
                        currentCar++;
                        Car();
                        break;
                    case Car_Char_Sel_Manager.CustomazationIndex.wheels:
                        currentWheels++;
                        Wheel();
                        break;
                }

                cD = true;
                StartCoroutine(CD());
            }
            if (Input.GetAxis("C" + controller + " Vert") > 0 && !cD)
            {
                switch (Car_Char_Sel_Manager.instance.current[panel])
                {
                    case Car_Char_Sel_Manager.CustomazationIndex.gender:
                        currentGender--;
                        Gender();
                        break;
                    case Car_Char_Sel_Manager.CustomazationIndex.beard:
                        currentBeard--;
                        Beard();
                        break;
                    case Car_Char_Sel_Manager.CustomazationIndex.hat:
                        currentHat--;
                        Hat();
                        break;
                    case Car_Char_Sel_Manager.CustomazationIndex.car:
                        currentCar--;
                        Car();
                        break;
                    case Car_Char_Sel_Manager.CustomazationIndex.wheels:
                        currentWheels--;
                        Wheel();
                        break;
                }
                cD = true;
                StartCoroutine(CD());
            }

            if(Input.GetButtonDown("C" + controller + " A"))
            {
                Car_Char_Sel_Manager.instance.NextCatergory(panel);
            }
        }
        
    }

    private void Hat()
    {
        if (currentHat < 0)
        {
            currentHat = Car_Char_Sel_Manager.instance.hatSpr.Count - 1;
        }
        if (currentHat == Car_Char_Sel_Manager.instance.hatSpr.Count)
        {
            currentHat = 0;
        }
        int next = currentHat - 1;
        int last = currentHat + 1;
        if (next < 0)
        {
            next = Car_Char_Sel_Manager.instance.hatSpr.Count - 1;
        }
        if (last == Car_Char_Sel_Manager.instance.hatSpr.Count)
        {
            last = 0;
        }

        hat[0].sprite = Car_Char_Sel_Manager.instance.hatSpr[next];
        hat[1].sprite = Car_Char_Sel_Manager.instance.hatSpr[currentHat];
        hat[2].sprite = Car_Char_Sel_Manager.instance.hatSpr[last];
    }

    private void Beard()
    {
        if (currentBeard < 0)
        {
            currentBeard = Car_Char_Sel_Manager.instance.beardSpr.Count - 1;
        }
        if (currentBeard == Car_Char_Sel_Manager.instance.beardSpr.Count)
        {
            currentBeard = 0;
        }
        int next = currentBeard - 1;
        int last = currentBeard + 1;
        if (next < 0)
        {
            next = Car_Char_Sel_Manager.instance.beardSpr.Count - 1;
        }
        if (last == Car_Char_Sel_Manager.instance.beardSpr.Count)
        {
            last = 0;
        }

        beard[0].sprite = Car_Char_Sel_Manager.instance.beardSpr[next];
        beard[1].sprite = Car_Char_Sel_Manager.instance.beardSpr[currentBeard];
        beard[2].sprite = Car_Char_Sel_Manager.instance.beardSpr[last];
    }

    private void Car()
    {
        if (currentCar < 0)
        {
            currentCar = Car_Char_Sel_Manager.instance.carSpr.Count - 1;
        }
        if (currentCar == Car_Char_Sel_Manager.instance.carSpr.Count)
        {
            currentCar = 0;
        }
        int next = currentCar - 1;
        int last = currentCar + 1;
        if (next < 0)
        {
            next = Car_Char_Sel_Manager.instance.carSpr.Count - 1;
        }
        if (last == Car_Char_Sel_Manager.instance.carSpr.Count)
        {
            last = 0;
        }

        car[0].sprite = Car_Char_Sel_Manager.instance.carSpr[next];
        car[1].sprite = Car_Char_Sel_Manager.instance.carSpr[currentCar];
        car[2].sprite = Car_Char_Sel_Manager.instance.carSpr[last];
    }

    private void Wheel()
    {
        if (currentWheels < 0)
        {
            currentWheels = Car_Char_Sel_Manager.instance.wheelSpr.Count - 1;
        }
        if (currentWheels == Car_Char_Sel_Manager.instance.wheelSpr.Count)
        {
            currentWheels = 0;
        }
        int next = currentWheels - 1;
        int last = currentWheels + 1;
        if (next < 0)
        {
            next = Car_Char_Sel_Manager.instance.wheelSpr.Count - 1;
        }
        if (last == Car_Char_Sel_Manager.instance.wheelSpr.Count)
        {
            last = 0;
        }

        wheel[0].sprite = Car_Char_Sel_Manager.instance.wheelSpr[next];
        wheel[1].sprite = Car_Char_Sel_Manager.instance.wheelSpr[currentWheels];
        wheel[2].sprite = Car_Char_Sel_Manager.instance.wheelSpr[last];
    }

    private void Gender()
    {
        if (currentGender < 0)
        {
            currentGender = Car_Char_Sel_Manager.instance.genderSpr.Count -1;
        }
        if (currentGender == Car_Char_Sel_Manager.instance.genderSpr.Count)
        {
            currentGender = 0;
        }
        int next = currentGender - 1;
        int last = currentGender + 1;
        if (next < 0)
        {
            next = Car_Char_Sel_Manager.instance.genderSpr.Count - 1;
        }
        if (last >= Car_Char_Sel_Manager.instance.genderSpr.Count)
        {
            last = 0;
        }
        gender[0].sprite = Car_Char_Sel_Manager.instance.genderSpr[next];
        gender[1].sprite = Car_Char_Sel_Manager.instance.genderSpr[currentGender];
        gender[2].sprite = Car_Char_Sel_Manager.instance.genderSpr[last];
    }

    IEnumerator CD()
    {
        yield return new WaitForSeconds(0.2f);
        cD = false;
    }

}
