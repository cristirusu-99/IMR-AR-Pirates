
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

public class PhotonSendEvent
{
    public static void SendHintsAndBoardLatAndLong()
    {
        object[] content = new object[] { ScenesData.GetValidRiddlesCoords(), ScenesData.GetValidRiddlesText(), ScenesData.treasureCoords };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(1, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void SendCurrentUserLocation(double[] location)
    {
        object[] content = new object[] { location };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(2, content, raiseEventOptions, SendOptions.SendReliable);
    }
}
