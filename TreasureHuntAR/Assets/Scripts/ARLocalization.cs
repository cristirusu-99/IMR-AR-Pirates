﻿using System;
using System.Collections;
using UnityEngine;

public class ARLocalization : MonoBehaviour
{
    public double[] riddlesCoords;
    public string[] riddlesTexts;
    public double[] treasureCoords;
    public int[] foundRiddles;
    public string currentRiddleText;


    IEnumerator Start()
    {
        /* Input.location.Start();

         int maxWait = 20;
         while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
         {
             yield return new WaitForSeconds(1);
             maxWait--;
         }
         if (maxWait < 1)
         {
             print("Timed out");
             yield break;
         }*/
        return null;
    }

    void Update()
    {
        if(riddlesCoords != null)
        {
            //CalculateDistanceBetweenUserAndRiddles();
        }
        
    }

    public void ReceivedGameCoordsAndTexts(double[] receivedRiddlesCoords, double[] receivedTreasureCoords, string[] receivedRiddlesTexts) 
    {
        riddlesCoords = new double[receivedRiddlesCoords.Length];
        riddlesTexts = new string[receivedRiddlesTexts.Length];
        treasureCoords = new double[receivedTreasureCoords.Length];
        foundRiddles = new int[receivedRiddlesTexts.Length];
        currentRiddleText = "";
        receivedRiddlesCoords.CopyTo(riddlesCoords, 0);
        receivedTreasureCoords.CopyTo(treasureCoords, 0);
        receivedRiddlesTexts.CopyTo(riddlesTexts, 0);
        for (int i = 0, j = 0; i < riddlesTexts.Length; i++, j += 2)
        {
            Debug.Log("Riddle " + (i + 1) + " text : " + riddlesTexts[i]);
            Debug.Log("Riddle " + (i + 1) + " x coord: " + riddlesCoords[j] + " y coord: " + riddlesCoords[j + 1]);
        }
        Debug.Log("Treasure x coord: " + treasureCoords[0] + " y coord: " + treasureCoords[1]);
    }

    public void CalculateDistanceBetweenUserAndRiddles()
    {
        double[] userLocation = GetCurrentLocation();
        bool nearRiddle = false;
        for (int i = 0, j = 0; i < riddlesCoords.Length; i += 2, j++)
        {
            double distanceInMetres = DistanceInMetres(userLocation[0], userLocation[1], riddlesCoords[i], riddlesCoords[i + 1]);
            if (distanceInMetres < 20)
            {
                if(foundRiddles[j] == 1)
                {
                    continue;
                } 
                else
                {
                    foundRiddles[j] = 1;
                    currentRiddleText = riddlesTexts[j];
                    nearRiddle = true;
                    break;
                }
                
            }

        }
        if(nearRiddle == false)
        {
            currentRiddleText = "";
        }
    }

    public double DistanceInMetres(double lat1, double lon1, double lat2, double lon2)
    {
        double R = 6371e3; // metres
        double φ1 = lat1 * Math.PI / 180; // φ, λ in radians
        double φ2 = lat2 * Math.PI / 180;
        double Δφ = (lat2 - lat1) * Math.PI / 180;
        double Δλ = (lon2 - lon1) * Math.PI / 180;

        double a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                  Math.Cos(φ1) * Math.Cos(φ2) *
                  Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double d = R * c; // in metres
        return d;
    }

    double[] GetCurrentLocation()
    {

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            double[] userLocation = new double[]{ 0, 0 };
            PhotonSendEvent.SendCurrentUserLocation(userLocation);
            return userLocation;
        }
        else
        {
            double[] userLocation = new double[] { Input.location.lastData.latitude, Input.location.lastData.longitude };
            PhotonSendEvent.SendCurrentUserLocation(userLocation);
            return userLocation;
        }
    }
}