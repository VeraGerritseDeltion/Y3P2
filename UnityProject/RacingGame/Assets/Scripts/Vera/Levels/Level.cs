using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    public List<Transform> startLocations = new List<Transform>();

    private void Start()
    {
        LevelManager.instance.currentLevel = this;
    }
}
