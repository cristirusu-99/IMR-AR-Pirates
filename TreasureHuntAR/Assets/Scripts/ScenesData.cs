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
    public static double[] treasureCoords = new double[2];
    public static bool treasForT = false;
    public static void AddNewRiddleCoords(Vector2d latLong)
    {
        riddlesCoords[2 * (currentRiddle - 1)] = latLong.x;
        riddlesCoords[2 * (currentRiddle - 1) + 1] = latLong.y;
        foreach(double riddle in riddlesCoords)
            Debug.Log("Coord: " + riddle);
    }
    public static void AddNewTreasureCord(Vector2d latLong)
    {
        treasureCoords[0] = latLong.x;
        treasureCoords[1] = latLong.y;
         Debug.Log("Coord: " + treasureCoords);
       
    }
}
