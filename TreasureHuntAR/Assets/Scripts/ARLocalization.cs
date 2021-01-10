using System;
using UnityEngine;

public class ARLocalization : MonoBehaviour
{
    public double[] riddlesCoords;
    public string[] riddlesTexts;
    public double[] treasureCoords;
    public int[] foundRiddles;
    public string currentRiddleText;

    void Update()
    {
        if(riddlesCoords != null)
        {
            CalculateDistanceBetweenUserAndRiddles();
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
        Debug.Log(riddlesCoords.ToString());
        Debug.Log(riddlesTexts.ToString());
        Debug.Log(treasureCoords.ToString());
    }

    public void CalculateDistanceBetweenUserAndRiddles()
    {
        double[] userLocation = GetCurrentLocation();
        currentRiddleText = "";
        for (int i = 0, j = 0; i < riddlesCoords.Length; i += 2, j++)
        {
            if(DistanceInMetres(userLocation[0], userLocation[1], riddlesCoords[i], riddlesCoords[i + 1]) < 20)
            {
                if(foundRiddles[j] == 1)
                {
                    continue;
                } 
                else
                {
                    foundRiddles[j] = 1;
                    currentRiddleText = riddlesTexts[j];
                    break;
                }
            }

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
