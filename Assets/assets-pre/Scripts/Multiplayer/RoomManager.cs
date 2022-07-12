using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private string mapType;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region UI Callback Methods

    public void JoinRandomRoom(){
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnEnterButtonClicked_Outdoor(){
        mapType = MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR;
        ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() {{MultiplayerVRConstants.MAP_TYPE_KEY, mapType}};
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties,0);
    }
    #endregion

    #region Photon Callback Methods

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("A room us create with the name: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("The local player: " + PhotonNetwork.NickName + "joined to" + PhotonNetwork.CurrentRoom.Name + "Player count " + PhotonNetwork.CurrentRoom.PlayerCount);
        if(PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerVRConstants.MAP_TYPE_KEY)){

            object mapType;
            if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerVRConstants.MAP_TYPE_KEY, out mapType)){
                Debug.Log("Joined room with the map: " + (string)mapType);

                if((string)mapType == MultiplayerVRConstants.MAP_TYPE_VALUE_OUTDOOR){
                    PhotonNetwork.LoadLevel("World_Outdoor");
                }//else if para school
            }
        }
    
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    #endregion

    #region Private Methods

    private void CreateAndJoinRoom(){
        string randomRoom = "Room_" + Random.Range(0,1000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;
        string[] roomPropsInLobby = {MultiplayerVRConstants.MAP_TYPE_KEY};

        ExitGames.Client.Photon.Hashtable custoomRoomProperties = new ExitGames.Client.Photon.Hashtable() {{MultiplayerVRConstants.MAP_TYPE_KEY, mapType}};


        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = custoomRoomProperties;
        PhotonNetwork.CreateRoom(randomRoom, roomOptions);
    }
    #endregion
}
