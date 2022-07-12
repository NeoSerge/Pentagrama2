using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class SpawnerPlaces : MonoBehaviourPun
{
    public CharacterController controller;
    public GameObject myRig;
    public Vector3 lobby,home,barSpawn,discoSpawn,teatroSpawn,conciertoSpawn,festivalSpawn;
    public string place;
    void Awake()
    {
        if (!photonView.IsMine)
            return;
    }

    void Start()
    {
        controller = this.gameObject.GetComponent<CharacterController>();
        //myRig = gameObject.GetComponentInParent<GameObject>();
    }
    [PunRPC]
    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.name.Equals("GoHome")){
            GoToPosition(home);
        }else if(collider.gameObject.name.Equals("BarMoneda")){
            GoToLobby("BarMoneda");
        }else if(collider.gameObject.name.Equals("DiscoMoneda")){
            GoToLobby("DiscoMoneda");
        }else if(collider.gameObject.name.Equals("ConciertoMoneda")){
            GoToLobby("ConciertoMoneda");
        }else if(collider.gameObject.name.Equals("TeatroMoneda")){
            GoToLobby("TeatroMoneda");
        }else if(collider.gameObject.name.Equals("FestivalMoneda")){
            GoToLobby("FestivalMoneda");
        }else if(collider.gameObject.name.Equals("IrLugar")){
            ChoosePlace();
        }
    }
    [PunRPC]
    public void GoToPosition(Vector3 spawnTarget){
        controller.enabled = false;
        this.gameObject.transform.localPosition = Vector3.zero;
        myRig.transform.localPosition = spawnTarget;
        StartCoroutine(WaitChangeSP());
        controller.enabled = true;
    }

    [PunRPC]
    public void GoToLobby(string targetPlace){
        GoToPosition(lobby);
        place = targetPlace;
    }


    [PunRPC]
    public void ChoosePlace(){
        string namePlace = place;
        switch(namePlace){
            case "BarMoneda":
                GoToPosition(barSpawn);
                break;
            case "DiscoMoneda":
                GoToPosition(discoSpawn);
                break;
            case "ConciertoMoneda":
                GoToPosition(conciertoSpawn);
                break;
            case "TeatroMoneda":
                GoToPosition(teatroSpawn);
                break;
            case "FestivalMoneda":
                GoToPosition(festivalSpawn);
            break;
        }
    }

    [PunRPC]
    public IEnumerator WaitChangeSP()
    {
        yield return new WaitForSeconds(2.0f);

    }
}
