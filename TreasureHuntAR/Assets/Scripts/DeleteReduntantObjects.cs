﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteReduntantObjects : MonoBehaviour
{
    
    void Start()
    {
        GameObject[] riddlesRiddles = GameObject.FindGameObjectsWithTag("Gege");
        GameObject[] riddlesRiddlesNumber = GameObject.FindGameObjectsWithTag("Gugu");
        /*GameObject[] photon = GameObject.FindGameObjectsWithTag("Photon");
        if(photon.Length > 1)
        {
            //Destroy(riddlesRiddles[1]);
            Destroy(photon[1]);

        }*/
        if(riddlesRiddlesNumber.Length > 1)
        {
            Destroy(riddlesRiddlesNumber[1]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
