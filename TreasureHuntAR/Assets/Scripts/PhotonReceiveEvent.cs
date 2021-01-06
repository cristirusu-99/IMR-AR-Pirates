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
            Debug.Log("Coord X: " + latLong.x + "Coord Y:" + latLong.y);
           /* for (int index = 0; index < latLong.Count(); ++index)
            {
                Debug.Log("Coordonata: " + latLong[index]);
            }*/
        }
    }
}
