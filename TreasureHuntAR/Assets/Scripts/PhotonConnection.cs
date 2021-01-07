
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.Globalization;

public class PhotonConnection : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    #endregion


    #region Private Fields


    /// <summary>
    /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
    /// </summary>
    string gameVersion = "1";
    PhotonSendEvent photonSendEvent = new PhotonSendEvent();

    #endregion


    #region MonoBehaviour CallBacks


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
    /// </summary>
    void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    /// <summary>
    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    /// </summary>
    void Start()
    {
        Connect();
    }


    #endregion


    #region Public Methods


    /// <summary>
    /// Start the connection process.
    /// - If already connected, we attempt joining a random room
    /// - if not yet connected, Connect this application instance to Photon Cloud Network
    /// </summary>
    public void Connect()
    {
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)
        {

        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }


    public void CreateRoom()
    {
        Text roomCode = GameObject.Find("RoomCode").GetComponent<Text>();
        Debug.Log("Creating room with code: " + roomCode.text.Substring(roomCode.text.Length - 4, 4) + " lenght " + (roomCode.text.Length - (roomCode.text.Length - 4)));
        PhotonNetwork.CreateRoom(roomCode.text.Substring(roomCode.text.Length - 4, 4), new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        //PhotonNetwork.LoadLevel("ARScene");
    }

    public void JoinRoom()
    {
        InputField roomCode = GameObject.Find("InputField").GetComponent<InputField>();
        Debug.Log("Joining room with code: " + roomCode.text);
        bool joinable = PhotonNetwork.JoinRoom(roomCode.text);
        if (joinable == false)
        {
            Resources.FindObjectsOfTypeAll<GameObject>()
                     .FirstOrDefault(g => g.name == "Pop-upRoomCode")
                     .SetActive(true);
            Debug.Log("Enter code!");
            GameObject.Find("InputField").SetActive(false);
        }
        Debug.Log(joinable);
    }
    #region MonoBehaviourPunCallbacks Callbacks


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                string coord = GameObject.Find("Coord_Riddle1").GetComponent<Text>().text;
                string[] latLong = coord.Split(' ');
                float x = float.Parse(latLong[0], CultureInfo.InvariantCulture.NumberFormat);
                float y = float.Parse(latLong[1], CultureInfo.InvariantCulture.NumberFormat);
                Debug.Log("Coco x: " + x + " Coco y: ");
                photonSendEvent.SendHintsAndBoardLatAndLong(new Vector2(x, y));
            }
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Create Room with success");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(returnCode + " " + message);
        //base.OnCreateRoomFailed(returnCode, message);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        //PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        #endregion
    }

    #endregion
}
