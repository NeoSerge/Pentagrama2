using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class VirtualWorldManager : MonoBehaviourPunCallbacks
{
  #region Photon Callbacks Method

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("player count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

  #endregion
  
}
