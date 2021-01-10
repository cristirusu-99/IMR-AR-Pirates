﻿using Mapbox.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenesData
{
    public static double[] riddlesCoords = new double[10];   // first value is first riddle x coordinate and second value y coordinate and so on
    public static string[] riddlesText = new string[6];      // first riddle starts at position 1 for conventional purposes
    public static bool nicknameIntroduced = false;
    public static int currentRiddle = 1;
    public static int numberOfRiddles = 1;
    public static double[] treasureCoords = new double[2];
    public static bool treasForT = false;
    
    public static void AddNewRiddleCoords(Vector2d latLong)
    {
        riddlesCoords[2 * (currentRiddle - 1)] = latLong.x;
        riddlesCoords[2 * (currentRiddle - 1) + 1] = latLong.y;
    }

    public static void AddNewTreasureCord(Vector2d latLong)
    {
        treasureCoords[0] = latLong.x;
        treasureCoords[1] = latLong.y;
    }

    public static double[] GetValidRiddlesCoords()
    {
        List<double> validCoords = new List<double>();
        for(int i = 0; i < 10; i += 2)
        {
            if(riddlesCoords[i] != 0)
            {
                validCoords.Add(riddlesCoords[i]);
                validCoords.Add(riddlesCoords[i + 1]);
            }
        }
        /*for(int i = 0, j = 0; i < validCoords.Count; j++, i += 2)
        {
            Debug.Log("Riddle " + j + " x coord: " + validCoords[i] + " y coord: " + validCoords[i + 1]);
        }*/
        return validCoords.ToArray();
    }

    public static string[] GetValidRiddlesText()
    {
        List<string> validTexts = new List<string>();
        for(int i = 1; i < 6; i++)
        {
            if(riddlesText[i] != null && riddlesText[i] != "")
            {
                validTexts.Add(riddlesText[i]);
            }
        }
        /*for(int i = 0; i < validTexts.Count; i++)
        {
            Debug.Log("Riddle " + i + " text: " + validTexts[i]);
        }*/
        return validTexts.ToArray();
    }
}