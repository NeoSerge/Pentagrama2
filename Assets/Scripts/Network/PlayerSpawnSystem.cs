using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DiscoVR;
using Mirror;
using System;
using System.Linq;
namespace DiscoVR.Spawning
{
    public class PlayerSpawnSystem : NetworkBehaviour
    {
        [SerializeField] private GameObject playerPrefab = null;

        private static List<Transform> spawnPoints = new List<Transform>();

        private int nextIndex = 0;

        public static void AddSpawnPoin(Transform transform)
        {
            spawnPoints.Add(transform);
            spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
        }
        public static void RemoveSpawPoint(Transform transform) => spawnPoints.Remove(transform);

        public override void OnStartClient()
        {
            //InputManager.Add(actionMapNames.Player);
            //InputManager.Controls.Player.Look.Enagle();
        }
        public override void OnStartServer() => NetworkManagerDisco.OnServerReadied += SpawnPlayer;

        private void OnDestroy()
        {
            if (!isServer) { return; }
            NetworkManagerDisco.OnServerReadied -= SpawnPlayer;            
        }

        [Server]
        public void SpawnPlayer(NetworkConnection conn)
        {
            Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndex);
            if (spawnPoint == null)
            {
                Debug.Log($"missing spawn point for player {nextIndex}");
                return;
            }
            GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);
            nextIndex++;
        }
    }
}
