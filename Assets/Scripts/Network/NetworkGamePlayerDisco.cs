using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
namespace DiscoVR
{
    public class NetworkGamePlayerDisco : NetworkBehaviour
    {
        [SyncVar]
        private string displayName = "Loading ...";

        private NetworkManagerDisco room;
        private NetworkManagerDisco Room
        {
            get
            {
                if (room != null) { return room; }
                return room = NetworkManager.singleton as NetworkManagerDisco;
            }
        }

        public override void OnStartClient()
        {
            if (Room.dontDestroyOnLoad) { DontDestroyOnLoad(gameObject); }

            Room.GamePlayers.Add(this);
        }
        
        public override void OnStopClient()
        {
            Room.GamePlayers.Remove(this);
        }
        [Server]
        public void SetDisplayName(string displayName)
        {
            this.displayName = displayName;
        }

    }
}
