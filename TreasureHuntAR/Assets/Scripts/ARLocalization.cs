using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ARLocalization : MonoBehaviour
{
    public double[] riddlesCoords;
    public string[] riddlesTexts;
    public double[] treasureCoords;
    public int[] foundRiddles;
    public string currentRiddleText;
    public bool treasureFound = false;
    public int currentRiddleNumber = -1;

    IEnumerator Start()
    {
#if UNITY_EDITOR
        yield return null;

#elif UNITY_ANDROID
        Input.location.Start();

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
        }
#elif UNITY_IOS
        Input.location.Start();

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
        }
#endif
        ScenesData.nicknameIntroduced = false;
    }



    void Update()
    {
        if(riddlesCoords != null)
        {
#if UNITY_EDITOR
            
#elif UNITY_ANDROID
            CalculateDistanceBetweenUserAndRiddles();
#elif UNITY_IOS
            CalculateDistanceBetweenUserAndRiddles();
#endif

        }

    }

    public void ReceivedGameCoordsAndTexts(double[] receivedRiddlesCoords, double[] receivedTreasureCoords, string[] receivedRiddlesTexts, byte playerNumber) 
    {
        riddlesCoords = new double[receivedRiddlesCoords.Length];
        riddlesTexts = new string[receivedRiddlesTexts.Length];
        treasureCoords = new double[receivedTreasureCoords.Length];
        if(ScenesData.riddlesReceived == 0)
        {
            foundRiddles = new int[receivedRiddlesTexts.Length];
            foundRiddles[0] = 1;
            ScenesData.riddlesReceived = 1;
            currentRiddleText = "";
        }
        ScenesData.playerNumber = playerNumber;
        receivedRiddlesCoords.CopyTo(riddlesCoords, 0);
        receivedTreasureCoords.CopyTo(treasureCoords, 0);
        receivedRiddlesTexts.CopyTo(riddlesTexts, 0);
        /*for (int i = 0, j = 0; i < riddlesTexts.Length; i++, j += 2)
        {
            Debug.Log("Riddle " + (i + 1) + " text : " + riddlesTexts[i]);
            Debug.Log("Riddle " + (i + 1) + " x coord: " + riddlesCoords[j] + " y coord: " + riddlesCoords[j + 1]);
        }
        Debug.Log("Treasure x coord: " + treasureCoords[0] + " y coord: " + treasureCoords[1]);*/
    }

    public void CalculateDistanceBetweenUserAndRiddles()
    {
        double[] userLocation = GetCurrentLocation();
        bool nearRiddle = false;
        for (int i = 2, j = 1; i < riddlesCoords.Length; i += 2, j++)
        {
            double distanceInMetres = DistanceInMetres(userLocation[0], userLocation[1], riddlesCoords[i], riddlesCoords[i + 1]);
            if (distanceInMetres < 20)
            {
                //foundRiddles[j] = 1;
                currentRiddleText = riddlesTexts[j];
                currentRiddleNumber = j;
                nearRiddle = true;
            }

        }
        if (nearRiddle == false)
        {
            currentRiddleText = "";
        }
        double distanceInMetresTreasure = DistanceInMetres(userLocation[0], userLocation[1], treasureCoords[0], treasureCoords[1]);
        if(distanceInMetresTreasure < 20)
        {
            treasureFound = true;
            for(int i = 0; i < foundRiddles.Length; i++)
            {
               if(foundRiddles[i] != foundRiddles[0])
                {
               
                    treasureFound = false;
                }
       
            }
            if(treasureFound == true)
            {
                if(foundRiddles[0] == 0)
                {
                    treasureFound = false;
                }
            }
        }
    }

    public double CalculateBearingInDegreesBetweenTwoCoords(double lat1, double lon1, double lat2, double lon2)
    {
        double y = Math.Sin(lon2 - lon1) * Math.Cos(lat2);
        double x = Math.Cos(lat1) * Math.Sin(lat2) -
                  Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(lon2 - lon1);
        double θ = Math.Atan2(y, x);
        double brng = (θ * 180 / Math.PI + 360) % 360;
        return brng;
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

    public double[] GetCurrentLocation()
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
