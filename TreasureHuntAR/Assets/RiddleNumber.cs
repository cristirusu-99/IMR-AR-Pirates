using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiddleNumber : MonoBehaviour
{
    public static int riddleNumber;
    void Start()
    {
        try
        {
            string riddleNumber = GameObject.Find("RiddleNumber").GetComponent<Text>().text;
            GameObject.Find("RiddleText").GetComponent<Text>().text = "Riddle " + riddleNumber;
        }
        catch (NullReferenceException e)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
