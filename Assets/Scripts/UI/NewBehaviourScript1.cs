﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ssss" + PlayerPrefs.GetString("1") + ":" + PlayerPrefs.GetString("2"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
