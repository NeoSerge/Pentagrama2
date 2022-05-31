using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System.Linq;

namespace DiscoVR
{
    public class NetworkManagerDisco : NetworkManager
    {
        [SerializeField] private int minPlayer = 2;
        [SerializeField] private string menuScene = string.Empty;
        [SerializeField] private string GameScene = string.Empty;
        [Header("Room")]
        [SerializeField] private NetworkRoomPlayerDisco roomPlayerPrefab = null;

        [Header("Game")]
        [SerializeField] private NetworkGamePlayerDisco gamePlayerPrefab = null;
        [SerializeField] private GameObject playerSpawnSysytem = null;
        [SerializeField] private GameObject roundSystem = null;

        public static event Action OnClientConnected;
        public static event Action OnClientDisconnected;
        public static event Action<NetworkConnection> OnServerReadied;

        public List<NetworkRoomPlayerDisco> RoomPlayers { get; } = new List<NetworkRoomPlayerDisco>();
        public List<NetworkGamePlayerDisco> GamePlayers { get; } = new List<NetworkGamePlayerDisco>();

        public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();//falta algo
        public override void OnStartClient()
        {
            var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();
            // var spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();
            Debug.Log(spawnablePrefabs.Count);
            foreach (var prefab in spawnablePrefabs)
            {
                // ClientScene == NetworkClient
                // NetworkClient.RegisterPrefab(prefab);
                NetworkClient.RegisterPrefab(prefab);
            }
            
        }
        
        public override void OnClientConnect(NetworkConnection conn)
        {
            
            if (!clientLoadedScene)
            {
                //if (!NetworkClient.ready) NetworkClient.Ready(conn);
                if (!NetworkClient.ready)
                {
                    NetworkClient.Ready();
                }
                NetworkClient.AddPlayer();
                //OnClientSceneChanged.AddPlayer();
                //OnClientSceneChanged();
            }
            //base.OnClientConnect(conn);
            OnClientConnected?.Invoke();
            
        }

        
        public override void OnClientDisconnect(NetworkConnection conn)
        {
            base.OnClientDisconnect(conn);
            OnClientConnected?.Invoke();
        }
        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            if (numPlayers >= maxConnections)
            {
                conn.Disconnect();
                return;
            }
            if (SceneManager.GetActiveScene().name != menuScene)
            {
                conn.Disconnect();
                return;
            }
        }
        
        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            Debug.Log("*****" + SceneManager.GetActiveScene().name + "  menuScene: "+ menuScene);
            if (SceneManager.GetActiveScene().name == menuScene)
            {
                bool isLeader = RoomPlayers.Count == 0;
                NetworkRoomPlayerDisco roomPlayerInstance = Instantiate(roomPlayerPrefab);

                roomPlayerInstance.IsLeader = isLeader;
                NetworkServer.AddPlayerForConnection(conn, roomPlayerInstance.gameObject);
            }
        }
        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            if (conn.identity != null)
            {
                var player = conn.identity.GetComponent<NetworkRoomPlayerDisco>();

                RoomPlayers.Remove(player);
                NotifyPlayerOfReadyState();
            }
            base.OnServerDisconnect(conn);
        }

        public void NotifyPlayerOfReadyState()
        {
            foreach (var player in RoomPlayers)
            {
                player.HandleReadyToStart(IsReadyToStart());
            }
        }
        public bool IsReadyToStart()
        {
            if (numPlayers < minPlayer) { return false; }
            foreach (var player in RoomPlayers)
            {
                if (!player.IsReady) { return false; }
            }
            return true;
        }

        public override void OnStopServer()
        {
            RoomPlayers.Clear();
            GamePlayers.Clear();
        }




        public void StartGame()
        {
            if (SceneManager.GetActiveScene().name == menuScene)
            {
                if (!IsReadyToStart()) { return; }
                ServerChangeScene(GameScene);
            }
        }
        public override void ServerChangeScene(string newSceneName)
        {
            //From menu to game
            //newSceneName.StartsWith("scene_Map")
            if (SceneManager.GetActiveScene().name == menuScene && newSceneName.StartsWith(GameScene))
            {
                for(int i = RoomPlayers.Count-1; i>=0; i--)
                {
                    var conn = RoomPlayers[i].connectionToClient;
                    var gameplayInstace = Instantiate(gamePlayerPrefab);
                    gameplayInstace.SetDisplayName(RoomPlayers[i].DisplayName);

                    NetworkServer.Destroy(conn.identity.gameObject);

                    NetworkServer.ReplacePlayerForConnection(conn, gameplayInstace.gameObject);
                }
            }
            base.ServerChangeScene(newSceneName);
        }
        public override void OnServerSceneChanged(string sceneName)
        {
            if (sceneName.StartsWith(GameScene))
            {
                GameObject playerSpawnSystemInstance = Instantiate(playerSpawnSysytem);
                NetworkServer.Spawn(playerSpawnSystemInstance);                
            }
        }
        public override void OnServerReady(NetworkConnectionToClient conn)
        {
            base.OnServerReady(conn);
            OnServerReadied?.Invoke(conn);
        }

    }
}
