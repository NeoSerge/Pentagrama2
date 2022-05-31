using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;
using DiscoVR;

public class RoundSystem : NetworkBehaviour
{
    private NetworkManagerDisco room;

    private NetworkManagerDisco Room
    {
        get
        {
            if (room != null) { return room; }
            return room = NetworkManager.singleton as NetworkManagerDisco;
        }
    }
    public override void OnStartServer()
    {
        NetworkManagerDisco.OnServerReadied += (conn)=> CheckToStartRound();
    }

    [ServerCallback]

    private void OnDestroy()
    {
        NetworkManagerDisco.OnServerReadied -= (conn) => CheckToStartRound();

    }

    [Server]
    private void CheckToStartRound()
    {
        if (Room.GamePlayers.Count(x => x.connectionToClient.isReady) != Room.GamePlayers.Count) { return; }
        Debug.Log("cuenta regresiva");
        RcpStartCountdown();
    }
    //llamar en una parte del codigo ej contador cuando llegue a cero
    [ServerCallback]
    public void StartRound()
    {
        RcpStartRound();
    }

    public void CountdownEnded()
    {
        // apagar textto, panel,etc
        Debug.Log("termino la animacion");

    }

    [ClientRpc]
    private void RcpStartCountdown()
    {
        Debug.Log("activar cuenta regresiva 3,2,1 000");
    }
    [ClientRpc]
    private void RcpStartRound()
    {
        Debug.Log("RcpStartRound");
        //InputManager.Remove(ActionNames.Players);
    }

}
