using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenesData
{
    public static double[] riddlesCoords = new double[10];
    public static string[] riddlesText = new string[6];
    public static bool nicknameIntroduced = false;
    public static int currentRiddle = 1;
    public static int numberOfRiddles = 1;

    public static void AddNewRiddleCoords(Vector2d latLong)
    {
        riddlesCoords[2 * (currentRiddle - 1)] = latLong.x;
        riddlesCoords[2 * (currentRiddle - 1) + 1] = latLong.y;
        foreach(double riddle in riddlesCoords)
        {
            Debug.Log("Coord: " + riddle);
        }
    }
}
