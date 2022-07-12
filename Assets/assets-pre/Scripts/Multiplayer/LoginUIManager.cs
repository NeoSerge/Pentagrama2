using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUIManager : MonoBehaviour
{
    public GameObject ConnectOptionsPanelGameObject,ConnectWithNamePanelGameObject;

    #region Unity Methods
    void Start()
    {
        ConnectOptionsPanelGameObject.SetActive(true);
        ConnectWithNamePanelGameObject.SetActive(false);
    }

    
    #endregion
}
