using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{ //Ver video para garegar funcionalidad de teleportation
    public GameObject LocalXRRigGameObject;
    public GameObject AvatarHeadGameObject;
    public GameObject AvatarBodyGameObject;

    void Start()
    {
        if(photonView.IsMine){
            //The player is local
            LocalXRRigGameObject.SetActive(true);
            SetLayerRecursively(AvatarHeadGameObject, 6);
            SetLayerRecursively(AvatarBodyGameObject, 6);

        }else{
            LocalXRRigGameObject.SetActive(false);
            SetLayerRecursively(AvatarHeadGameObject, 0);
            SetLayerRecursively(AvatarBodyGameObject, 0);
        }
    }

    
    void Update()
    {
        
    }

     void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
