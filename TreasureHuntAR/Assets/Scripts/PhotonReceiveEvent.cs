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
            Vector2 latLong = (Vector2)data[0];
            for (int index = 0; index < data.Length; ++index)
            {
                Debug.Log("Coordonata: " + latLong[index]);
            }
        }
    }
}
