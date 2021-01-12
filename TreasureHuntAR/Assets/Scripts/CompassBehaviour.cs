using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class CompassBehaviour : MonoBehaviour { 
    public string headingDegrees; 
    public string headingBearing;
    public float degrees;
    private bool startTracking = false; 
    void Start() 
    { 
        Input.compass.enabled = true; 
        if(!(Input.location.status == LocationServiceStatus.Running) || !(Input.location.status == LocationServiceStatus.Initializing))
        {
            Input.location.Start();
        }
        StartCoroutine(InitializeCompass()); 
    } 
    void Update() 
    {
        if (startTracking)
        {
            degrees = Input.compass.trueHeading;
            transform.rotation = Quaternion.Euler(0, Input.compass.trueHeading, 0);
            /*headingDegrees = ((int)degrees).ToString() + "° ";
            headingBearing = DegreesToCardinalDetailed(degrees);
            if (!(GameObject.Find("DebugText1").GetComponent<Text>().text == headingDegrees))
            {
                GameObject.Find("DebugText1").GetComponent<Text>().text = headingDegrees;
            }
            if (!(GameObject.Find("DebugText2").GetComponent<Text>().text == headingBearing))
            {
                GameObject.Find("DebugText2").GetComponent<Text>().text = headingBearing;
            }*/

        } 
    } 
    IEnumerator InitializeCompass() 
    { 
        yield return new WaitForSeconds(1f); 
        startTracking |= Input.compass.enabled; 
    } 
    private static string DegreesToCardinalDetailed(double degrees) 
    { 
        string[] caridnals = { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW", "N" }; 
        return caridnals[(int)Math.Round(((double)degrees * 10 % 3600) / 225)]; 
    } 
}