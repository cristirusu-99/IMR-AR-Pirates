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
        if (eventCode == 1)
        {
            object[] data = (object[])photonEvent.CustomData;
            double[] riddlesCoords = (double[])data[0];
            string[] riddlesTexts = (string[])data[1];
            double[] treasureCoords = (double[])data[2];
            for(int i = 0, j = 0; i < riddlesTexts.Length; i++, j += 2)
            {
                Debug.Log("Riddle " + (i + 1) + " text : " + riddlesTexts[i]);
                Debug.Log("Riddle " + (i + 1) + " x coord: " + riddlesCoords[j] + " y coord: " + riddlesCoords[j + 1]);            
            }
            Debug.Log("Treasure x coord: " + treasureCoords[0] + " y coord: " + treasureCoords[1]);
        }
    }
}
