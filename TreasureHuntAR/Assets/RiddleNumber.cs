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
        string riddleNumber = GameObject.Find("RiddleNumber").GetComponent<Text>().text;
        GameObject.Find("RiddleText").GetComponent<Text>().text = "Riddle " + riddleNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
