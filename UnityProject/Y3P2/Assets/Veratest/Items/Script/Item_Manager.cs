﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Manager : MonoBehaviour
{
    public static Item_Manager instance;
    [SerializeField] private List<GameObject> allItems = new List<GameObject>();

    public GameObject GetItem()
    {
        int i = Random.Range(0, allItems.Count);
        return allItems[i];
    }

}