using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool finish;

    public List<Racer> myRacers = new List<Racer>();

    private void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        Racer newInCp = other.gameObject.GetComponent<Racer>();

        if(newInCp != null)
        {
            if (finish && CheckLastCheckpoint(newInCp))
            {
                CheckpointManager.instance.ResetLap(newInCp);
            }
            if (!myRacers.Contains(newInCp) && CheckLastCheckpoint(newInCp))
            {
                myRacers.Add(newInCp);
            }
        }
    }

    bool CheckLastCheckpoint(Racer nR)
    {
        return CheckpointManager.instance.CheckLastCheckpoint(nR, this);
    }

    public bool BeenHere(Racer nR)
    {
        if (myRacers.Contains(nR))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemovePlayer(Racer r)
    {
        if (myRacers.Contains(r))
        {
            myRacers.Remove(r);
        }
    }
}
