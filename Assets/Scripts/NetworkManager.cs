using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[System.Serializable]
public class DefaultRoom
{
    public string name;
    public int sceneIndex;
    public int maxPlayer;
}

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public List<DefaultRoom> defaultRooms; 

    public bool plaza = false;


    // Start is called before the first frame update
    void Start()
    {
            ConnectToServer();
    }

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect To Server...");
    }

    public override void OnConnectedToMaster()
    {
        
        base.OnConnectedToMaster();
        Debug.Log("Connected To Master...");


        if (plaza)
        {
            InitializeRoom(0);
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }

    }

    public override void OnJoinedLobby()
    {
        
        base.OnJoinedLobby();
        Debug.Log("We joined the lobby");
    }

    public void InitializeRoom(int defaultRoomIndex)
    {
        DefaultRoom roomSettings = defaultRooms[defaultRoomIndex];

        //LoadScene
        PhotonNetwork.LoadLevel(roomSettings.sceneIndex);
        
        //Create a room
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)roomSettings.maxPlayer;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        PhotonNetwork.JoinOrCreateRoom(roomSettings.name, roomOptions, TypedLobby.Default);
        Debug.Log(roomSettings.name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a Room");
        base.OnJoinedRoom();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player joined the roomPlaza");
        base.OnPlayerEnteredRoom(newPlayer);
    }
}
