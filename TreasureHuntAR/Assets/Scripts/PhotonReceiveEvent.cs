using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

public class PhotonReceiveEvent : MonoBehaviour, IOnEventCallback
{
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode == 10)
        {
            object[] data = (object[])photonEvent.CustomData;
            double[] riddlesCoords = (double[])data[0];
            string[] riddlesTexts = (string[])data[1];
            double[] treasureCoords = (double[])data[2];
            byte playerNumber = (byte)data[3];
            for(int i = 0, j = 0; i < riddlesTexts.Length; i++, j += 2)
            {
                Debug.Log("Riddle " + (i + 1) + " text : " + riddlesTexts[i]);
                Debug.Log("Riddle " + (i + 1) + " x coord: " + riddlesCoords[j] + " y coord: " + riddlesCoords[j + 1]);            
            }
            Debug.Log("Treasure x coord: " + treasureCoords[0] + " y coord: " + treasureCoords[1]);
            GameObject.Find("ARLocalization").GetComponent<ARLocalization>().ReceivedGameCoordsAndTexts(riddlesCoords, treasureCoords, riddlesTexts, playerNumber);
        }
        if(eventCode == 1)
        {
            object[] data = (object[])photonEvent.CustomData;
            double[] location = (double[])data[0];
            ScenesData.playersCoords[0] = location[0];
            ScenesData.playersCoords[1] = location[1];
            //Debug.Log("Coord x1: " + ScenesData.playersCoords[0] + " Coord y1: " + ScenesData.playersCoords[1]);

        }
        if(eventCode == 2)
        {
            object[] data = (object[])photonEvent.CustomData;
            double[] location = (double[])data[0];
            ScenesData.playersCoords[2] = location[0];
            ScenesData.playersCoords[3] = location[1];
            //Debug.Log("Coord x2: " + ScenesData.playersCoords[2] + " Coord y2: " + ScenesData.playersCoords[3]);
        }
        if(eventCode == 3)
        {
            object[] data = (object[])photonEvent.CustomData;
            double[] location = (double[])data[0];
            ScenesData.playersCoords[4] = location[0];
            ScenesData.playersCoords[5] = location[1];
            //Debug.Log("Coord x3: " + ScenesData.playersCoords[4] + " Coord y3: " + ScenesData.playersCoords[5]);
        }
        if(eventCode == 4)
        {
            object[] data = (object[])photonEvent.CustomData;
            double[] location = (double[])data[0];
            ScenesData.playersCoords[6] = location[0];
            ScenesData.playersCoords[7] = location[1];
            //Debug.Log("Coord x4: " + ScenesData.playersCoords[6] + " Coord y4: " + ScenesData.playersCoords[7]);
        }
    }
}
