
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;

public class PhotonSendEvent
{
    public static void SendHintsAndBoardLatAndLong(byte playerCount)
    {
        object[] content = new object[] { ScenesData.GetValidRiddlesCoords(), ScenesData.GetValidRiddlesText(), ScenesData.treasureCoords, playerCount };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(10, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void SendCurrentUserLocation(double[] location)
    {
        object[] content = new object[] { location };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(ScenesData.playerNumber, content, raiseEventOptions, SendOptions.SendReliable);
    }
}
