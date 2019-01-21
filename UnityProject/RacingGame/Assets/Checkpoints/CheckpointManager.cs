﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;
    public int amoutOfLaps;

    public List<Checkpoint> allCheckpoints = new List<Checkpoint>();
    public List<int> laps = new List<int>();
    public List<Racer> finished = new List<Racer>();
    public List<Racer> racers = new List<Racer>();



    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public int GetNumber(Racer myRacer)
    {
        laps.Add(1);
        racers.Add(myRacer);
        return laps.Count - 1;
    }

    public void ResetLap(Racer r)
    {
        for (int i = 0; i < allCheckpoints.Count; i++)
        {
            allCheckpoints[i].RemovePlayer(r);
        }
        if (laps[r.number] == amoutOfLaps)
        {
            finished.Add(r);
            r.finished = true;
        }
        laps[r.number]++;
    }

    public bool CheckLastCheckpoint(Racer mR, Checkpoint cP)
    {
        int index = allCheckpoints.IndexOf(cP);
        if(index - 1 >= 0)
        {
            index --;
            if (allCheckpoints[index].finish || allCheckpoints[index].BeenHere(mR))
            {
                return true;
            }
        }
        else
        {
            index = allCheckpoints.Count - 1;
            if (allCheckpoints[index].finish || allCheckpoints[index].BeenHere(mR))
            {
                return true;
            }
        }

        return false;
    }

    private void Update()
    {
        for (int i = 0; i < racers.Count; i++)
        {
            if (!racers[i].finished)
            {
                racers[i].SetScore(GetPoints(racers[i]));
            }
        }
        SetOrder(finished);
        for (int i = 0; i < racers.Count; i++)
        {
            racers[i].racePosition = i + 1;
        }
    }

    private void SetOrder(List<Racer> finished)
    {
        List<Racer> left = new List<Racer>(racers);
        List<Racer> newOrder = new List<Racer>(finished);
        while (left.Count != 0)
        {
            Racer highest = left[0];
            for (int i = 0; i < left.Count; i++)
            {
                if (left[i].GetScore() > highest.GetScore())
                {
                    highest = left[i];
                }
            }
            newOrder.Add(highest);
            left.Remove(highest);
        }
        if(newOrder.Count == racers.Count)
        {
            racers = newOrder;
        }
    }

    private float GetPoints(Racer r)
    {
        float points = laps[r.number] * allCheckpoints.Count;
        for (int i = 0; i < allCheckpoints.Count; i++)
        {
            if (allCheckpoints[i].BeenHere(r))
            {
                points++;
            }
        }
        points = points * 10000;
        points += getDistance(r);
        return points;
    }

    private Checkpoint LastCheckpoint(Racer r)
    {
        int index = -1;
        for (int i = 0; i < allCheckpoints.Count; i++)
        {
            if (allCheckpoints[i].BeenHere(r))
            {
                index++;
            }
            else
            {
                break;
            }
        }
        if(index == -1)
        {
            index = allCheckpoints.Count - 1;
        }
        return allCheckpoints[index];
    }

    private float getDistance(Racer r)
    {
        Checkpoint lc = LastCheckpoint(r);
        float d = Vector3.Distance(lc.gameObject.transform.position, r.gameObject.transform.position);
        if(d > 1000)
        {
            return -1;
        }
        return d;
    }
}